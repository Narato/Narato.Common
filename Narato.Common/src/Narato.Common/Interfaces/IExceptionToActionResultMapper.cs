using Microsoft.AspNetCore.Mvc;
using System;

namespace Narato.Common.Interfaces
{
    public interface IExceptionToActionResultMapper
    {
        IActionResult Map<T>(Exception ex, string absolutePath);
    }
}
