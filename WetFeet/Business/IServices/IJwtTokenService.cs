using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
