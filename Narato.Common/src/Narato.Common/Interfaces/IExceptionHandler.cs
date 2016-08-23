using Narato.Common.Models;
using System;
using System.Collections.Generic;

namespace Narato.Common.Interfaces
{
    public interface IExceptionHandler
    {
        T PrettifyExceptions<T>(Func<T> callback);
        PagedCollectionResponse<IEnumerable<T>> PrettifyExceptions<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback);
    }
}
