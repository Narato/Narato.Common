using System;
using System.Collections.Generic;

namespace Narato.Common.Interfaces
{
    public interface IExceptionHandler
    {
        T PrettifyExceptions<T>(Func<T> callback);
        IEnumerable<T> PrettifyExceptions<T>(Func<IEnumerable<T>> callback);
    }
}
