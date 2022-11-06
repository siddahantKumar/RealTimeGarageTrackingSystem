using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Vendor
    {
        public Vendor()
        {
            V_Name = "-";
            V_Cnic = "-";
            V_Address = "-";
            V_PrimaryPhone = "-";
            V_SecondaryPhone = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int VENDOR_ID { get; set; }

        [MaxLength(100)]
        public string V_Name { get; set; }

        [MaxLength(50)]
        public string V_Cnic { get; set; }

        [MaxLength(200)]
        public string V_Address { get; set; }

        [MaxLength(20)]
        public string V_PrimaryPhone { get; set; }

        [MaxLength(20)]
        public string V_SecondaryPhone { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
