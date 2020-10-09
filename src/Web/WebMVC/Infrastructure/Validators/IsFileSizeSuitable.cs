using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebMVC.Services;

namespace WebMVC.Infrastructure.Validators
{
    public class IsFileSizeSuitable : ISpecification<IFormFile>
    {
        private readonly IOptions<AppSettings> options;

        public IsFileSizeSuitable(IOptions<AppSettings> options)
        {
            this.options = options;
        }
        public bool IsSatisfiedBy(IFormFile file) =>  options.Value.FileSizeLimit >= file.Length;
    }
}
