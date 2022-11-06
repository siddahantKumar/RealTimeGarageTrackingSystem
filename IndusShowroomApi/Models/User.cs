using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class User
    {
        public User()
        {
            IsDelete = false;
            CreateDate = DateTime.Now;
        }
        [Key]
        public int USER_ID { get; set; }
        public int? USER_TYPE_ID { get; set; }
        [MaxLength(100)]
        public string username { get; set; }
        [MaxLength(100)]
        public string password { get; set; }
        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
