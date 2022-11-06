using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Product_Brand
    {

        public Product_Brand()
        {
            ProductBrandTitle = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }

        [Key]
        public int PRODUCT_BRAND_ID { get; set; }

        [MaxLength(100)]
        public string ProductBrandTitle { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}