using Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Content : BaseEntity
    {
        public string? Text { get; set; }
        public bool? IsPaid { get; set; }
        public double? Price { get; set; }
        public bool IsPublic { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<ContentFile>? ContentFiles { get; set; }
    }
}
