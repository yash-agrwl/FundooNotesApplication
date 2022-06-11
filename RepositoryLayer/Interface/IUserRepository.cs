using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }

        string EncryptPassword(string password);

        ResponseModel<RegisterModel> Register(RegisterModel userData);

        ResponseModel<LoginModel> Login(LoginModel userdata);
    }
}