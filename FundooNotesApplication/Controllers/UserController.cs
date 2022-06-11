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
                var result = this.manager.Register(userdata);
                if (result.Status == true)
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest(result);
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
                var result = this.manager.Login(userData);
                if (result.Status == true)
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/resetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel userData)
        {
            try
            {
                var result = this.manager.ResetPassword(userData);
                if (result.Status == true)
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
