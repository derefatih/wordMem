using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordMem.CrossCutting.DTOs;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.Controllers
{
    [Authorize]
    public class WordController : Controller
    {
        private IUnitOfWork uow;
        private UserManager<ApplicationUser> userManager;

        public WordController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            uow = _uow;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Get(id).ApplicationUser;

            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }

            var viewModel = new WordListDTO();
            viewModel.Words = new List<WordList>();
            viewModel.LearnedWords = new List<WordList>();
            viewModel.StudiedWords = new List<WordList>();
            var cat = uow.Categories.Get(id);
            viewModel.CategoryName = cat.CategoryName;
            viewModel.CategoryId = cat.CategoryId;

            //var words = uow.Words.GetAll()
            //    .Include(i => i.WordCategories)
            //    .ThenInclude(i => i.Category)
            //    .Where(i => i.WordCategories.Any(a => a.Category.CategoryId == id));

            var words = uow.Words.GetAll()
                .Include(i=>i.WordStatistic)
                .Where(i=>i.WordCategories.Any(k=>k.CategoryId==id) && i.WordStatistic.IsLearned==false && i.WordStatistic.IsStudied == false);

            var LearnedWords = uow.Words.GetAll()
             .Include(i => i.WordStatistic)
             .Where(i => i.WordCategories.Any(k => k.CategoryId == id) && i.WordStatistic.IsLearned == true);

            var StudiedWords = uow.Words.GetAll()
           .Include(i => i.WordStatistic)
           .Where(i => i.WordCategories.Any(k => k.CategoryId == id) && i.WordStatistic.IsStudied == true && i.WordStatistic.IsLearned == false);

            viewModel.WordCount = words.Count();
            viewModel.LearnedWordCount = LearnedWords.Count();
            viewModel.StudiedWordCount = StudiedWords.Count();
                
            

            foreach (var item in words)
            {
                var model = new WordList();
                model.WordId = item.WordId;
                model.OwnLang = item.OwnLang;
                model.ForeignLang = item.ForeignLang;
                model.Image = item.Image;
                var wordStatistics=uow.WordStatistics.Find(i => i.WordId == item.WordId).First();
                model.Asked = wordStatistics.Asked;
                model.Answered = wordStatistics.Answered;

                viewModel.Words.Add(model);
            }

            foreach (var item in LearnedWords)
            {
                var model = new WordList();
                model.WordId = item.WordId;
                model.OwnLang = item.OwnLang;
                model.ForeignLang = item.ForeignLang;
                model.Image = item.Image;
                var wordStatistics = uow.WordStatistics.Find(i => i.WordId == item.WordId).First();
                model.Asked = wordStatistics.Asked;
                model.Answered = wordStatistics.Answered;

                viewModel.LearnedWords.Add(model);
            }

            foreach (var item in StudiedWords)
            {
                var model = new WordList();
                model.WordId = item.WordId;
                model.OwnLang = item.OwnLang;
                model.ForeignLang = item.ForeignLang;
                model.Image = item.Image;
                var wordStatistics = uow.WordStatistics.Find(i => i.WordId == item.WordId).First();
                model.Asked = wordStatistics.Asked;
                model.Answered = wordStatistics.Answered;

                viewModel.StudiedWords.Add(model);
            }

            return View(viewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.WordCategories.Any(k => k.WordId == id)).Select(l=>l.ApplicationUser).First();
                

            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }
            return View(uow.Words.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Word word, IFormFile file)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.WordCategories.Any(k => k.Word == word)).Select(l => l.ApplicationUser).First();


            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }


            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        word.Image = file.FileName;
                    }
                }
                uow.Words.Edit(word);
                uow.Words.Save();
                //return RedirectToAction("List", new { id = id });
            }
            return View(word);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
           
            return View(new Word());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Word word, IFormFile file, int id)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        word.Image = file.FileName;
                    }
                }
                else
                {
                    word.Image = "th.jpg";
                }

                //save category
                var category = uow.Categories.Get(id);
                word.WordCategories = new List<WordCategory>() { new WordCategory() { Word = word, Category = category } };

                //save word
                uow.Words.Add(word);

                //save word statistics
                var wordStatistic = new WordStatistic()
                {
                    Word = word,
                    Asked = 0,
                    IsLearned = false,
                    IsStudied=false,
                    Answered = 0
                };
                uow.WordStatistics.Add(wordStatistic);


                //save all changes
                uow.SaveChanges();

                return RedirectToAction("List", new { id = id });
            }

            return View(word);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.WordCategories.Any(k => k.WordId == id)).Select(l => l.ApplicationUser).First();


            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }

            //get category
            var cat = uow.Categories.GetAll()
                        .Include(i => i.WordCategories)
                        .Where(a => a.WordCategories.Any(k => k.WordId == id)).FirstOrDefault();
            

            var word = uow.Words.Get(id);
            uow.Words.Delete(word);
            uow.Words.Save();
            return RedirectToAction("List", new { id = cat.CategoryId });
        }

        [HttpGet]
        public async Task<IActionResult> MarkAsLearned(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.WordCategories.Any(k => k.WordId == id)).Select(l => l.ApplicationUser).First();


            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }

            var word = uow.Words.Get(id);
            //var catId = word.WordCategories.Where(i=>i.WordId==id).Select(i=>i.CategoryId).First();
            var catId = uow.Categories.GetAll()
                .Include(i => i.WordCategories)
                .Where(i => i.WordCategories.Any(k => k.WordId == id)).Select(k => k.CategoryId).First();
            var wordStatistic = uow.WordStatistics.GetAll()
                .Where(i=>i.WordId == id).First();
            wordStatistic.IsLearned = true;

            uow.WordStatistics.Edit(wordStatistic);
            uow.SaveChanges();

            return RedirectToAction("List", new {id= catId });
        }

        [HttpGet]
        public async Task<IActionResult> AddToWords(int id)
        {

            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.WordCategories.Any(k => k.WordId == id)).Select(l => l.ApplicationUser).First();


            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }

            var word = uow.Words.Get(id);
            var catId = uow.Categories.GetAll()
            .Include(i => i.WordCategories)
            .Where(i => i.WordCategories.Any(k => k.WordId == id)).Select(k => k.CategoryId).First();
            var wordStatistic = uow.WordStatistics.GetAll()
                .Where(i => i.WordId == id).First();
            wordStatistic.IsLearned = false;

            uow.WordStatistics.Edit(wordStatistic);
            uow.SaveChanges();

            return RedirectToAction("List", new { id = catId });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsStudied(int[]  wordId)
        {
            var currentUser = await GetCurrentUserAsync();
            var categoryUser = uow.Categories.Find(i => i.WordCategories.Any(k => k.WordId == wordId[0])).Select(l => l.ApplicationUser).First();


            if (currentUser != categoryUser)
            {
                return View("AccessDenied");
            }

            var wordStatistic = new WordStatistic();

            foreach (var id in wordId)
            {
                wordStatistic = uow.WordStatistics.GetAll()
                    .Where(i => i.WordId == id).First();
                wordStatistic.IsStudied = true;
                uow.WordStatistics.Edit(wordStatistic);
            }


            uow.SaveChanges();

            var catId = uow.Categories.GetAll()
             .Include(i => i.WordCategories)
             .Where(i => i.WordCategories.Any(k => k.WordId == wordId[0])).Select(k => k.CategoryId).First();


            return RedirectToAction("List", new { id = catId });


        }

    }
}