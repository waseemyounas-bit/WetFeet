﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
