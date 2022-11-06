using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using System;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Services
{
    public interface ILoginService
    {
        bool InsertUser(User user);
        List<User> GetUsers();
        bool InsertPageRoutes(List<Page_Routes> page_Routes);
        Tuple<bool, int> InsertUserType(User_Type user_Type);
        List<UserTypeRoutes> GetUserTypeRoutes();
        List<User_Type> GetUser_Types();
        string AuthenticateUser(User user);
    }
}
