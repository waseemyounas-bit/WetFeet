using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public bool? IsPaid { get; set; }
        public double? Price { get; set; }
        public bool IsPublic { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<ContentFile>? ContentFiles { get; set; }
    }
}
