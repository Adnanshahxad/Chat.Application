using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

internal sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable(nameof(Chat));
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.Message).HasMaxLength(500);
        builder.Property(c => c.DateTime);
        builder.Property(c => c.ChatRoom).HasDefaultValue("Default").HasMaxLength(50);
        builder.Property(c => c.UserName).IsRequired().HasMaxLength(100);
    }
}