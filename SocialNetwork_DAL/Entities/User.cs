using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class User : IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }

        public UserProfile UserProfile { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
