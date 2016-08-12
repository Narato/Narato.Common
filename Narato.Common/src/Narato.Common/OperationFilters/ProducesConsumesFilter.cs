using Swashbuckle.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narato.Common.OperationFilters
{
    public class ProducesConsumesFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //Consumes
            if (operation.Consumes == null)
                operation.Consumes = new List<string>();

            if (!operation.Consumes.Any())
                operation.Consumes.Add("application/json");

            //Produces
            if (operation.Produces == null)
                operation.Produces = new List<string>();

            if (!operation.Produces.Any())
                operation.Produces.Add("application/json");
        }
    }
}
