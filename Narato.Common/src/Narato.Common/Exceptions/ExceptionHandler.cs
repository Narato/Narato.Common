using Narato.Common.Interfaces;
using Narato.Common.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Narato.Common.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        const string couldNotConnectToDatabse = "Could not connect to the database.";

        public T PrettifyExceptions<T>(Func<T> callback)
        {
            try
            {
                var returnData = callback();
                return returnData;
            }
            catch(SocketException ex)
            {
                Logger.Error(ex);
                //log the exception
                throw new ExceptionWithFeedback(new FeedbackItem() { Description = couldNotConnectToDatabse });
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public async Task<T> PrettifyExceptionsAsync<T>(Func<Task<T>> callback)
        {
            try
            {
                var returnData = await callback();
                return returnData;
            }
            catch (SocketException ex)
            {
                Logger.Error(ex);
                //log the exception
                throw new ExceptionWithFeedback(new FeedbackItem() { Description = couldNotConnectToDatabse });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public PagedCollectionResponse<IEnumerable<T>> PrettifyExceptions<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback)
        {
            try
            {
                var returnData = callback();
                return returnData;
            }
            catch (SocketException ex)
            {
                Logger.Error(ex);
                throw new ExceptionWithFeedback(new FeedbackItem() { Description = couldNotConnectToDatabse });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }
    }
}
