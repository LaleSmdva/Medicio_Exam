using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Doctors
{
	public class UpdateDoctorVM
	{
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required, MaxLength(100)]
        public string? Position { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
