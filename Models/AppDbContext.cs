using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//database içeriği oluşturuyoruz
namespace NewNewProject.Models
{
    public class AppDbContext : DbContext //DbContext ctrl+. basarak using'e ekliyoruz
    {
        public AppDbContext(DbContextOptions <AppDbContext> options)
            :base(options)
        {

        }
        //veri tabanında oluşmasını istediğimiz class'ları ekliyoruz
        public DbSet<Category>Categories { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}
