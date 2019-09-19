using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordMem.Business.Abstract;
using WordMem.Entity;

namespace WordMem.Business.Concrete
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;
        public UserService(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {

           return await userManager.FindByEmailAsync(email);
        }
    }
}
