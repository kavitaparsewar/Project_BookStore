using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common_Layer.Models
{
    public class MSMQModel
    {
        MessageQueue message = new MessageQueue();
        public void MsmqSender(string Token)
        {
            message.Path = @".\private$\Token";

            if (!MessageQueue.Exists(message.Path))
            {
                MessageQueue.Create(message.Path);
            }
            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });  //it Creates a asynchronous communication

            message.ReceiveCompleted += Message_ReceiveCompleted;    //Press tab

            message.Send(Token);
            message.BeginReceive();
            message.Close();
        }

        void Message_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = message.EndReceive(e.AsyncResult);

            string token = msg.Body.ToString();

            string subject = " Book Store Password Reset";

            string Body = $"Book Store Reset Password: <a href=http://localhost:4200/resetPassword/{token}> Click Here</a>";
            string jwt = DecodeJwt(token);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("parsewar11@gmail.com ", "Komal@11"), //here valid email and password
                EnableSsl = true,
            };

            // SendAsync(string from, string recipients, string subject, string body, object userToken);
            smtpClient.Send("parsewar11@gmail.com", jwt, subject, Body);

            message.BeginReceive();
        }
        public string DecodeJwt(string token)
        {
            try
            {
                var decodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken((decodeToken));
                var result = jsonToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
