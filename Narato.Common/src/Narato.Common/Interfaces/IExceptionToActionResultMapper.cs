using Microsoft.AspNetCore.Mvc;
using System;

namespace Narato.Common.Interfaces
{
    public interface IExceptionToActionResultMapper
    {
        IActionResult Map(Exception ex, string absolutePath);
    }
}
