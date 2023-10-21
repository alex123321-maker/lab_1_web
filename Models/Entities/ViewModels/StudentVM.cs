using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace lab_1.Models.Entities.ViewModels
{
    public class StudentVM
    {
        [Key]
        public System.Guid ID_студента { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Фамилия { get; set; }
        [Required]
        public string Имя { get; set; }
        [Required]
        public string Отчество { get; set; }
        [Required]
        public bool Пол { get; set; }
        [DisplayName("Адрес проживания")]
        public string Адрес_проживания { get; set; }
        [Required]
        [DisplayName("Дата рождения")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public System.DateTime Дата_рождения { get; set; }
        [DisplayName("Уровень Владения ИЯ")]
        [Required]
        public string Уровень_владения_ИЯ { get; set; }
    }
}