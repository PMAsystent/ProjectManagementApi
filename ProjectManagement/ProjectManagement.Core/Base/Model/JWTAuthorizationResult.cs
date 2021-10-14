using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Base.Model
{
    public class JWTAuthorizationResult : Result
    {
        internal JWTAuthorizationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        { }

        public string Token { get; set; }


        public static JWTAuthorizationResult Success(string token)
        {
            return new JWTAuthorizationResult(true, new string[] { }) { Token = token };
        }

        new public static JWTAuthorizationResult Failure(IEnumerable<string> errors)
        {
            return new JWTAuthorizationResult(false, errors);
        }
    }
}
