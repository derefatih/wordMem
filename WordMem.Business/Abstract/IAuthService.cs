using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordMem.CrossCutting.DTOs;
using WordMem.Entity;

namespace WordMem.Business.Abstract
{
    public interface IAuthService
    {
        Task<SignInResult> LogginUser(LoginDTO loginDto);
        Task<IdentityResult> Register(RegisterDTO registerDTO);
        void InitialUserSettingsSave(ApplicationUser appuser);
        Task LogoutAsync();
    }
}
