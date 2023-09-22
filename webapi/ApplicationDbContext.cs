using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Memo> Memos { get; set; }
    }
}
