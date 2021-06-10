using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Interfaces;
using SocialNetwork_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL
{
    /// <summary>
    /// The UnitOfWork class provides access to repositories through separate properties and defines a common context for both repositories 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialNetworkContext _context;
        public IUserProfileRepository UserProfileRepository { get; private set; }

        public IPostRepository PostRepository { get; private set; }

        public IMessageRepository MessageRepository { get; private set; }

        public IChatRepository ChatRepository { get; private set; }
        public IBloggerSubscriberRepository BloggerSubscriberRepository { get; private set; }

        /// <summary>
        /// In the constructor, we create an object of context and repositories 
        /// </summary>
        /// <param name="option">The option to be used by DBContext</param>
        public UnitOfWork(DbContextOptions option)
        {
            _context = new SocialNetworkContext(option);
            UserProfileRepository = new UserProfileRepository(_context);
            PostRepository = new PostRepository(_context);
            MessageRepository = new MessageRepository(_context);
            ChatRepository = new ChatRepository(_context);
            BloggerSubscriberRepository = new BloggerSubscriberRepository(_context);
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
