using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Transaction_Details
    {
        public Transaction_Details()
        {
            TM_ID = 0;
            ACC_ID = 0;
            Debit = 0;
            Credit = 0;
            ChequeNo = "-";
            Narration = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }

        [Key]
        public int TD_ID { get; set; }

        public int TM_ID { get; set; } // <------------ FK of transaction master

        public int ACC_ID { get; set; } // <--------- FK for accounts

        public double Debit { get; set; }

        public double Credit { get; set; }

        [MaxLength(50)]
        public string ChequeNo { get; set; }

        public string Narration { get; set; }
        [MaxLength(100)]

        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
