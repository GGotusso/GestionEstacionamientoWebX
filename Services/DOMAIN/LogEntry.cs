using System;

namespace Services.DOMAIN
{
    public class LogEntry
    {
        public int IdLog { get; set; }
        public DateTime Date { get; set; }
        public TraceLevel Level { get; set; }
        public string Message { get; set; }

        public LogEntry()
        {
            Date = DateTime.Now;
        }
    }

    public enum TraceLevel
    {
        Error = 1,
        Warning = 2,
        Info = 3
    }
}
