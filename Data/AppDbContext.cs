using Microsoft.EntityFrameworkCore;
using Udemy.DatingApp.Web.Entity;

namespace Udemy.DatingApp.Web.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set;}
}
