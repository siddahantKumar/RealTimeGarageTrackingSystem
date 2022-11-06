using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Purchase_Transaction_Log
    {
        public Purchase_Transaction_Log()
        {
            ChassisNumber = "-";
            Cplc = false;
            BookNumber = "-";
            RegistrationNum = "-";
            EngineNumber = "-";
            Keys = 0;
            File = "-";
            RunningPage = "-";
            NumberPlate = "-";
            Description = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int PTL_ID { get; set; }

        public int PURCHASE_LINE_ID { get; set; }

        public int ITEM_ID { get; set; }

        [MaxLength(100)]
        public string ChassisNumber { get; set; }

        public DateTime Tax { get; set; }

        public bool? Cplc { get; set; }

        [MaxLength(100)]
        public string RegistrationNum { get; set; }

        [MaxLength(100)]
        public string BookNumber { get; set; }

        [MaxLength(100)]
        public string EngineNumber { get; set; }

        public int Keys { get; set; }

        [MaxLength(100)]
        public string File { get; set; }

        [MaxLength(100)]
        public string RunningPage { get; set; }

        [MaxLength(100)]
        public string NumberPlate { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string CreateBy { get; set; }

        [MaxLength(100)]
        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
