using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Core
{
    public interface IPlugin
    {
        void Log(string type, string message);
    }
}
