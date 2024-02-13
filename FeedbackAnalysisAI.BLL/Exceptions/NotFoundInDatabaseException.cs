using System;

namespace FeedbackAnalysisAI.BLL.Exceptions
{
    public class NotFoundInDatabaseException : Exception
    {
        private const string ExceptionText = "Data wasn't found in database.";

        public NotFoundInDatabaseException()
        : base(ExceptionText)
        { }
    }
}
