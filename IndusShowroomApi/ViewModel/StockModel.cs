using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.ViewModel
{
    public class StockModel
    {
        public int CAR_ID { get; set; }

        public int ITEM_ID { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(100)]
        public string File { get; set; }

        [MaxLength(100)]
        public string RunningPage { get; set; }

        public DateTime Tax { get; set; }

        public bool? Cplc { get; set; }

        [MaxLength(100)]
        public string RegistrationNum { get; set; }

        [MaxLength(100)]
        public string ChassisNumber { get; set; }

        public float Cc { get; set; }

        [MaxLength(100)]
        public string NumberPlate { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public DateTime MfYear { get; set; }

        [MaxLength(100)]
        public string BookNumber { get; set; }

        [MaxLength(100)]
        public string EngineNumber { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
