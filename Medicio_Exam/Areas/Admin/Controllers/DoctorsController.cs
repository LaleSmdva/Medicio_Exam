using Business.Utilities;
using Business.ViewModels.Doctors;
using Core.Entities.Medicio;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Medicio_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorsController : Controller
    {
        private readonly IDoctorsRepository _repository;
        private readonly IWebHostEnvironment _env;

        public DoctorsController(IDoctorsRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll().AsEnumerable());
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model=await _repository.GetByIdAsync(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorVM createDoctorVM)
        {
            if(!ModelState.IsValid) return View(createDoctorVM);
            var fileName =await createDoctorVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "doctors");
            Doctor doctor = new()
            {
                Name = createDoctorVM.Name,
                Position= createDoctorVM.Position,
                Image=fileName
            };
            await _repository.CreateAsync(doctor);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            if (!ModelState.IsValid) return View();
            var model = await _repository.GetByIdAsync(id);
            if (model is null) return NotFound();
            if (id!=model.Id) return BadRequest();
            UpdateDoctorVM doctorVM = new()
            {
                Name=model.Name,
                Position = model.Position,
            };

            return View(doctorVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,UpdateDoctorVM updateDoctorVM)
        {
            if (!ModelState.IsValid) return View(updateDoctorVM);
            var model = await _repository.GetByIdAsync(id);
            if (model is null) return NotFound();
            if (id != model.Id) return BadRequest();
            var fileName = await updateDoctorVM.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "doctors");
            model.Name=updateDoctorVM.Name;
            model.Position = updateDoctorVM.Position;
            model.Image = fileName;

            _repository.Update(model);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model is null) return NotFound();
            //_repository.Delete(model);
            //await _repository.SaveAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (!ModelState.IsValid) return View();
            var model = await _repository.GetByIdAsync(id);
            if(model is null) return NotFound();
            var fileName=Path.Combine(Helper.GetCopyPath(_env.WebRootPath, "assets", "img", "doctors"), model.Image);

            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
           _repository.Delete(model);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
