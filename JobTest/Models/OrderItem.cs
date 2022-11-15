using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Models
{
    public class OrderItem
    {       
       /// <summary>
         /*OrderItem (элемент заказа)
         - Id(int)
         - OrderId(int)
         - Name(nvarchar(max)) *редактируется* используется для фильтрации
         - Quantity decimal (18, 3) *редактируется
         - Unit(nvarchar(max)) *редактируется* используется для фильтрации*/
       /// </summary>
        public int Id { get; set; }
      
        [Display(Name = "Наименование")]
       
        public string Name { get; set; }
        [Required(ErrorMessage = "Данно поле должно быть с точкой.")]
        [Display(Name = "Количество")]
        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }
        [Display(Name = "Ед. изм")]
        public string Unit { get; set; }
        [Display(Name = "Номер заказа")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
