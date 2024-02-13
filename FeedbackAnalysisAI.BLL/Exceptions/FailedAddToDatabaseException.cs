using System;

namespace FeedbackAnalysisAI.BLL.Exceptions
{
    public class FailedAddToDatabaseException : Exception
    {
        private const string FailedAdd = "Failed add to database. Please try do this later.";

        public FailedAddToDatabaseException()
        : base(FailedAdd)
        { }
    }
}
