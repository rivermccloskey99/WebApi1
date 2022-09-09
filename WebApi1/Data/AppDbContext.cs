using Microsoft.EntityFrameworkCore;
using WebApi1.Models;

namespace WebApi1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}