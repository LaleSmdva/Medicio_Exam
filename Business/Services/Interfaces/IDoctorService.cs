using Business.ViewModels.Doctors;
using Core.Entities.Medicio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces;

public interface IDoctorService
{
    IEnumerable<Doctor> GetAll();
    Task<Doctor> GetByIdAsync(int id);
    Task CreateAsync(CreateDoctorVM entity);
    Task UpdateAsync(int id,UpdateDoctorVM entity);
    Task Delete(int id);
    Task SaveAsync();
}
