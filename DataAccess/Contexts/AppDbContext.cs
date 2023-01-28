using Core.Entities.Medicio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>opts):base(opts)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
