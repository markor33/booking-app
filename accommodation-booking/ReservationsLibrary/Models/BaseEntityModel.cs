using System.ComponentModel.DataAnnotations;

namespace ReservationsLibrary.Models
{
    public class BaseEntityModel
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}

