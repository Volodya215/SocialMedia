using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_DAL
{
    public class SocialNetworkContext : IdentityDbContext
    {
        public SocialNetworkContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BloggerSubscriber> BloggerSubscribers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(c => c.UserProfile)
                .WithOne(u => u.User)
                .HasForeignKey<UserProfile>(b => b.UserId);
        }
    }
}
