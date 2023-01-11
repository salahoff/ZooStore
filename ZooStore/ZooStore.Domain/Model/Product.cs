using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Model
{
    public partial class Product
    {
        public Product()
        {
            this.Reviews = new HashSet<Review>();
        }
        [Required]
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
        [Display(Name = "Картинка")]
        [Required]       
        public string Image { get; set; }
        [Display(Name = "Описание")]
        [Required]     
        public string Condition { get; set; }
        [Display(Name = "Скидка")]
        [Required]  
        public int Discount { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
