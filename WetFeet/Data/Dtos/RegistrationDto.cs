using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string ImageName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
