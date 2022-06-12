using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }

        string EncryptPassword(string password);

        ResponseModel<RegisterModel> Register(RegisterModel userData);

        ResponseModel<LoginModel> Login(LoginModel userdata);

        ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData);

        Task<string> ForgotPassword(string email);
    }
}