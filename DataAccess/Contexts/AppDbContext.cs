using Core.Entities.Identity;
using Core.Entities.Medicio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>opts):base(opts)
        {
        }
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Settings> SettingsTable { get; set; } = null!;
    }
}
