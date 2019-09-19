using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordMem.Business.Abstract;
using WordMem.Business.Abstract.Admin.User;

namespace WordMem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private IAdminUserService adminuserService;


        public AdminController(IAdminUserService _adminuserService)
        {
            adminuserService = _adminuserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList()
        {
            var dto = adminuserService.GetAll();
            return View(dto);
        }
    }
}