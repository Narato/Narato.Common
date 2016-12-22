using Microsoft.AspNetCore.Mvc;
using Narato.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narato.Common.Factory
{
    public interface IResponseFactory
    {
        IActionResult CreateGetResponse<T>(Func<T> callback, string absolutePath);
        Task<IActionResult> CreateGetResponseAsync<T>(Func<Task<T>> callback, string absolutePath);
        IActionResult CreateGetResponseForCollection<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback, string absolutePath);
        Task<IActionResult> CreateGetResponseForCollectionAsync<T>(Func<Task<PagedCollectionResponse<IEnumerable<T>>>> callback, string absolutePath);
        IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath, string routeName, object routeValues, List<RouteValuesIdentifierPair> routeValueIdentifierPairs);
        IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath);
        Task<IActionResult> CreatePostResponseAsync<T>(Func<Task<T>> callback, string absolutePath, string routeName, object routeValues, List<RouteValuesIdentifierPair> routeValueIdentifierPairs);
        Task<IActionResult> CreatePostResponseAsync<T>(Func<Task<T>> callback, string absolutePath);
        IActionResult CreateDeleteResponse(Action callback, string absolutePath);
        Task<IActionResult> CreateDeleteResponseAsync(Func<Task> callback, string absolutePath);
        IActionResult CreateMissingParam(MissingParam missingParam);
        IActionResult CreateMissingParam(List<MissingParam> missingParams);
        IActionResult CreatePutResponse<T>(Func<T> callback, string absolutePath);
        Task<IActionResult> CreatePutResponseAsync<T>(Func<Task<T>> callback, string absolutePath);
    }
}