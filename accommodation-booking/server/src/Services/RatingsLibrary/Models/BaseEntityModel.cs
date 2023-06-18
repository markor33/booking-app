using System.ComponentModel.DataAnnotations;

namespace Ratings.Models
{
    public class BaseEntityModel
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}

