using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Db.Context
{
    public class LoggerRepository
    {
        private LoggerContext _context;

        public LoggerRepository(LoggerContext context)
        {
            _context = context;
        }

        public void AddLog(string type, string message)
        {
            _context.Logs.Add(new Log { Date = DateTime.Now, Type = type, Messege = message });
            _context.SaveChanges();
        }
    }
}
