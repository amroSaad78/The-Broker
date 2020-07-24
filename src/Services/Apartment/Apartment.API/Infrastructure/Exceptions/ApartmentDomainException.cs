using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Exceptions
{
    public class ApartmentDomainException: Exception
    {
        public ApartmentDomainException()
        { }

        public ApartmentDomainException(string message)
            : base(message)
        { }

        public ApartmentDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
