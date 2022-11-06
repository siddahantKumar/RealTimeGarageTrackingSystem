using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IUserRepo
    {
        void InsertUser(User user);
        List<User> GetUsers();
        bool SaveChanges();
    }
}
