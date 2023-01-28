using Microsoft.AspNetCore.Mvc;

namespace Medicio_Exam.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
