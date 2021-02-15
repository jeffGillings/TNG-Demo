namespace ToneAnalyzerFunction.Options
{
    using System;

    public class CommentClientOptions
    {
        public const string CommentClient = "CommentClient";

        public string EndPoint { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
