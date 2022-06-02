using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApplication.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
