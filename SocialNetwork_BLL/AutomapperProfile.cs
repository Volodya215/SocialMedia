using AutoMapper;
using SocialNetwork_BLL.Models;
using SocialNetwork_DAL.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

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

            CreateMap<Post, PostModel>().ReverseMap();

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
        }
    }
}
