using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Inventory
    {
        public Inventory()
        {
            Qty = 0;
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int INV_ID { get; set; }

        public int ITEM_ID { get; set; }

        public int PRODUCT_ID { get; set; }

        public int Qty { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
