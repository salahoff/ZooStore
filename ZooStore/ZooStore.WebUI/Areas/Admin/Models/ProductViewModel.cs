using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Наименование товара")]
        [Required]
        public string ProductName { get; set; }
        [Display(Name = "Категория")]
        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "Цена")]
        [Required]
        public double Price { get; set; }
        [Display(Name = "Изображение")]
        [Required]
        public string Image { get; set; }
        [Display(Name = "Описание")]
        [Required]
        public string Condition { get; set; }
        [Display(Name = "Скидка")]
        [Required]
        public int Discount { get; set; }
    }
}