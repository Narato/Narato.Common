namespace Narato.Common.Models
{
    public class FeedbackItem
    {
        public FeedbackType Type { get; set; }
        public string Description { get; set; }

        public static FeedbackItem CreateInfoFeedbackItem(string description)
        {
            return new FeedbackItem
            {
                Type = FeedbackType.Info,
                Description = description
            };
        }

        public static FeedbackItem CreateWarningFeedbackItem(string description)
        {
            return new FeedbackItem
            {
                Type = FeedbackType.Warning,
                Description = description
            };
        }

        public static FeedbackItem CreateErrorFeedbackItem(string description)
        {
            return new FeedbackItem
            {
                Type = FeedbackType.Error,
                Description = description
            };
        }

        public static FeedbackItem CreateValidationErrorFeedbackItem(string description)
        {
            return new FeedbackItem
            {
                Type = FeedbackType.ValidationError,
                Description = description
            };
        }
    }
}