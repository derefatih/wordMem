using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.Entity;

namespace WordMem.CrossCutting.DTOs
{
    public class UserProfileDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhoto { get; set; }
        public string MainPhoto { get; set; }
        public Address Address { get; set; }
        public string AboutMe { get; set; }
    }
}
