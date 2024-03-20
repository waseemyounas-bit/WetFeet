using AutoMapper;
using Business.IServices;
using Business.Services;
using Data.Dtos;
using Data.Entities;
using Data.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Formats.Tar;
using System.Net.Http.Json;

namespace WetFeetAPI.Controllers
{
    [Route("api/content")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly ResponseDto _response;

        public ContentController(IContentService contentService, IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            this._contentService = contentService;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
            _response = new ResponseDto();
        }

        [HttpPost("postcontent")]
        public async Task<IActionResult> PostContent([FromForm] CreateContentDto contentDto)
        {
            try
            {
                var content = new Content();
                content.Text = contentDto.Text;
                content.IsPaid = contentDto.IsPaid;
                content.Price = contentDto.Price;
                content.IsPublic = contentDto.IsPublic;
                content.UserId = contentDto.UserId;
                var result = _contentService.AddContent(content);

                var contentFiles = new List<ContentFile>();
                if (contentDto?.ContentFiles?.Count() > 0)
                {
                    foreach (var file in contentDto.ContentFiles)
                    {
                        if (file?.Length > 0)
                        {
                            var contentFile = new ContentFile();
                            contentFile.ContentId = result.Id;
                            string wwwRootPath = this._webHostEnvironment.WebRootPath;
                            string NewGuid = Guid.NewGuid().ToString();
                            // Get File Extension
                            string extension = System.IO.Path.GetExtension(file.FileName);
                            contentFile.ContentType = file.ContentType;
                            contentFile.Title = file.FileName;
                            //content.Url = NewGuid + "_" + file.FileName;
                            contentFile.Url = NewGuid + extension;
                            var filePath = Path.Combine(wwwRootPath + "/contents/", contentFile.Url);

                            // Building the path to the uploads directory
                            var fileRoute = Path.Combine(wwwRootPath, "contents");
                            // Create directory if it dose not exist.
                            if (!Directory.Exists(fileRoute))
                            {
                                Directory.CreateDirectory(fileRoute);
                            }

                            Stream stream;
                            stream = new MemoryStream();
                            file.CopyTo(stream);
                            stream.Position = 0;
                            String serverPath = filePath;

                            // Save the file
                            using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                            {
                                await stream.CopyToAsync(writerFileStream);
                                writerFileStream.Dispose();
                            }
                            contentFiles.Add(contentFile);
                        }
                    }

                }
                this._contentService.AddContentFiles(contentFiles);
                _response.Message = "Content posted successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost("editcontent")]
        public async Task<IActionResult> EditContent([FromForm] UpdateContentDto contentDto)
        {
            try
            {
                var content = this._contentService.GetContent(contentDto.Id);
                content.Text = contentDto.Text;
                content.IsPaid = contentDto.IsPaid;
                content.Price = contentDto.Price;
                content.IsPublic = contentDto.IsPublic;
                var result = _contentService.UpdateContent(content);

                var contentFiles = new List<ContentFile>();
                if (contentDto?.ContentFiles?.Count() > 0)
                {
                    foreach (var file in contentDto.ContentFiles)
                    {
                        if (file?.Length > 0)
                        {
                            var contentFile = new ContentFile();
                            contentFile.ContentId = result.Id;
                            string wwwRootPath = this._webHostEnvironment.WebRootPath;
                            string NewGuid = Guid.NewGuid().ToString();
                            // Get File Extension
                            string extension = System.IO.Path.GetExtension(file.FileName);
                            contentFile.ContentType = file.ContentType;
                            contentFile.Title = file.FileName;
                            //content.Url = NewGuid + "_" + file.FileName;
                            contentFile.Url = NewGuid + extension;
                            var filePath = Path.Combine(wwwRootPath + "/contents/", contentFile.Url);

                            // Building the path to the uploads directory
                            var fileRoute = Path.Combine(wwwRootPath, "contents");
                            // Create directory if it dose not exist.
                            if (!Directory.Exists(fileRoute))
                            {
                                Directory.CreateDirectory(fileRoute);
                            }

                            Stream stream;
                            stream = new MemoryStream();
                            file.CopyTo(stream);
                            stream.Position = 0;
                            String serverPath = filePath;

                            // Save the file
                            using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                            {
                                await stream.CopyToAsync(writerFileStream);
                                writerFileStream.Dispose();
                            }
                            contentFiles.Add(contentFile);
                        }
                    }

                }

                //delete existing files
                DeleteContentFiles(contentDto.Id);
                this._contentService.DeleteContentFiles(contentDto.Id);
                this._contentService.AddContentFiles(contentFiles);
                _response.Message = "Content updated successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("getcontents")]
        public async Task<IActionResult> GetAllContents(string userId, int pageNo = 1, int pageSize = 10)
        {
            try
            {
                var objList = await this._contentService.GetContentList(userId, pageNo, pageSize);
                _response.Result = JsonConvert.SerializeObject(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete("deletecontent")]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            try
            {
                DeleteContentFiles(id);
                var result = this._contentService.DeleteContent(id);
                if (result > 0)
                {
                    
                    _response.Message = "Content is deleted succesfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        private void DeleteContentFiles(Guid id)
        {
            var contentFiles = this._contentService.GetContentFiles(id);
            foreach (var file in contentFiles)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "/contents/", file.Url);
                if (System.IO.File.Exists(filePath))//check file exsit or not  
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }
    }
}
