using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] RegisterModel userdata)
        {
            try
            {
                string result = this.manager.Register(userdata);
                if (result.Equals("Registration Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = "Session Data" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel userData)
        {
            try
            {
                string result = this.manager.Login(userData);
                if (result.Equals("Login Successful"))
                {
                    return this.Ok(new { Status = true, Message = result, Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
