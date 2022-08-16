using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DebetCredit.Models
{
    public class LoginModel
    {

        public int ID { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Введите логин правильно!")]
        public string Login { get; set; }        
        
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введите пароль правильно!")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}