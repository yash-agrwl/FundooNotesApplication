using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;

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

        public ResponseModel<LoginModel> Login(LoginModel userdata)
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

        public string ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
