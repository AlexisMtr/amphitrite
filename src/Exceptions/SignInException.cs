using System;

namespace Amphitrite.Exceptions
{
    public class SignInException : Exception
    {
        public SignInException()
            : base() { }

        public SignInException(string message)
            : base(message) { }

        public SignInException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
