using Business.Services.Interfaces;
using Business.Utilities;
using Business.ViewModels.Doctors;
using Core.Entities.Medicio;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class DoctorService:IDoctorService
    {
        private readonly IDoctorsRepository _repository;
        private readonly IWebHostEnvironment _env;

        public DoctorService(IDoctorsRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public DbSet<Doctor> _table => throw new NotImplementedException();

        public IEnumerable<Doctor> GetAll()
        {
            return _repository.GetAll().AsEnumerable();
        }

        public Task<Doctor> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
        public async Task CreateAsync(CreateDoctorVM entity)
        {
            var fileName = await entity.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "doctors");
            Doctor doctor = new()
            {
                Name = entity.Name,
                Position = entity.Position,
                Image = fileName
            };
            await _repository.CreateAsync(doctor);
            await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            var fileName = Path.Combine(Helper.GetCopyPath(_env.WebRootPath, "assets", "img", "doctors"), model.Image);

            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            _repository.Delete(model);
            await _repository.SaveAsync();
        }


        public async Task SaveAsync()
        {
           await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateDoctorVM entity)
        {
            var model = await _repository.GetByIdAsync(id);
           
            var fileName = await entity.Image.CopyFileAsync(_env.WebRootPath, "assets", "img", "doctors");

            model.Name = entity.Name;
            model.Position = entity.Position;
            model.Image = fileName;

            _repository.Update(model);
            await _repository.SaveAsync();
        }
    }
}
