using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Page_Routes
    {
        public Page_Routes()
        {
            IsDelete = false;
            CreateDate = DateTime.Now;
        }
        [Key]
        public int ROUTES_ID { get; set; }
        public int? USER_TYPE_ID { get; set; }
        [MaxLength(100)]
        public string routeName { get; set; }
        [MaxLength(100)]
        public string routeValue { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
