using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure.Filters
{
    public class CheckUploadedFile<T> : IAsyncActionFilter where T : IOptions<AppSettings>
    {
        private readonly IOptions<AppSettings> options;

        public CheckUploadedFile(IOptions<AppSettings> options)
        {
            this.options = options;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IFormFile);
            if (param.Value != null)
            {
                var file = (IFormFile)context.ActionArguments["file"];
                string fileName = file.FileName;
                if (fileName.Length > 50 || String.IsNullOrEmpty(fileName))
                {
                    context.ModelState.AddModelError("error", "Invalid filename.");
                }

                var ext = Path.GetExtension(fileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(ext) || !options.Value.PermittedExtensions.Contains(ext))
                {
                    context.ModelState.AddModelError("error", "Unsupported file extension.");
                }

                if (file.Length > options.Value.FileSizeLimit)
                {
                    context.ModelState.AddModelError("error", "File size should not exceed 2M.");
                }
            }
            await next();
        }
    }
}
