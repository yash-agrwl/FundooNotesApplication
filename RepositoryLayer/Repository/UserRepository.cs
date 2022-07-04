using CommonLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
                var result = new ResponseModel<RegisterModel>();
                var validEmail = this.fundooContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                result.Data = userData;
                if (validEmail == null)
                {
                    // Encrypt password with MD5.
                    userData.Password = EncryptPassword(userData.Password);
                    // Add data to the database using FundooContext.
                    this.fundooContext.Add(userData);
                    // Saving data in database.
                    this.fundooContext.SaveChanges();

                    result.Status = true;
                    result.Message = "Registration Successful";
                    return result;
                }

                userData.UserID = validEmail.UserID;
                result.Message = "Email Id Already Exists";
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
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

        public ResponseModel<RegisterModel> Login(LoginModel userData)
        {
            try
            {
                var result = new ResponseModel<RegisterModel>();
                var validEmail = this.fundooContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                result.Data = validEmail;

                if (validEmail != null)
                {
                    userData.Password = EncryptPassword(userData.Password);

                    if (userData.Password == validEmail.Password)
                    {
                        SetRedisCache(validEmail);

                        result.Status = true;
                        result.Message = "Login Successful";
                        return result;
                    }

                    result.Message = "Incorrect Password";
                    return result;
                }

                result.Message = "Email not Registered";
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        private void SetRedisCache(RegisterModel user)
        {
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase database = connectionMultiplexer.GetDatabase();
            database.StringSet(key: "FirstName", user.FirstName);
            database.StringSet(key: "LastName", user.LastName);
            database.StringSet(key: "UserId", user.UserID);
            database.StringSet(key: "Email", user.Email);
        }

        public ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData)
        {
            try
            {
                var result = new ResponseModel<ResetPasswordModel>();
                var validEmail = this.fundooContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                result.Data = userData;

                if (validEmail != null)
                {
                    validEmail.Password = EncryptPassword(userData.Password);

                    // Add data to the database using userContext.
                    this.fundooContext.Update(validEmail);
                    // Saving data in database.
                    this.fundooContext.SaveChanges();

                    result.Status = true;
                    result.Message = "Password Reset Successful";
                    return result;
                }
                
                result.Message = "Wrong Email Entered";
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<string> ForgotPassword(string email)
        {
            try
            {
                var validEmail = this.fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();

                if (validEmail != null)
                {
                    this.SendMSMQ("Link for resetting the password");
                    string linkToBeSend = this.ReceiveMSMQ();
                    this.SendMailUsingSMTP(email, linkToBeSend);
                    return Task.FromResult("Email Sent Successfully");
                }

                return Task.FromResult("Email Not Registered");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SendMSMQ(string url)
        {
            MessageQueue messageQueue = this.QueueDetail();
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            messageQueue.Label = "url link";
            messageQueue.Send(message);
        }

        private MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }

            return messageQueue;
        }

        private string ReceiveMSMQ()
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();
            return receiveMsg.Body.ToString();
        }

        private void SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();          
            mailMessage.From = new MailAddress(this.Configuration["Credentials:EmailId"]);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset your password for fundoo Application";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(this.Configuration["Credentials:EmailId"],
                                                       this.Configuration["Credentials:EmailPassword"]);
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mailMessage);
        }
    }
}
