﻿using System;

namespace Amphitrite.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base() { }

        public InvalidCredentialsException(string message)
            : base(message) { }

        public InvalidCredentialsException(string message, Exception innerException)
            : base(message, innerException) { }

    }
}
