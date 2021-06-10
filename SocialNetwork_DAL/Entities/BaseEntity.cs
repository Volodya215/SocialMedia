using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    /// <summary>
    ///  Base entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Primary key for element in database
        /// </summary>
        public int Id { get; set; }
    }
}
