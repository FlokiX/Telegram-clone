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

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email).IsUnique();

            // Связь один-ко-многим между UserEntity и ContactEntity
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Contacts) 
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            // Связь один-ко-многим между ChatEntity и ChatMessageEntity
            modelBuilder.Entity<ChatEntity>()
                .HasMany(c => c.Messages) 
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId) 
                .OnDelete(DeleteBehavior.Cascade); 

            // Связь один-к-одному между ContactEntity и ChatEntity
            modelBuilder.Entity<ContactEntity>()
                .HasOne(c => c.Chat) 
                .WithOne()
                .HasForeignKey<ContactEntity>(c => c.ChatId)
                .OnDelete(DeleteBehavior.Cascade); 
        }

    }


}
