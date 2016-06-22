using Narato.Common.Models;
using System;
using System.Collections.Generic;

namespace Narato.Common.Exceptions
{
    public class ExceptionWithFeedback : Exception
    {
        public List<FeedbackItem> Feedback { get; set; }

        public ExceptionWithFeedback(List<FeedbackItem> feedback)
        {
            Feedback = feedback;
        }

        public ExceptionWithFeedback(FeedbackItem feedback) :this (new List<FeedbackItem>() {feedback}){}
    }
}
