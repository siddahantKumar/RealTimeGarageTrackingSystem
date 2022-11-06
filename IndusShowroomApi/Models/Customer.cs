using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Customer
    {
        public Customer()
        {
            C_Name = "-";
            C_Cnic = "-";
            C_Address = "-";
            C_PrimaryPhone = "-";
            C_SecondaryPhone = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int CUSTOMER_ID { get; set; }

        [MaxLength(100)]
        public string C_Name { get; set; }

        [MaxLength(50)]
        public string C_Cnic { get; set; }

        [MaxLength(200)]
        public string C_Address { get; set; }

        [MaxLength(20)]
        public string C_PrimaryPhone { get; set; }

        [MaxLength(20)]
        public string C_SecondaryPhone { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
