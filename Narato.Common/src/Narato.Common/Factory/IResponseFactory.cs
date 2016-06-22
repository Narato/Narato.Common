using Narato.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Narato.Common.Factory
{
    public interface IResponseFactory
    {
        IActionResult CreateGetResponse<T>(Func<T> callback, string absolutePath);
        IActionResult CreateGetResponseForCollection<T>(Func<IEnumerable<T>> callback, string absolutePath);
        IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath, string routeName, object routeValues);
        IActionResult CreateMissingParam(MissingParam missingParam);
        IActionResult CreateMissingParam(List<MissingParam> missingParams);
    }
}
