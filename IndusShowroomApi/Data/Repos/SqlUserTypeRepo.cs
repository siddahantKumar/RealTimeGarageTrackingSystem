using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlUserTypeRepo : IUserTypeRepo
    {
        private readonly DatabaseContext context;

        public SqlUserTypeRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public void InsertUserType(User_Type user_Type)
        {
            context.User_Types.Add(user_Type);
        }

        public int GetUserNewId()
        {

            return context.User_Types.Max(x => x.USER_TYPE_ID);
        }

        public List<User_Type> GetUser_Types()
        {
            return context.User_Types.ToList();
        }


        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
