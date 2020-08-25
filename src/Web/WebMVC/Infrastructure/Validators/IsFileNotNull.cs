using Microsoft.AspNetCore.Http;
using WebMVC.Services;

namespace WebMVC.Infrastructure.Validators
{
    public class IsFileNotNull : ISpecification<IFormFile>
    {
        public bool IsSatisfiedBy(IFormFile file) => file.Length > 0;
    }
}
