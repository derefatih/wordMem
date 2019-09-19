using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordMem.Business.Abstract;
using WordMem.CrossCutting.DTOs;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WordMem.Business.Concrete
{
    public class AuthService:IAuthService
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IUnitOfWork uow;

        public AuthService(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            uow = _uow;
        }

        public void InitialUserSettingsSave(ApplicationUser appuser)
        {
            var userSettings = new UserSetting()
            {
                ApplicationUser = appuser,
                Compare = true,
                Fill = true,
                Random = true,
                TestWordCount = 5
            };
            uow.UserSettings.Add(userSettings);
            uow.SaveChanges();
        }

        public async Task<SignInResult> LogginUser(LoginDTO loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                return result;
            }
            return SignInResult.Failed;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            ApplicationUser appuser = new ApplicationUser();
            appuser.UserName = registerDTO.UserName;
            appuser.Email = registerDTO.Email;

            var result = await userManager.CreateAsync(appuser, registerDTO.Password);

            if (result.Succeeded)
            {
                //usersetting create
                InitialUserSettingsSave(appuser);
            }

            return result;
        }
    }
}
