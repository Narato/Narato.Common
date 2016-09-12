using Narato.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narato.Common.Interfaces
{
    public interface IExceptionHandler
    {
        T PrettifyExceptions<T>(Func<T> callback);
        Task<T> PrettifyExceptionsAsync<T>(Func<Task<T>> callback);
        PagedCollectionResponse<IEnumerable<T>> PrettifyExceptions<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback);
    }
}
