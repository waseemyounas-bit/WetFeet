using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ContentFile
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? ContentType { get; set; }

        [ForeignKey(nameof(Content))]
        public Guid ContentId { get; set; }
    }
}
