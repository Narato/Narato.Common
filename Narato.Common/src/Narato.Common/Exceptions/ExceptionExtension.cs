using System;

namespace Narato.Common.Exceptions
{
    public static class ExceptionExtension
    {
        private const string TRACKING_GUID_NAME = "Tracking GUID";

        public static Guid AddTrackingGuid(this Exception e)
        {
            var guid = Guid.NewGuid();
            e.Data.Add(TRACKING_GUID_NAME, guid);
            return guid;
        }

        public static Guid GetTrackingGuid(this Exception e)
        {
            if (! e.Data.Contains(TRACKING_GUID_NAME))
            {
                // Tempted to throw an exception here... but that would also be kinda weird, so we'll just return an empty guid
                return Guid.Empty;
            }
            return (Guid)e.Data[TRACKING_GUID_NAME];
        }
    }
}
