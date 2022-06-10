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

        public string Register(RegisterModel userdata)
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
    }
}
