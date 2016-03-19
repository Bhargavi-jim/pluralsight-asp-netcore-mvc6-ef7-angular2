using System;
using MyWorld.Services.Interfaces;

namespace MyWorld.Services
{
    public class MailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}