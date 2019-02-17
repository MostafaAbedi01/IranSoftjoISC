using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eShopMvc.Models
{
    public class OrderDetailsViewModel
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int ProductID { get; set; }

        public int OrderID { get; set; }

        [Display(Name = "نام کالا")]
        public string ProductTitle { get; set; }

        [Display(Name = "قیمت کالا")]
        [DisplayFormat(DataFormatString = "{0:#,0 ریال}")]
        public int ProductPrice { get; set; }

        [Display(Name = "تعداد")]
        [DisplayFormat(DataFormatString = "{0:#,0 عدد}")]
        public int ProductCount { get; set; }

        [Display(Name = "جمع این ردیف")]
        [DisplayFormat(DataFormatString = "{0:#,0 ریال}")]
        public int RowTotal { get; set; }
    }
}