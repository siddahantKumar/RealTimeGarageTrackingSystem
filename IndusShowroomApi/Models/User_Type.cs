using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class User_Type
    {
        public User_Type()
        {
            IsDelete = false;
            CreateDate = DateTime.Now;
        }
        [Key]
        public int USER_TYPE_ID { get; set; }
        [MaxLength(100)]
        public string userTypeTitle { get; set; }
        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
