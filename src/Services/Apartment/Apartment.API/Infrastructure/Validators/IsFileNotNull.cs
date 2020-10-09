using Apartment.API.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace Apartment.API.Infrastructure.Validators
{
    public class IsFileNotNull : ISpecification<IFormFile>
    {
        public bool IsSatisfiedBy(IFormFile file) => file?.Length > 0;
    }
}
