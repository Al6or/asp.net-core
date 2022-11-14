using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Models
{
    public class Order
    {
        /*. Order (заказ)
        - Id (int)
        - Number (nvarchar(max)) *редактируется *используется для фильтрации
        - Date (datetime2(7)) *редактируется *используется для фильтрации
        - ProviderId (int) *редактируется *используется для фильтрации
        */

        public int Id { get; set; }
        [Display(Name = "Номер")]
        public string Number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }



    }
}
