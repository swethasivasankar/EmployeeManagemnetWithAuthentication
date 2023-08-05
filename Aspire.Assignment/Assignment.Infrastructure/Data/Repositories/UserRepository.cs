using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Migrations;

namespace Assignment.Core.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AspireAssignmentDBContext context) : base(context)
        {
        }
    }
}