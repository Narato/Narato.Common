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
        const string couldNotConnectToDatabase = "Could not connect to the database.";

        public T PrettifyExceptions<T>(Func<T> callback)
        {
            try
            {
                var returnData = callback();
                return returnData;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex; // even though HandleException will alway throw an exception, we still need this line to shush the compiler
            }
        }

        public void PrettifyExceptions(Action callback)
        {
            try
            {
                callback();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex; // even though HandleException will alway throw an exception, we still need this line to shush the compiler
            }
        }

        public async Task<T> PrettifyExceptionsAsync<T>(Func<Task<T>> callback)
        {
            try
            {
                var returnData = await callback();
                return returnData;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex; // even though HandleException will alway throw an exception, we still need this line to shush the compiler
            }
        }

        public async Task PrettifyExceptionsAsync(Func<Task> callback)
        {
            try
            {
                await callback();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex; // even though HandleException will alway throw an exception, we still need this line to shush the compiler
            }
        }

        public PagedCollectionResponse<IEnumerable<T>> PrettifyExceptions<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback)
        {
            try
            {
                var returnData = callback();
                return returnData;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex; // even though HandleException will alway throw an exception, we still need this line to shush the compiler
            }
        }

        private void HandleException(Exception ex)
        {
            if (ex is AggregateException && ex.InnerException != null) // Exception was thrown in an async task, which was Resulted. Those are wrapped in an AggregateException
            {
                ex = ex.InnerException;
            }
            ex.AddTrackingGuid();
            Logger.Error(ex, ex.GetTrackingGuid().ToString());
            if (ex is SocketException)
            {
                throw new ExceptionWithFeedback(new FeedbackItem() { Description = couldNotConnectToDatabase });
            }
            if (ex is UnauthorizedAccessException) // this was wrong (mis)use of a system exception. Due to legacy code, we just rethrow it in the correct format
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    throw new UnauthorizedException();
                }
                throw new UnauthorizedException(ex.Message);
            }
            throw ex;
        }
    }
}
