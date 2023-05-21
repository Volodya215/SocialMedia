using System.ComponentModel.DataAnnotations;

namespace SocialNetwork_DAL.Entities
{
    public class UserInterests : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int InterestId { get; set; }

        public User User { get; set; }
        public Interest Interest { get; set; }
    }
}
