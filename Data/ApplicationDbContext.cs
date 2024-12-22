using TaskManager.Data;
using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Models.Task;
using TaskManager.Models; // User sınıfını tanımlayan ad alanını ekleyin

namespace TaskManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
    } // class ApplicationDbContext için kapanış
} // namespace TaskManager.Data için kapanış