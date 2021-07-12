using Microsoft.EntityFrameworkCore;
namespace exam.Models
{
    
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activites> Activitess { get; set; }
        public DbSet<Patricipanats> Patricipanatss { get; set; }



    }
}