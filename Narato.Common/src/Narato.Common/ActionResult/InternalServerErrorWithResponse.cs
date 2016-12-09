using Microsoft.AspNetCore.Mvc;
using Narato.Common.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Narato.Common.ActionResult
{
    public class InternalServerErrorWithResponse : IActionResult
    {

        private readonly Response _response;

        public InternalServerErrorWithResponse(Response response)
        {
            _response = response;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var responseContent = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(_response));
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
            return context.HttpContext.Response.Body.WriteAsync(responseContent, 0, responseContent.Length);
        }
    }
}
