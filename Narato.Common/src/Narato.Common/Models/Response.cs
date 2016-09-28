namespace Narato.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class Response
    {
        public Response()
        {
            Feedback = new List<FeedbackItem>();
            Generation = new Generation();
        }

        public Response(List<FeedbackItem> feedbackItems, string absolutePath, int status) : this()
        {
            Feedback = feedbackItems;
            Self = absolutePath;
            Status = status;
        }

        public Response(FeedbackItem feedbackItem, string absolutePath, int status) : this(new List<FeedbackItem>() { feedbackItem }, absolutePath, status) { }

        public string Self { get; set; }

        // only ignores when 0
        [JsonProperty("skip", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Skip { get; set; }

        // only ignores when 0
        [JsonProperty("take", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Take { get; set; }

        // only ignores when 0
        [JsonProperty("total", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Total { get; set; }

        public Generation Generation { get; set; }

        public List<FeedbackItem> Feedback { get; set; }

        // ignore this if it's not set :-)
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; } // this is the statuscode

        // ignore this if it's not set :-)
        [JsonProperty("identifier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid Identifier { get; set; } // for tracking exceptions
    }


    public class Response<T> : Response
    {
        public Response(T data, List<FeedbackItem> feedbackItems, string abslutePath, int status)
        {
            Data = data;
            Feedback = feedbackItems;
            Self = abslutePath;
            Status = status;
        }

        public Response(T data, FeedbackItem feedbackItem, string abslutePath, int status):this(data, new List<FeedbackItem>() { feedbackItem }, abslutePath, status) {}

        public Response(List<FeedbackItem> feedbackItems, string abslutePath, int status) : this (default(T), feedbackItems, abslutePath, status) {}

        public Response(FeedbackItem feedbackItem, string abslutePath, int status) : this(default(T), new List<FeedbackItem>() { feedbackItem }, abslutePath, status){}

        public Response(T data, string abslutePath, int status) : this(data, (List<FeedbackItem>)null, abslutePath, status){}

        public Response(PagedCollectionResponse<T> dataResponse, string absolutePath, int status)
        {
            Data = dataResponse.Data;
            Total = dataResponse.Total;
            Skip = dataResponse.Skip;
            Take = dataResponse.Take;
            Feedback = null;
            Self = absolutePath;
            Status = status;
        }

        public Response(){}

        public T Data { get; set; }
    }
}