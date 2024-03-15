using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.WetFeetEnums;

namespace Data.Dtos
{
    public class RegistrationAudienceDto
    {
        public RegistrationAudienceDto()
        {
            RegistrationDto = new RegistrationDto();
        }
        public RegistrationDto RegistrationDto { get; set; }
        [Required]
        public Guid SubscriptionId { get; set; }
        public SubscriptionType Type { get; set; }
    }
}
