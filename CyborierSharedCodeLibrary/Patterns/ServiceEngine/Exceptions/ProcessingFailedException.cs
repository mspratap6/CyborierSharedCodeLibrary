using System;
using System.Runtime.Serialization;

namespace Cyborier.Shared.Patterns.ServiceEngine.Exceptions
{
    [Serializable]
    public class ProcessingFailedException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ProcessingFailedException()
        {
        }

        public ProcessingFailedException(string message) : base(message)
        {
        }

        public ProcessingFailedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ProcessingFailedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
