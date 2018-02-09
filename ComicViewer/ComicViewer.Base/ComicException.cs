using System;
using System.Runtime.Serialization;

namespace ComicViewer.Base
{
    [Serializable]
    public class ComicException : Exception
    {
        public ComicException(string message)
            : base(message)
        {
        }

        public ComicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ComicException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
