using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using TelegramClone.DataAccess.Entites;

namespace TelegramClone.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set;} 

        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }


        public ApplicationDbContext() => Database.EnsureCreated();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Username).IsUnique(); 
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique(); 

            modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Contacts) // Один пользователь может иметь много контактов
            .WithOne(c => c.User) // Один контакт связан с одним пользователем
            .HasForeignKey(c => c.UserId); // Внешний ключ на UserEntity
            //.OnDelete(DeleteBehavior.Cascade); // Удаление пользователя удаляет все его контакты

            // Связь один-ко-многим между Chat и ChatMessage
            modelBuilder.Entity<ChatEntity>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь один-к-одному между Contact и Chat
            modelBuilder.Entity<ContactEntity>()
                .HasOne(c => c.Chat)
                .WithMany() // Один чат связан с одним контактом
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
