using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }


    }
}
