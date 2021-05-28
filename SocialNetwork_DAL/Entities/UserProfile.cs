using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class UserProfile : BaseEntity
    {
        public string UserId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Work { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Hobby { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string About { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
