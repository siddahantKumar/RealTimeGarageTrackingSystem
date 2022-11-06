using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndusShowroomApi.Models
{
    public class Instalment_Details
    {
        public Instalment_Details()
        {
            CreateDate = DateTime.Now;
            IsDelete = false;
            IsDue = false;
            IsActive = true;
            Narration = "-";
        }

        [Key]
        public int IND_ID { get; set; }

        public int IN_ID { get; set; } // <------------ FK of installement master

        public int TM_ID { get; set; } // <------------ FK of transaction master 
    
        public double Amount { get; set; }

        public string Narration { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime TransactionDate { get; set; }
        
        public bool IsDue { get; set; }
        
        public bool IsActive { get; set; }
        
        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
