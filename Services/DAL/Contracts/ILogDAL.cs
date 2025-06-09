using Services.DOMAIN;
using System;
using System.Collections.Generic;

namespace Services.DAL.Contracts
{
    public interface ILogDAL
    {
        void AddLog(LogEntry entry);
        List<LogEntry> GetLogs(DateTime? from, DateTime? to, TraceLevel? level, string message);
    }
}
