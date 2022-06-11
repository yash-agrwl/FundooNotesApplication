using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FundooContext fundooContext;

        public IConfiguration Configuration { get; }

        public UserRepository(IConfiguration configuration, FundooContext fundooContext)
        {
            Configuration = configuration;
            this.fundooContext = fundooContext;
        }

        public ResponseModel<RegisterModel> Register(RegisterModel userData)
        {
            try
            {
                var validEmail = this.fundooContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    // Encrypt password with MD5.
                    userData.Password = EncryptPassword(userData.Password);
                    // Add data to the database using FundooContext.
                    this.fundooContext.Add(userData);
                    // Saving data in database.
                    this.fundooContext.SaveChanges();
                    return new ResponseModel<RegisterModel>()
                    {
                        Status = true,
                        Message = "Registration Successful",
                        Data = userData
                    };
                }
                return new ResponseModel<RegisterModel>()
                {
                    Status = true,
                    Message = "Email Id Already Exists",
                    Data = userData
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string EncryptPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            // Encrypt the given password string into Encrypted data.
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptData = new StringBuilder();
            // Create a new string by using the Encrypted data.
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptData.Append(encrypt[i].ToString());
            }
            return encryptData.ToString();
        }

        public ResponseModel<LoginModel> Login(LoginModel userData)
        {
            try
            {
                var validEmail = this.fundooContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (validEmail != null)
                {
                    userData.Password = EncryptPassword(userData.Password);
                    if (userData.Password == validEmail.Password)
                    {
                        return new ResponseModel<LoginModel>()
                        {
                            Status = true,
                            Message = "Login Successful",
                            Data = userData
                        };
                    }
                    return new ResponseModel<LoginModel>()
                    {
                        Status = false,
                        Message = "Incorrect Password",
                        Data = userData
                    };
                }
                return new ResponseModel<LoginModel>()
                {
                    Status = false,
                    Message = "Email not Registered",
                    Data = userData
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
