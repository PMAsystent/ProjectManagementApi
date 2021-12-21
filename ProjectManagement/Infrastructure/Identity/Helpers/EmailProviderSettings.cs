using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Helpers
{
    public class EmailProviderSettings
    {
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmMessage { get; set; }
        public string ResetEmailMessage { get; set; }
        public string ResetPasswordMessage { get; set; }
        public string SenderServer { get; set; }
    }
}
