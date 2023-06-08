using AutoMapper;
using SocialNetwork_BLL.Models;
using SocialNetwork_DAL.Entities;
using System.Linq;


namespace SocialNetwork_BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Chat, ChatModel>()
                .ForMember(p => p.MessagesIds, c => c.MapFrom(chat => chat.Messages.Select(x => x.Id)))
                .ForMember(p => p.FirstUserId, c => c.MapFrom(chat => chat.FirstUserId))
                .ForMember(p => p.SecondUserId, c => c.MapFrom(chat => chat.SecondUserId))
                .ReverseMap();

            CreateMap<Message, MessageModel>().ReverseMap();

            CreateMap<Message, MessageAdminModel>()
                .ForMember(p => p.AuthorName, c => c.MapFrom(message => message.Author.UserName))
                .ForMember(p => p.ChatName, c => c.MapFrom(message => message.Chat.FirstUser.UserName + message.Chat.SecondUser.UserName))
                .ReverseMap();

            CreateMap<Post, PostModel>()
                .ForMember(p => p.UserName, c => c.MapFrom(user => user.User.UserName))
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(p => p.UserProfileId, c => c.MapFrom(user => user.UserProfile.Id))
                .ForMember(p => p.PostsIds, c => c.MapFrom(user => user.Posts.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<UserProfile, UserProfileModel>()
                .ReverseMap();

            CreateMap<BloggerSubscriber, BloggerSubscriberModel>()
                .ForMember(p => p.BloggerUserName, c => c.MapFrom(blogServ => blogServ.Blogger.UserName))
                .ForMember(p => p.SubscriberUserName, c => c.MapFrom(blogServ => blogServ.Subscriber.UserName))
                .ReverseMap();

            CreateMap<UserInterests, UserInterestModel>()
                .ForMember(p => p.InterestId, c => c.MapFrom(x => x.InterestId))
                .ForMember(p => p.UserName, c => c.MapFrom(x => x.User.UserName))
                .ReverseMap();

            CreateMap<UserInterests, InterestModel>()
                .ForMember(p => p.Id, c => c.MapFrom(x => x.InterestId))
                .ForMember(p => p.Name, c => c.MapFrom(x => x.Interest.Name));

            CreateMap<Interest, InterestModel>()
                .ForMember(p => p.Id, c => c.MapFrom(x => x.Id))
                .ForMember(p => p.Name, c => c.MapFrom(x => x.Name))
                .ReverseMap();
        }
    }
}
