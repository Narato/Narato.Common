using Microsoft.Extensions.DependencyInjection;
using Narato.Common.Exceptions;
using Narato.Common.Factory;
using Narato.Common.Interfaces;
using Narato.Common.Mappers;

namespace Narato.Common.DependencyInjection
{
    public static class NaratoCommonServiceCollectionExtensions
    {

        public static IServiceCollection AddNaratoCommon(this IServiceCollection services)
        {
            services.AddTransient<IResponseFactory, ResponseFactory>();
            services.AddTransient<IExceptionHandler, ExceptionHandler>();
            services.AddTransient<IExceptionToActionResultMapper, ExceptionToActionResultMapper>();
            return services;
        }
    }
}
