using System;

namespace Owners.API.Infrastructure.Exceptions
{
    public class OwnersDomainException: Exception
    {
        public OwnersDomainException()
        { }

        public OwnersDomainException(string message)
            : base(message)
        { }

        public OwnersDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
