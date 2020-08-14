using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Apartment.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apartment.API.Controllers
{
    [ApiController]
    public class PicController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApartmentContext _apartmentContext;

        public PicController(IWebHostEnvironment env,
            ApartmentContext apartmentContext)
        {
            _env = env;
            _apartmentContext = apartmentContext;
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

            var apartment = await _apartmentContext.Rent
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
        [Authorize(Roles ="Admin")]
        [Route("api/v1/apartment/pic")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> SaveImage(IFormFile imgfile)
        {
            //validate file
            // add azure blob storage and key vault to save keys
            var filePath = Path.Combine(_env.WebRootPath, imgfile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imgfile.CopyToAsync(fileStream);
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
