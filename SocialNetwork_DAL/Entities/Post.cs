using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Topic { get; set; }
        public DateTime DateOfPost { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
