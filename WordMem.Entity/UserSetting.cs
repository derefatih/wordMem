using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class UserSetting
    {
        public int UserSettingId { get; set; }
        public int TestWordCount { get; set; }
        public bool Compare { get; set; }
        public bool Fill { get; set; }
        public bool Random { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
