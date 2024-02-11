using System.Runtime.Serialization;

namespace PermitRequest.Domain.Exceptions
{
    public class ExceptionMessage : Exception
    {
        public ExceptionMessage() { }

        public ExceptionMessage(string? message) : base(message) { }

        public ExceptionMessage(string? message, Exception? innerException) : base(message, innerException) { }

        protected ExceptionMessage(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
