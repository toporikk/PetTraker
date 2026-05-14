using System;
using System.ComponentModel.DataAnnotations;

namespace PetTracker.ViewModels
{
    public class PetViewModel
    {
        [Required(ErrorMessage = "Введите кличку питомца")]
        [Display(Name = "Кличка")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Кличка должна быть от 1 до 50 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите тип животного")]
        [Display(Name = "Тип животного")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите дату рождения")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Заметки")]
        [StringLength(500, ErrorMessage = "Заметки не могут быть длиннее 500 символов")]
        public string? Notes { get; set; }
    }
}