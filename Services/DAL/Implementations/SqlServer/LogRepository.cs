using Services.DAL.Contracts;
using Services.DAL.Implementations.SqlServer.Helpers;
using Services.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.DAL.Implementations.SqlServer
{
    public class LogRepository : ILogDAL
    {
        private const string ConnectionName = "Log_Conecction";

        public void AddLog(LogEntry entry)
        {
            SqlHelper.ExecuteNonQuery(ConnectionName,

                new SqlParameter("@Date", entry.Date),
                new SqlParameter("@TraceLevel", entry.Level.ToString()),
                new SqlParameter("@Message", entry.Message));
        }

        public List<LogEntry> GetLogs(DateTime? from, DateTime? to, TraceLevel? level, string message)
        {
            List<LogEntry> logs = new List<LogEntry>();
            StringBuilder query = new StringBuilder("SELECT IdLog, Date, TraceLevel, Message FROM Log WHERE 1=1");
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (from.HasValue)
            {
                query.Append(" AND Date >= @From");
                parameters.Add(new SqlParameter("@From", from.Value));
            }
            if (to.HasValue)
            {
                query.Append(" AND Date <= @To");
                parameters.Add(new SqlParameter("@To", to.Value));
            }
            if (level.HasValue)
            {
                query.Append(" AND TraceLevel = @Level");
                parameters.Add(new SqlParameter("@Level", level.Value.ToString()));
            }
            if (!string.IsNullOrEmpty(message))
            {
                query.Append(" AND Message LIKE @Msg");
                parameters.Add(new SqlParameter("@Msg", "%" + message + "%"));
            }

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionName, query.ToString(), CommandType.Text, parameters.ToArray()))
            {
                while (reader.Read())
                {
                    logs.Add(new LogEntry
                    {

                        Date = reader.GetDateTime(1),
                        Level = (TraceLevel)Enum.Parse(typeof(TraceLevel), reader.GetString(2)),
                        Message = reader.GetString(3)
                    });
                }
            }

            return logs;
        }
    }
}
