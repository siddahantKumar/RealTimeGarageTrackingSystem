using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Instalment_Master
    {
        public Instalment_Master()
        {
            Paid = 0;
            Balance = 0;
            CreateDate = DateTime.Now;
            IsDelete = false;
        }

        [Key]
        public int IN_ID { get; set; }

        public DateTime TransactionDate { get; set; }

        public double Paid { get; set; }

        public double Balance { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
