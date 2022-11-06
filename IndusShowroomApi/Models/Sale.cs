using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Sale
    {
        public Sale()
        {
            CUSTOMER_ID = 0;
            S_Amount = 0;
            S_Discount = 0;
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int SALE_ID { get; set; }

        public int CUSTOMER_ID { get; set; }

        public double S_Amount { get; set; }

        public double S_Discount { get; set; }

        public DateTime TransactionDate { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
