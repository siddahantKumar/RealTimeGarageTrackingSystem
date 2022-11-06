using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndusShowroomApi.Models
{
    public class Transaction_Master
    {
        public Transaction_Master()
        {

            OP_ID = 0;
            IN_ID = 0;
            CreateDate = DateTime.Now;
            IsDelete = false;
        }

        [Key]
        public int TM_ID { get; set; }

        public int OP_ID { get; set; } // <---------------- For operations like inv or other vouchers

        public int IN_ID { get; set; } // <------------ Task id Instalement ID
        public double Amount { get; set; }

        [MaxLength(5)]
        public string Type { get; set; }

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
