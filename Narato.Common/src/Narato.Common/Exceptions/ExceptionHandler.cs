using Narato.Common.Interfaces;
using Narato.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Narato.Common.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
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
                //log the exception
                throw new ExceptionWithFeedback(new FeedbackItem() { Description = couldNotConnectToDatabse });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> PrettifyExceptions<T>(Func<IEnumerable<T>> callback)
        {
            try
            {
                var returnData = callback();
                return returnData;
            }
            catch (SocketException ex)
            {
                //log the exception
                throw new ExceptionWithFeedback(new FeedbackItem() { Description = couldNotConnectToDatabse });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
