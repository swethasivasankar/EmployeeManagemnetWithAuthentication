using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Contracts.DTO;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Core.Data.Repositories
{
    public class EmployerRepository : Repository<Employer>, IEmployerRepository
    {
        public EmployerRepository(AspireAssignmentDBContext context) : base(context)
        {
            
        }
    }
}

