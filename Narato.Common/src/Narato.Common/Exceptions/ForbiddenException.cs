using System;

namespace Narato.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() {}

        public ForbiddenException(string message)
            : base(message) {}
    }
}
