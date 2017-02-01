using Narato.Common.Exceptions;
using Narato.Common.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Narato.Common.Test
{
    public class ExceptionHandlerTest
    {
        [Fact]
        public void ExceptionsThrownInTaskAreConvertedCorrectlyTest()
        {
            var handler = new ExceptionHandler();

            var throwingTask = Task.Run(() =>
            {
                throw new ExceptionWithFeedback(FeedbackItem.CreateErrorFeedbackItem("test"));
            });

            var wrapped = Assert.Throws<AggregateException>(() => throwingTask.Wait());
            Assert.IsType(typeof(ExceptionWithFeedback), wrapped.InnerException);

            var ex = Assert.Throws<ExceptionWithFeedback>(() => handler.PrettifyExceptions(() => throwingTask.Wait()));
            Assert.Equal(ex.Feedback[0].Description, "test");
            
        }
    }
}
