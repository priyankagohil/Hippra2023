using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Hippra.Data;
using Hippra.Code;

namespace Hippra.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private AzureStorage Storage;
        private ImageHelper ImageHelper;


        public ImageController(UserManager<IdentityUser> userManager,
            ApplicationDbContext context,

            IOptions<AppSettings> settings)
        {
            _userManager = userManager;
            _context = context;
            Storage = new AzureStorage(settings);
            ImageHelper = new ImageHelper(Storage);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadimageAsync(IFormFile ImageForm)
        {
            // Perform an initial check to catch FileUpload class
            // attribute violations.
            if (!ModelState.IsValid)
            {
                return new JsonResult("FAILED");
            }

            var fileData =
                await ImageHelper.ProcessFormFile(ImageForm, ModelState);

            // Perform a second check to catch ProcessFormFile method
            // violations.
            if (!ModelState.IsValid)
            {
                return new JsonResult("FAILED");
            }
            var fileName = WebUtility.HtmlEncode(
                Path.GetFileName(ImageForm.FileName));

            // SECURITY TODO: sanitize input
            if (fileName.Contains(";"))
            {
                return new JsonResult("FAILED");
            }

            // here we considered save to store
            if (ImageHelper.IsImage(ImageForm))
            {
                if (ImageForm.Length > 0)
                {
                    using (Stream stream = ImageForm.OpenReadStream())
                    {
                        fileName = await ImageHelper.UploadImageToStorage(stream, ImageForm.FileName);
                    }
                }
            }
            else
            {
                return new JsonResult("FAILED");
                //return new UnsupportedMediaTypeResult();
            }

            return new JsonResult(fileName);
        }

    }
}
