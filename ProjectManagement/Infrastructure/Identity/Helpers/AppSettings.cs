using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Helpers
{
    public class AppSettings
    {
        public string AuthKey { get; set; }
        public long Expire { get; set; }
    }
}
