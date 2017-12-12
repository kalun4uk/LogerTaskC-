using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
    sealed class CallerMemberNameAttribute : Attribute { }
}

namespace LogLogger
{
    /// <summary>
    /// Choosing the right action due to entered data
    /// </summary>
    class MyLog : ILog
    {
        private InternalLoggers _log = new InternalLoggers();


        public void Debug(string message, [CallerMemberName] string name = null)
        {
            _log.WriteMessage(message, LogLevel.Debug, name, DateTime.Now);
        }

        public void Info(string message, [CallerMemberName] string name = null)
        {
            _log.WriteMessage(message, LogLevel.Info, name, DateTime.Now);
        }

        public void Warning(string message, [CallerMemberName] string name = null)
        {
            _log.WriteMessage(message, LogLevel.Warning, name, DateTime.Now);
        }

        public void Error(string message, [CallerMemberName] string name = null)
        {
            _log.WriteMessage(message, LogLevel.Error, name, DateTime.Now);
        }
    }
}
