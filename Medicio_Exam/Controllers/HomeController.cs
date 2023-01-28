using Business.ViewModels;
using DataAccess.Contexts;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicio_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDoctorsRepository _repository;
        //private readonly AppDbContext _context;

        public HomeController(IDoctorsRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            //HomeViewModel homeVM = new()
            //{
            //    Doctors = _context.Doctors.AsEnumerable(),
            //    SettingsTable = _context.SettingsTable.AsEnumerable()

            //};
            return View(_repository.GetAll().AsEnumerable());
            //return View(homeVM);
        }
    }
}
