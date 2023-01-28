using Core.Entities.Medicio;
using DataAccess.Contexts;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class DoctorsRepository:Repository<Doctor>,IDoctorsRepository
    {
        public DoctorsRepository(AppDbContext context):base(context)
        {

        }
    }
}
