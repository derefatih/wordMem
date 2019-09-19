using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.Controllers
{
    [Authorize]
    public class GameController : Controller
    {

        private IUnitOfWork uow;
        private UserManager<ApplicationUser> userManager;

        public GameController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
        {
            uow = _uow;
            userManager = _userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> StudyMode(int categoryId)
        {
            var user = await GetCurrentUserAsync();

            var wordCount = uow.UserSettings.Find(i => i.ApplicationUser == user).Select(i => i.TestWordCount).First();

            var words = uow.Words.GetAll()
                .Include(i => i.WordStatistic)
                .Include(i => i.WordCategories)
                .ThenInclude(i => i.Category)
                .Where(i => i.WordStatistic.IsStudied == false
                    && i.WordStatistic.IsLearned == false
                    && i.WordCategories.Any(k => k.CategoryId == categoryId))
                .Take(wordCount);

            return View(words);
        }
        public IActionResult CheckMode()
        {
            return View();
        }
    }
}