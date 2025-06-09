using Services.DOMAIN;
using Services.Logic;
using System;
using System.Collections.Generic;

namespace Services.Facade
{
    public static class LogService
    {
        private static readonly LogLogic _logic = new LogLogic();

        public static void WriteLog(TraceLevel level, string message)
        {
            var entry = new LogEntry
            {
                Level = level,
                Message = message
            };
            _logic.AddLog(entry);
        }

        public static List<LogEntry> SearchLogs(DateTime? from, DateTime? to, TraceLevel? level, string message)
        {
            return _logic.GetLogs(from, to, level, message);
        }
    }
}
