using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTest.Models
{
    public class Provider
    {
        /// <summary>
        /*Provider (поставщик, заполнена изначально, нигде не редактируется)
        - Id(int)
        - Name(nvarchar(max)) *используется для фильтрации*/
        /// </summary>
        [Display(Name = "Номер поставщика")]
        public int Id { get; set; }
        [Display(Name = "Поставщик")]
        public string Name { get; set; }       
    }
}
