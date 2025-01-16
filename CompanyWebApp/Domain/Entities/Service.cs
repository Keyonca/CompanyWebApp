    using CompanyWebApp.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    namespace CompanyWebApp.Domain.Entities
    {
        public class Service : EntityBase
        {
            [Display(Name = "Выберите категорию, к которой относится услуга")]
            public int? ServiceCategoryId { get; set; }
            public ServiceCategory? ServiceCategory { get; set; }

            [Display(Name = "Краткое описание")]
            [MaxLength(3_000)]
            public string? DescriptionShort { get; set; }

            [Display(Name = "Описание")]
            [MaxLength(100_000)]
            public string? Description {  get; set; }

            [Display(Name = "Титульная картинка")]
            [MaxLength(300)]
            public string? Photo {  get; set; }

            [Display(Name = "Выберите тип услуги, к которой относится услуга")]
            public int? ServiceTypeId { get; set; }
            public ServiceType? Type { get; set; }
        }
    }
