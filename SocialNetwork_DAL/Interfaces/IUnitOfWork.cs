using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserProfileRepository UserProfileRepository { get; }
        IPostRepository PostRepository { get; }
        IMessageRepository MessageRepository { get; }
        IChatRepository ChatRepository { get; }
        IBloggerSubscriberRepository BloggerSubscriberRepository { get; }

        Task<int> SaveAsync();
    }
}
