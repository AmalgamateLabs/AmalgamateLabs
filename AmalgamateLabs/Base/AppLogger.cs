using AmalgamateLabs.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace AmalgamateLabs.Base
{
    public class AppLogger
    {
        public ILogger Logger { get; set; }

        private SQLiteSink _sqliteSink;

        public AppLogger(SQLiteDBContext sqliteDBContext)
        {
            _sqliteSink = new SQLiteSink(sqliteDBContext);

            Logger = new LoggerConfiguration()
                .WriteTo.Sink(_sqliteSink)
                .CreateLogger();
        }
    }

    public class SQLiteSink : ILogEventSink
    {
        private SQLiteDBContext _sqliteDBContext;

        public SQLiteSink(SQLiteDBContext sqliteDBContext)
        {
            _sqliteDBContext = sqliteDBContext;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent != null)
            {
                _sqliteDBContext.Add(new AppException()
                {
                    Date = logEvent.Timestamp.DateTime,
                    Level = logEvent.Level.ToString(),
                    Message = logEvent.MessageTemplate.Text

                });

                _sqliteDBContext.SaveChanges();
            }
            else
            {
                _sqliteDBContext.Add(new AppException()
                {
                    Date = DateTime.Now,
                    Level = LogEventLevel.Error.ToString(),
                    Message = "Exception occured, but unable to access the Log Event."

                });

                _sqliteDBContext.SaveChanges();
            }
        }
    }
}
