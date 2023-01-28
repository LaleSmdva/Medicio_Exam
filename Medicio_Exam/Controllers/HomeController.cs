using Business.Services.Interfaces;
using Business.ViewModels;
using DataAccess.Contexts;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicio_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly AppDbContext _context;

        public HomeController(IDoctorService doctorService, AppDbContext context)
        {
            _doctorService = doctorService;
            _context = context;
        }

        //private readonly AppDbContext _context;



        public IActionResult Index()
        {
            HomeViewModel homeVM = new()
            {
                Doctors = _context.Doctors.AsEnumerable(),
                SettingsTable = _context.SettingsTable.AsEnumerable()

            };
            //return View(_doctorService.GetAll());
            return View(homeVM);
        }
    }
}
