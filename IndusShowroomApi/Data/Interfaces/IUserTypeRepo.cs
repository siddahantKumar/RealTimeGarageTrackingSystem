using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IUserTypeRepo
    {
        void InsertUserType(User_Type user_Type);
        int GetUserNewId();
        List<User_Type> GetUser_Types();

        bool SaveChanges();
    }
}
