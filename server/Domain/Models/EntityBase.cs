namespace Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class EntityBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
