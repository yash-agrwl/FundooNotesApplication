using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> logger;

        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
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
                    this.logger.LogInformation("New User successfully registered with userId: " + userdata.UserID + "\n");
                    return this.Ok(result);
                }

                this.logger.LogError("Unsuccessful attempt at registration for email: '" + userdata.Email + 
                    "' with Error: '" + result.Message + "'\n");
                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogCritical(" Exception Thrown: '" + ex.Message + "'\n");
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

                if (result.Status == true)
                {
                    string token = this.manager.GenerateToken(result.Data.UserID);
                    var data = GetRedisCache();
                    this.logger.LogInformation($"User Successfully LoggedIn with UserId:{result.Data.UserID}\n");

                    return this.Ok(new
                    {
                        result.Status,
                        result.Message,
                        token,
                        data
                    });
                }

                this.logger.LogError($"Unsuccessful attempt to LogIn for email:'{userData.Email}' " +
                    $"with Error:'{result.Message}'\n");

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogCritical(" Exception Thrown: '" + ex.Message + "'\n");

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
