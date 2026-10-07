using Messenger_Prototype.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger_Prototype.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=messenger.db");
        }
    }
}
