using Microsoft.EntityFrameworkCore;

namespace entitytest.Models;  // Ensure this matches your project and folder structure

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employer> Employers { get; set; }

    public DbSet<Job> Jobs { get; set; }
}
