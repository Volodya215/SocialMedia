using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    /// <summary>
    /// Contains data about user post
    /// </summary>
    public class Post : BaseEntity
    {
        /// <summary>
        /// The topic of the post 
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Topic { get; set; }
        /// <summary>
        /// Posting time 
        /// </summary>
        public DateTime DateOfPost { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
