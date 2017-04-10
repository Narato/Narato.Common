using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Narato.Common.Models
{
    public class ErrorResponse : Response
    {
        // ignore this if it's not set :-)
        [JsonProperty("identifier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid Identifier { get; set; } // for tracking exceptions

        [JsonProperty("title")]
        public string Title { get; set; }

        public ErrorResponse(List<FeedbackItem> feedbackItems, string abslutePath, int status) : base (feedbackItems, abslutePath, status) { }

        public ErrorResponse(FeedbackItem feedbackItem, string abslutePath, int status) : base (new List<FeedbackItem>() { feedbackItem }, abslutePath, status){ }
    }
}
