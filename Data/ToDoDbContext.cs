using Microsoft.EntityFrameworkCore;
using ToDoAuth.Models;

namespace ToDoAuth.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext (DbContextOptions options) :  base(options)
        {
        }

        public DbSet<ToDo> ToDotbl { get; set; }
        public DbSet<ToDoUser> ToDoUsers { get; set; }
    }
}