using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhoto { get; set; }
        public string MainPhoto { get; set; }
        public string AboutMe { get; set; }
        
        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}
