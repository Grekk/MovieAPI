using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Management;

namespace MoviesRememberServices.Utils
{
    public class LogEvent : WebRequestErrorEvent
    {
        public LogEvent(string message)
            : base(null, null, 100001, new Exception(message))
        {
        }
    }
}
