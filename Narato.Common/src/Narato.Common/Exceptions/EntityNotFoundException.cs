using System;

namespace Narato.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public bool MessageSet { get; set; }

        public EntityNotFoundException()
        {
            MessageSet = false;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
            MessageSet = true;
        }
    }
}
