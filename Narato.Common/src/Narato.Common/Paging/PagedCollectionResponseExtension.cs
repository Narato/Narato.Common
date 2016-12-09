using Narato.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Narato.Common.Paging
{
    public static class PagedCollectionResponseExtension
    {
        public static PagedCollectionResponse<IEnumerable<T>> ToPagedCollectionResponse<T>(this IEnumerable<T> source, int page, int pageSize, int totalCount)
        {
            var response = new PagedCollectionResponse<IEnumerable<T>>();
            response.Data = source;
            response.Skip = (page - 1) * pageSize;
            response.Take = source.Count();
            response.Total = totalCount;
            return response;
        }
    }
}
