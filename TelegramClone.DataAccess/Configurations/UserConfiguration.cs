using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramClone.DataAccess.Entites;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        // Настройка имени таблицы
        builder.ToTable("Users");

        // Настройка ключа
        builder.HasKey(u => u.Id);

        // Настройка уникальных индексов
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        // Настройка свойств
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(20); // Устанавливаем максимальную длину

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(20); // Устанавливаем максимальную длину

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.DateOfRegistration)
            .IsRequired();
    }
}
