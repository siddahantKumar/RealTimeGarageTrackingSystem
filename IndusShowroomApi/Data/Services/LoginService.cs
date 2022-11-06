using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IndusShowroomApi.Data.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepo userRepo;
        private readonly IPageRoutesRepo pageRoutesRepo;
        private readonly IUserTypeRepo userTypeRepo;
        private readonly string key;

        public LoginService(IUserRepo userRepo, IPageRoutesRepo pageRoutesRepo, IUserTypeRepo userTypeRepo)
        {
            this.userRepo = userRepo;
            this.pageRoutesRepo = pageRoutesRepo;
            this.userTypeRepo = userTypeRepo;
            key = "This is my test key";
        }

        public bool InsertUser(User user)
        {
            userRepo.InsertUser(user);
            if (userRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            foreach (var item in userRepo.GetUsers())
            {
                if (item.IsDelete != true)
                {
                    users.Add(item);
                }
            }
            return users;
        }

        public bool InsertPageRoutes(List<Page_Routes> page_Routes)
        {
            foreach (var item in page_Routes)
            {
                pageRoutesRepo.InsertPageRoutes(item);

            }

            if (pageRoutesRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }
        public Tuple<bool, int> InsertUserType(User_Type user_Type)
        {
            userTypeRepo.InsertUserType(user_Type);
            if (userTypeRepo.SaveChanges())
            {
                return Tuple.Create(true, userTypeRepo.GetUserNewId());
            }
            return Tuple.Create(false, 0);
        }

        public List<UserTypeRoutes> GetUserTypeRoutes()
        {
            List<UserTypeRoutes> userTypeRoutes = new List<UserTypeRoutes>();

            foreach (var item in userTypeRepo.GetUser_Types())
            {
                if (!item.IsDelete)
                {
                    userTypeRoutes.Add(new UserTypeRoutes
                    {
                        User_Type = item,
                        Page_Routes = pageRoutesRepo.GetPage_RoutesId(item.USER_TYPE_ID)
                    });
                }
            }
            return userTypeRoutes;
        }

        public List<User_Type> GetUser_Types()
        {
            List<User_Type> user_Types = new List<User_Type>();
            foreach (var item in userTypeRepo.GetUser_Types())
            {
                if (!item.IsDelete)
                    user_Types.Add(item);
            }
            return user_Types;
        }

        public string AuthenticateUser(User user)
        {
            foreach (var item in GetUsers())
            {
                if ((item.username == user.username) && (item.password == user.password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.username) // Generate a claim related to my user username
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature
                            )

                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
            }
            return null;
        }
    }
}
