using Logger.Core;
using Logger.Db.Context;
using System;

namespace Logger.Db
{
    public class Logger : IPlugin
    {
        private string _conection;

        public Logger(string conection)
        {
            _conection = conection;
        }

        public void Log(string type, string message)
        {
            try
            {      
                using (var db = new LoggerContext(_conection))
                {
                    var repository = new LoggerRepository(db);
                    repository.AddLog(type, message);              
                }
            }
            catch (Exception ex)
            {
                // ignore
            }            
        }
    }
}
