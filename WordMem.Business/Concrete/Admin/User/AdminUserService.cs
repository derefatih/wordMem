using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WordMem.Business.Abstract.Admin.User;
using WordMem.CrossCutting.DTOs.Admin.User;
using WordMem.Entity;

namespace WordMem.Business.Concrete.Admin.User
{
    public class AdminUserService : IAdminUserService
    {
        private UserManager<ApplicationUser> userManager;

        public AdminUserService(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }


        public UserListDto GetAll()
        {
            UserListDto dto = new UserListDto();

            dto.Users = userManager.Users;

            return dto;
        }
    }
}
