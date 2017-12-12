using System;
using System.Linq;
using System.Configuration;

namespace LogLogger
{
    class DataConfig
    {
        public static Format filetype;
        public static string path;
        public static LogLevel loglevel;

        public DataConfig()
        {
            path = ConfigurationManager.AppSettings["filelocation"];
            filetype = stringToFormat(ConfigurationManager.AppSettings["filetype"]);
            loglevel = stringToEnum(ConfigurationManager.AppSettings["configlevel"]);
        }

        private LogLevel stringToEnum(string logType)
        {
            foreach (
                var level in
                    Enum.GetValues(typeof(LogLevel))
                        .Cast<LogLevel>()
                        .Where(level => level.ToString() == logType))
            {
                return level;
            }
            return default(LogLevel);
        }

        private Format stringToFormat(string formatType)
        {
            foreach (
                var format in
                    Enum.GetValues(typeof(Format))
                        .Cast<Format>()
                        .Where(level => level.ToString() == formatType))
            {
                return format;
            }
            return default(Format);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DataConfig()
        {
            Dispose(false);
        }
    }
}
