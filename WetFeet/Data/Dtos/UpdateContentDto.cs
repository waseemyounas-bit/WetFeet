using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class UpdateContentDto
    {
        public Guid Id { get; set; }
        [Required]
        public string? Text { get; set; }
        public double? Price { get; set; }
        public bool? IsPaid { get; set; }
        public bool IsPublic { get; set; }
        public string? UserId { get; set; }
        public IFormFile[]? ContentFiles { get; set; }
    }
}
