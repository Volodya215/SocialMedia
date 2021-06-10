using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_DAL
{
    /// <summary>
    /// The class provides ample opportunities for working with the database: creating queries, tracking changes and saving data to the database.
    /// </summary>
    public class SocialNetworkContext : IdentityDbContext
    {
        /// <summary>
        /// SocialNetworkContext constructor
        /// </summary>
        /// <param name="options">The option to be used by DBContext</param>
        public SocialNetworkContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Table with user data 
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Table with user profile data 
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }
        /// <summary>
        /// Table with posts data 
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        /// <summary>
        /// Table with chats data 
        /// </summary>
        public DbSet<Chat> Chats { get; set; }
        /// <summary>
        /// Table with messages data 
        /// </summary>
        public DbSet<Message> Messages { get; set; }
        /// <summary>
        /// Table with data  about subscribers
        /// </summary>
        public DbSet<BloggerSubscriber> BloggerSubscribers { get; set; }

        /// <summary>
        /// Setting up table interaction 
        /// </summary>
        /// <param name="builder"></param>
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
