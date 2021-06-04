﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class Message : BaseEntity
    {
        [Required]
        public string AuthorId { get; set; }
        [Required]
        public int ChatId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime MessageTime { get; set; }

        public Chat Chat { get; set; }

        public User Author { get; set; }
    }
}
