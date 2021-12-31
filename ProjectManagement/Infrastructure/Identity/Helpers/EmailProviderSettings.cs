﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Helpers
{
    public class EmailProviderSettings
    {
        public string SenderEmail { get; set; }
        public string ConfirmMessage { get; set; }
        public string ResetEmailMessage { get; set; }
        public string ResetPasswordMessage { get; set; }
        public string SendGridApiKey { get; set; }
        public string ClientUrl { get; set; }
        public string ConfirmUrl { get; set; }
        public string ResetEmailUrl { get; set; }
        public string ResetPasswordUrl { get; set; }
    }
}
