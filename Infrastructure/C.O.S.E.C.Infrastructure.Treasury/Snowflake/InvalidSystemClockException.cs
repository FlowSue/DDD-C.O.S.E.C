//系统包
using System;

namespace C.O.S.E.C.Infrastructure.Treasury.Snowflake
{
    public class InvalidSystemClockException : Exception
    {
        public InvalidSystemClockException(string message)
            : base(message)
        {
        }

        public InvalidSystemClockException()
        {
        }

        public InvalidSystemClockException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
