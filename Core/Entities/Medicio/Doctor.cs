using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Medicio
{
    public class Doctor:IEntity
    {
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string? Name { get; set; }
        [Required,MaxLength(100)]
        public string? Position { get; set; }
        public string? Image { get; set; } 
    }
}
