using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordMem.CrossCutting.DTOs;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace EduraWebUI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private IUnitOfWork uow;
        private UserManager<ApplicationUser> userManager;

        public CategoryController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
        {
            uow = _uow;
            userManager = _userManager;
            
        }

        public IActionResult Index()
        {
            return View(uow.Categories.GetByName("Electronics"));
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> List()
        {
            var currentUser = await GetCurrentUserAsync();
            var categories = uow.Categories.Find(k=>k.ApplicationUser== currentUser);
            var viewmodel = new List<CategoryListDTO>();
            foreach (var item in categories)
            {
                var model = new CategoryListDTO();
                model.CategoryId=item.CategoryId;
                model.CategoryName = item.CategoryName;

                model.LearnedCount = uow.WordStatistics.GetAll()
                    .Include(i => i.Word)
                    .ThenInclude(i => i.WordCategories)
                    .ThenInclude(i => i.Category)
                    .Where(s => s.Word.WordCategories.Any(a => a.CategoryId == item.CategoryId) && s.IsLearned==true).Count();

                model.TotalCount = uow.WordStatistics.GetAll()
                    .Include(i => i.Word)
                    .ThenInclude(i => i.WordCategories)
                    .ThenInclude(i => i.Category)
                    .Where(s=>s.Word.WordCategories.Any(a=>a.CategoryId==item.CategoryId)).Count();

                if (model.TotalCount > 0)
                {
                    model.Percentage =  (int)(((Double)model.LearnedCount / (Double)model.TotalCount) * 100);
                }
                else
                {
                    model.Percentage = 100;
                }

                viewmodel.Add(model);
            }
            return View(viewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Get(id).ApplicationUser;

            if (currentUser!=categoryUser)
            {
                return View("AccessDenied");
            }

            var category = uow.Categories.Get(id);
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }


        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            category.ApplicationUser = await GetCurrentUserAsync();
            uow.Categories.Add(category);
            uow.Categories.Save();
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i=>i.CategoryId==category.CategoryId).Select(k=>k.ApplicationUser).First();

            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }
            category.ApplicationUser = currentUser;
            uow.Categories.Edit(category);
            uow.Categories.Save();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.CategoryId==id).Select(l => l.ApplicationUser).First();


            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }

            var category=uow.Categories.Find(i=>i.CategoryId==id).FirstOrDefault();
            var categoryWords = uow.Words.GetAll()
                .Include(i => i.WordCategories)
                .ThenInclude(i => i.Category)
                .Where(i => i.WordCategories.Any(a => a.Category.CategoryId == id));
            foreach (var item in categoryWords)
            {
                uow.Words.Delete(item);
            }
            uow.Words.Save();
            uow.Categories.Delete(category);
            uow.Categories.Save();
            return RedirectToAction("List");
        }
    }
}