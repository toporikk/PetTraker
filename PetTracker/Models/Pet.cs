using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetTracker.Models
{
    public class Pet
    {
        //Данные из БД преобразуются в объекты C# класса Pet
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя питомца")]
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

        // Вычисляемый возраст
        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Birthday.Year;
                if (Birthday.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}