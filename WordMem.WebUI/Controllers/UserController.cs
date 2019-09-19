using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordMem.CrossCutting.DTOs;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUnitOfWork uow;

        public UserController(UserManager<ApplicationUser> _userManager, IUnitOfWork _uow)
        {
            userManager = _userManager;
            uow = _uow;
        }

        public IActionResult Index()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userProfile = new UserProfileDTO();
            var user = await GetCurrentUserAsync();
            userProfile.FirsName = user.FirstName;
            userProfile.LastName = user.LastName;
            userProfile.ProfilePhoto = user.ProfilePhoto;
            userProfile.Email = user.Email;
            return View(userProfile);
        }
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            
            var userSetting = new UserSettingsDTO();
            //get currentuser
            var user = await GetCurrentUserAsync();
            //get user settings
            var userSet=uow.UserSettings.Find(i=>i.ApplicationUser==user).First();
            //add viewmodel
            userSetting.Compare = userSet.Compare;
            userSetting.Fill = userSet.Fill;
            userSetting.Random = userSet.Random;
            userSetting.TestWordCount = userSet.TestWordCount;
            return View(userSetting);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDTO model)
        {
            var userProfile = new UserProfileDTO();
            var user = await GetCurrentUserAsync();
            userProfile.FirsName = user.FirstName;
            userProfile.LastName = user.LastName;
            userProfile.ProfilePhoto = user.ProfilePhoto;
            userProfile.Email = user.Email;
            return View(userProfile);
        }
        [HttpPost]
        public async Task<IActionResult> Settings(UserSettingsDTO model)
        {
            var userProfile = new UserProfileDTO();
            var user = await GetCurrentUserAsync();
            userProfile.FirsName = user.FirstName;
            userProfile.LastName = user.LastName;
            userProfile.ProfilePhoto = user.ProfilePhoto;
            userProfile.Email = user.Email;
            return View(userProfile);
        }
    }
}