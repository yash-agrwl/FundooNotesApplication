using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }

        string EncryptPassword(string password);
        string Register(RegisterModel userData);

        string Login(LoginModel userdata);
    }
}