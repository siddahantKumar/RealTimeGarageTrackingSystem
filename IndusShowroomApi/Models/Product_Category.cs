using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Product_Category
    {
        public Product_Category()
        {
            ProductCategoryTitle = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }

        [Key]
        public int PRODUCT_CAT_ID { get; set; }

        [MaxLength(100)]
        public string ProductCategoryTitle { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
