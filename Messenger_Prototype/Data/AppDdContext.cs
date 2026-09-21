using Messenger_Prototype.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger_Prototype.Data
{
    internal class AppDdContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatEntity> Chat { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=messenger.db");
        }
    }
}
