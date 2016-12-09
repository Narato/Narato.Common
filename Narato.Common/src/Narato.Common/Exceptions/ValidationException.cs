using Narato.Common.Models;
using System.Collections.Generic;

namespace Narato.Common.Exceptions
{
    public class ValidationException : ExceptionWithFeedback
    {
        public ValidationException(List<FeedbackItem> feedback) : base(feedback) { }
        public ValidationException(FeedbackItem feedback) : this(new List<FeedbackItem>() { feedback }) { }
    }
}
