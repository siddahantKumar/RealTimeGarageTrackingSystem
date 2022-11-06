using System;
using System.ComponentModel.DataAnnotations;

namespace IndusShowroomApi.Models
{
    public class Charts_Of_Accounts
    {
        public Charts_Of_Accounts()
        {
            P_ACC_ID = 0;
            Account_Title = "-";
            CreateDate = DateTime.Now;
            IsDelete = false;
        }
        [Key]
        public int ACC_ID { get; set; }  // here it is determine as pprimary kay

        public int P_ACC_ID { get; set; }

        public string Account_Title { get; set; }

        public string CreateBy { get; set; }

        public string DeleteBy { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DeleteDate { get; set; }
    }
}
