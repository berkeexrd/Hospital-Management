using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IApplicationUserService _userService;

        public UsersController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int PageNumber=1, int PageSize=10)
        {
            return View(_userService.GetAll(PageNumber,PageSize));
        }

        public IActionResult AllDoctors(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetAllDoctor(PageNumber, PageSize));
        }
        //public IActionResult SearchDoctors(int PageNumber = 1, int PageSize = 10, string Spicility=null)
        //{
        //    return View(_userService.SearchDoctor(PageNumber, PageSize,Spicility));
        //}

    }
}
