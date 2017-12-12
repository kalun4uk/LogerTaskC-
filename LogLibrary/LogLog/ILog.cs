using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogLog
{
    public interface ILog
    {
        void Debug(string message, string name);
        void Info(string message, string name);
        void Warning(string message, string name);
        void Error(string message, string name);
    }
}
