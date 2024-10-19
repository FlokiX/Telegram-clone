using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Entites;

namespace TelegramClone.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set;} // Измените User на UserEntity

        public ApplicationDbContext() => Database.EnsureCreated();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Username).IsUnique(); // Измените User на UserEntity
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique(); // Измените User на UserEntity
        }
    }


}
