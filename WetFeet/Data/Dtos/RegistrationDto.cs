using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string Email { get; set; }
        [NotMapped]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        public IFormFile Pic { get; set; }
        public string ImageName { get; set; }
        public string? Website { get; set; }
        public string About { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
