using System.ComponentModel.DataAnnotations;

namespace SocialNetwork_DAL.Entities
{
    public class Interest : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
