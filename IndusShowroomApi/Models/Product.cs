using System;
using System.ComponentModel.DataAnnotations;


namespace IndusShowroomApi.Models
{
    public class Product
    {
        public Product(){
            PRODUCT_BRAND_ID = 0;
            PRODUCT_CAT_ID = 0;
            ProductTitle = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }

        [Key]
        public int PRODUCT_ID { get; set; }

        public int PRODUCT_BRAND_ID { get; set; } // <---------- FK of carCategory

        public int PRODUCT_CAT_ID { get; set; }

        [MaxLength(100)]
        public string ProductTitle { get; set; }

        [MaxLength(100)]
        public string  CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }
        
        public DateTime? DeleteDate { get; set; }
    }
}