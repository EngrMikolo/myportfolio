using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace michealogundero.Models
{
    public class SendEmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
