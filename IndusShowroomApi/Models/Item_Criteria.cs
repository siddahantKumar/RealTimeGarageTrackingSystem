using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Item_Criteria
    {
        public Item_Criteria()
        {
            Color = "-";
            Model = "-";
            Cc = 0;
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int ITEM_CRIT_ID { get; set; }
        public int ITEM_ID { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        public DateTime MfYear { get; set; }

        public float Cc { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
