using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Purchase
    {
        public Purchase()
        {
            VENDOR_ID = 0;
            P_Amount = 0;
            P_Discount = 0;
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int PURCHASE_ID { get; set; }

        public int VENDOR_ID { get; set; }

        public double P_Amount { get; set; }

        public double P_Discount { get; set; }

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
