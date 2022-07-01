using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<RegisterModel> Register(RegisterModel userdata);

        ResponseModel<RegisterModel> Login(LoginModel userdata);

        ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData);

        Task<string> ForgotPassword(string email);

        string GenerateToken(int userId);
    }
}