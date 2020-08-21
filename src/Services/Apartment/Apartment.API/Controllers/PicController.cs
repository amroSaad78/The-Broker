using Apartment.API.Extensions;
using Apartment.API.Infrastructure;
using Apartment.API.Infrastructure.Services;
using Apartment.API.Infrastructure.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Apartment.API.Controllers
{
    [ApiController]
    public class PicController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<ApartmentSettings> _options;
        private readonly ApartmentContext _apartmentContext;
        private readonly IPicService _picService;
        private readonly IPicServicesHandler _picServicesHandler;

        public PicController(IWebHostEnvironment env,
                                IOptions<ApartmentSettings> options,
                                ApartmentContext apartmentContext,
                                IPicService picService,
                                IPicServicesHandler picServicesHandler)
        {
            _env = env;
            _options = options;
            _apartmentContext = apartmentContext;
            _picService = picService;
            _picServicesHandler = picServicesHandler;
            _picServicesHandler.Subscrib(picService);
        }

        [HttpGet]
        [Route("api/v1/apartment/{id:int}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetImageAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var apartment = await _apartmentContext.Apartment
                .SingleOrDefaultAsync(ci => ci.Id == id);

            if (apartment != null)
            {
                var webRoot = _env.WebRootPath;
                var path = Path.Combine(webRoot, apartment.PictureFileName);

                string imageFileExtension = Path.GetExtension(apartment.PictureFileName);
                string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                var buffer = System.IO.File.ReadAllBytes(path);

                return File(buffer, mimetype);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/v1/apartment/pic")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult SaveImage(IFormFile imgfile)
        {
            var rule = new IsFileNotNull().And(new IsFileSizeSuitable(_options)).And(new IsFileExtntionSuitable()).And(new IsFileSignatureSuitable());
            if (rule.IsSatisfiedBy(imgfile))
            {
                _picService.UploadFile(imgfile);
            }
            else
            { 
                return BadRequest("File size should less than 2Mb and type should be [JPG, JPEG, PNG].");
            }
            return Ok();
        }
        private string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".bmp":
                    mimetype = "image/bmp";
                    break;
                case ".tiff":
                    mimetype = "image/tiff";
                    break;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}
