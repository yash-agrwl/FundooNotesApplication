using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Interface
{
    public interface IUserManager
    {
        IConfiguration Configuration { get; }

        string Register(RegisterModel userdata);
    }
}