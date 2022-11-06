using AutoMapper;
using IndusShowroomApi.Data.Services;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    [Authorize]
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IMapper mapper;

        public LoginController(ILoginService loginService, IMapper mapper)
        {
            this.loginService = loginService;
            this.mapper = mapper;
        }


        [HttpPost("insertusertyperoutes")]
        public ActionResult<bool> InsertUserTypeRoutes(UserTypeRoutes userTypeRoutes)
        {
            Tuple<bool, int> tuple = loginService.InsertUserType(userTypeRoutes.User_Type);
            List<Page_Routes> page_Routes = userTypeRoutes.Page_Routes;

            if (tuple.Item1)
            {
                foreach (var item in page_Routes)
                {
                    item.USER_TYPE_ID = tuple.Item2;
                }

                if (loginService.InsertPageRoutes(page_Routes))
                    return Ok(true);
                return StatusCode(409, userTypeRoutes);
            }
            return StatusCode(409, userTypeRoutes);
        }

        [HttpPost("insertuser")]
        public ActionResult<bool> InsertUser(User user)
        {
            if (loginService.InsertUser(user))
            {
                return Ok(true);
            }
            return StatusCode(409, user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate(User user)
        {
            string token = loginService.AuthenticateUser(user);
            if (token != null)
                return Ok(new { token = "Bearer " + token, user = user.username });

            return Unauthorized(); //Not Authorized or Not authenticated
        }

        [HttpGet("getusers")]
        public ActionResult<List<User>> GetUsers()
        {
            var users = loginService.GetUsers();
            if (users.Count > 0)
            {
                return Ok(users);
            }
            return NoContent();
        }

        [HttpGet("getusertyperoutes")]
        public ActionResult<List<UserTypeRoutes>> GetUserTypeRoutes()
        {
            var UserTypeRoutes = loginService.GetUserTypeRoutes();

            if (UserTypeRoutes.Count > 0)
            {
                return Ok(UserTypeRoutes);
            }
            return NoContent();
        }

        [HttpGet("getusertypes")]
        public ActionResult<List<User_TypeDto>> GetUserType()
        {
            var UserTypes = mapper.Map<List<User_TypeDto>>(loginService.GetUser_Types());
            if (UserTypes.Count > 0)
            {
                return Ok(UserTypes);
            }
            return NoContent();
        }

        [HttpGet("validatesession")]
        public ActionResult ValidateSession()
        {
            return Ok();
        }
    }
}
