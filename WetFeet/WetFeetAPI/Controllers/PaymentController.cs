﻿using Business.IServices;
using Business.Services;
using Data.Context;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System;
using WetFeetAPI.Utility;

namespace WetFeetAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IStripeService stripeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> singinManager;
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ResponseDto _response;
        public PaymentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPaymentService paymentService, IStripeService stripe, DataContext dataContext, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            this.stripeService = stripe;
            this.userManager = userManager;
            this.singinManager = signInManager;
            this._context = dataContext;
            this.paymentService = paymentService;
            _config = config;
            this.httpContextAccessor = httpContextAccessor;
            this._response = new ResponseDto();
        }

        [HttpPost("processpayment")]
        public async Task<IActionResult> PaymentProcessingAsync([FromBody] AddStripeCard card)
        {
            try
            {
                var user = await this.userManager.FindByIdAsync(card.UserId);
                AddStripeCustomer customer = new AddStripeCustomer()
                {
                    Name = user?.FirstName + " " + user?.LastName,
                    Email = user?.Email,
                    CreditCard = card
                };
                var stripeCustomer = await stripeService.AddStripeCustomerAsync(customer, CancellationToken.None);
                double amount = card.Amount;
                AddStripePayment stripePayment = new AddStripePayment()
                {
                    Amount = Convert.ToInt64(amount) * 100,
                    Currency = "USD",
                    CustomerId = stripeCustomer.CustomerId,
                    Description = "This is testing payment made through Wetfeet.",
                    ReceiptEmail = user.Email
                };
                var result = await stripeService.AddStripePaymentAsync(stripePayment, CancellationToken.None);
                if (result.PaymentId != null)
                {
                    Data.Entities.Payment payment = new Data.Entities.Payment()
                    {

                        UserId = card.UserId,
                        CreatedDate = DateTime.Now,
                        PaidAmount = amount,
                        StripePaymentId = result.PaymentId,
                        UserSubscriptionId = card.UserSubscriptionId
                    };
                    paymentService.AddPayment(payment);
                    _response.Result = payment.Id;
                    _response.Message = "Payment is done successfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("processpaypalpayment")]
        public ActionResult PaymentWithPaypal(string Cancel = null, string blogId = "", string PayerID = "", string guid = "")
        {
            //getting the apiContext  
            var ClientID = _config.GetValue<string>("PayPal:ClientID");
            var ClientSecret = _config.GetValue<string>("PayPal:ClientSecret");
            var mode = _config.GetValue<string>("PayPal:mode");
            APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);
            // apiContext.AccessToken="Bearer access_token$production$j27yms5fthzx9vzm$c123e8e154c510d70ad20e396dd28287";
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = PayerID;
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guidd = Convert.ToString((new Random()).Next(100000));
                    guid = guidd;
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                    _response.Result = paypalRedirectUrl;
                    return Ok(_response);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  

                    var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Payment failed";
                        return BadRequest(_response);
                    }
                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;


                    _response.Message = "Payment is successfull";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            //on successful payment, show success page to user.  
            return Ok(_response);
        }
        private PayPal.Api.Payment payment;
        private PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new PayPal.Api.Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
        {
            //create itemlist and add item objects to it  

            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Detail",
                currency = "USD",
                price = "1.00",
                quantity = "1",
                sku = "asd"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            //var details = new Details()
            //{
            //    tax = "1",
            //    shipping = "1",
            //    subtotal = "1"
            //};
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "1.00", // Total must be equal to sum of tax, shipping and subtotal.  
                //details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}
