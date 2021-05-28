using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Interfaces;
using SocialNetwork_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialNetworkContext _context;
        public IUserProfileRepository UserProfileRepository { get; private set; }

        public IPostRepository PostRepository { get; private set; }

        public IMessageRepository MessageRepository { get; private set; }

        public IChatRepository ChatRepository { get; private set; }

        public UnitOfWork(DbContextOptions option)
        {
            _context = new SocialNetworkContext(option);
            UserProfileRepository = new UserProfileRepository(_context);
            PostRepository = new PostRepository(_context);
            MessageRepository = new MessageRepository(_context);
            ChatRepository = new ChatRepository(_context);
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
