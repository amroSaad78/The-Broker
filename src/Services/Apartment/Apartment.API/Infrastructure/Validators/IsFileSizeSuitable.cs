using Apartment.API.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Apartment.API.Infrastructure.Validators
{
    public class IsFileSizeSuitable : ISpecification<IFormFile>
    {
        private readonly IOptions<ApartmentSettings> options;

        public IsFileSizeSuitable(IOptions<ApartmentSettings> options)
        {
            this.options = options;
        }
        public bool IsSatisfiedBy(IFormFile file) =>  options.Value.FileSizeLimit >= file.Length;
    }
}
