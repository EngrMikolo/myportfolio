using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace michealogundero.EmailSettings
{
    public interface IMailSender
    {
        Task<string> SendEmail(string name, string message, string subject, string email);
    }
}
