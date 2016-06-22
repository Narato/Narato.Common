using Microsoft.AspNetCore.Mvc;

namespace Narato.Common
{
    public static class ControllerExtension
    {
        public static string GetRequestUri(this Controller controller)
        {
            if( controller.Request != null)
                return controller.Request.Path + controller.Request.QueryString;
            return string.Empty;
        }
    }
}
