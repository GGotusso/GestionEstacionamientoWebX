using Services.DAL.Contracts;
using Services.DAL.FactoryServices;
using Services.DOMAIN;
using System;
using System.Collections.Generic;

namespace Services.Logic
{
    public class LogLogic
    {
        private readonly ILogDAL _logRepository;

        public LogLogic()
        {
            _logRepository = FactoryDAL.LogRepository;
        }

        public void AddLog(LogEntry entry)
        {
            _logRepository.AddLog(entry);
        }

        public List<LogEntry> GetLogs(DateTime? from, DateTime? to, TraceLevel? level, string message)
        {
            return _logRepository.GetLogs(from, to, level, message);
        }
    }
}
