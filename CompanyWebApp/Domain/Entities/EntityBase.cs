using System.ComponentModel.DataAnnotations;

namespace CompanyWebApp.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Название")]
        [MaxLength(200)]
        public string? Title { get; set; }

        private DateTime _dateCreated = DateTime.UtcNow;

        public DateTime DateCreated
        {
            get => _dateCreated;
            set => _dateCreated = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

    }
}
