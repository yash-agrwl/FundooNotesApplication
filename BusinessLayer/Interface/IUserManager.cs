using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Interface
{
    public interface IUserManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<RegisterModel> Register(RegisterModel userdata);

        ResponseModel<LoginModel> Login(LoginModel userdata);

        ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData);
    }
}