using Narato.Common.Exceptions;
using Narato.Common.Models;
using System.Collections.Generic;
using Xunit;

namespace Narato.Common.Test
{
    public class ExceptionWithFeedbackTest
    {
        [Fact]
        public void ConstructorWithListGivesListOfFeedback()
        {
            var exception = new ExceptionWithFeedback(new List<FeedbackItem>());
            Assert.NotNull(exception.Feedback);
        }

        [Fact]
        public void ConstructorWithSingleFeedbackItemGivesListOfFeedback()
        {
            var exception = new ExceptionWithFeedback(new FeedbackItem());
            Assert.NotNull(exception.Feedback);
        }
    }
}
