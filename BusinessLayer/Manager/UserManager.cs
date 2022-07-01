using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public IConfiguration Configuration { get; }

        public UserManager(IUserRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }

        public ResponseModel<RegisterModel> Register(RegisterModel userdata)
        {
            try
            {
                return this.repository.Register(userdata);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<RegisterModel> Login(LoginModel userdata)
        {
            try
            {
                return this.repository.Login(userdata);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData)
        {
            try
            {
                return this.repository.ResetPassword(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                return await this.repository.ForgotPassword(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GenerateToken(int userId)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["JwtToken:SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
