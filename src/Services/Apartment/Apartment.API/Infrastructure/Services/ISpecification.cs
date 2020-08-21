using Microsoft.Extensions.Options;

namespace Apartment.API.Infrastructure.Services
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T file);
    }
}
