using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    /// <summary>
    /// User data inherited from IdentityUser
    /// </summary>
    public class User : IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }

        /// <summary>
        /// User profile link 
        /// </summary>
        public UserProfile UserProfile { get; set; }
        /// <summary>
        /// Link to the list of user posts 
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
    }
}
