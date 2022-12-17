using Microsoft.EntityFrameworkCore;
using FileObmennik.Models;
namespace FileObmennik.Data
{
    public class ApplicationDbContext:DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
       {

       }
       public DbSet<FileObmennik.Models.File> File { get; set; }
    }
}
