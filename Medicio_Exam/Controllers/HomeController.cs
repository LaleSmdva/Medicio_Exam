using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicio_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDoctorsRepository _repository;

        public HomeController(IDoctorsRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            //HomeVM homeVM = new()
            //{

            //};
            return View(_repository.GetAll().AsEnumerable());
        }
    }
}
