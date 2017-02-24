using NLog;
using System;
using System.Threading.Tasks;

namespace Narato.Common.Logging
{
    public static class LogExtensions
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public async static Task<T> LogOnException<T>(this Task<T> task)
        {
            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                Logger.Error("early stack: " + ex.StackTrace);
                throw ex;
            }
        }

        public async static Task LogOnException(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Logger.Error("early stack: " + ex.StackTrace);
                throw ex;
            }
        }
    }
}
