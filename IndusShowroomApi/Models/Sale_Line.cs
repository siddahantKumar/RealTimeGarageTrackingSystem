using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Sale_Line
    {
        public Sale_Line()
        {
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int SALE_LINE_ID { get; set; }

        public int SALE_ID { get; set; }

        [MaxLength(50)]
        public string ChassisNumber { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
