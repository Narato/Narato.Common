namespace Narato.Common.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Response
    {
        public Response()
        {
            Feedback = new List<FeedbackItem>();
            Generation = new Generation();
        }

        public Response(List<FeedbackItem> feedbackItems) : this()
        {
            Feedback = feedbackItems;
        }

        public string Self { get; set; }

        [JsonProperty("skip", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Skip { get; set; }

        [JsonProperty("take", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Take { get; set; }

        public int Total { get; set; }

        public Generation Generation { get; set; }

        public List<FeedbackItem> Feedback { get; set; }
    }


    public class Response<T> : Response
    {
        public Response(T data, List<FeedbackItem> feedbackItems, string abslutePath)
        {
            Data = data;
            Feedback = feedbackItems;
            Self = abslutePath;
        }

        public Response(T data, FeedbackItem feedbackItem, string abslutePath):this(data, new List<FeedbackItem>() { feedbackItem }, abslutePath){}

        public Response(List<FeedbackItem> feedbackItems, string abslutePath) : this (default(T), feedbackItems, abslutePath){}

        public Response(FeedbackItem feedbackItem, string abslutePath) : this(default(T), new List<FeedbackItem>() { feedbackItem }, abslutePath){}

        public Response(T data, string abslutePath) : this(data, (List<FeedbackItem>)null, abslutePath){}

        public Response(){}

        public T Data { get; set; }
    }
}