using Microsoft.EntityFrameworkCore;
using Assignment.Contracts.Data.Entities;

namespace Assignment.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            {
                item.Entity.AddedOn = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<App> App { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<Employer> Employers { get; set; }

        public DbSet<ReferenceTbl> ReferenceTbls { get; set; }




    }
}