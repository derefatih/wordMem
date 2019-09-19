using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordMem.Entity;

namespace WordMem.CrossCutting.DTOs.Admin.User
{
    public class UserListDto
    {
        public IQueryable<ApplicationUser> Users { get; set; }
       
    }
}
