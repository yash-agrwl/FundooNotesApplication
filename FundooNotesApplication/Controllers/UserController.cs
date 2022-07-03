using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel userdata)
        {
            try
            {
                var result = this.manager.Register(userdata);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel userData)
        {
            try
            {
                var result = this.manager.Login(userData);
                string token = this.manager.GenerateToken(result.Data.UserID);

                if (result.Status == true)
                {
                    var data = GetRedisCache();

                    return this.Ok(new
                    {
                        result.Status,
                        result.Message,
                        token,
                        data
                    });
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        private object GetRedisCache()
        {
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase database = connectionMultiplexer.GetDatabase();

            string firstName = database.StringGet("FirstName");
            string lastName = database.StringGet("LastName");
            int userId = Convert.ToInt32(database.StringGet("UserId"));
            string email = database.StringGet("Email");

            return new
            {
                UserID = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel userData)
        {
            try
            {
                var result = this.manager.ResetPassword(userData);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                string result = await this.manager.ForgotPassword(email);

                if (result.Equals("Email Sent Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = email });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
