using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlUserRepo : IUserRepo
    {
        private readonly DatabaseContext context;

        public SqlUserRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }
        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
