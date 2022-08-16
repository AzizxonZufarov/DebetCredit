using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DebetCredit.Models
{
    public class BalanceModel
    {
        public int ID { get; set; }

        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [DisplayName("Приход / Расход")]
        public string Inout { get; set; }

        [DisplayName("Категория")]
        public string Category{ get; set; }

        [DisplayName("Сумма")]
        public int Summ { get; set; }

        [DisplayName("Комментарии")]
        public string Comment { get; set; }



    }        
    public enum CategoryEnum
        {
            [EnumMember(Value = "Доход")]
            Доход,
            [EnumMember(Value = "Расход")]
            Расход
    }
}