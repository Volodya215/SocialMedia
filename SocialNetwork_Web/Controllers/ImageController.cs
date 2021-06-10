using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public ImageController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        /// <summary>
        /// Add user foto to storage 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost("UploadImage/{userName}")]
        [Authorize(Roles = "Customer")]
        // POST: /api/Image/UploadImage/{userName}
        public async Task<IActionResult> UploadImage(string userName)
        {
            try
            {
                string imageName = null;
                var httpRequest = HttpContext.Request;
                // Upload image
                var postedFile = httpRequest.Form.Files["Image"];
                imageName = userName + ".png" /*Path.GetExtension(postedFile.FileName)*/;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", imageName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(fileStream);
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Download user foto from storage
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User foto</returns>
        [HttpGet("GetImage/{userName}")]
        [Authorize(Roles = "Customer")]
        // POST: /api/Image/GetImage/{userName}
        public async Task<IActionResult> GetImage(string userName)
        {
            try
            {
                userName += ".png";
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploads, userName);
                if (!System.IO.File.Exists(filePath))
                    return NotFound();

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, GetContentType(filePath), userName);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Get type of file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>type</returns>
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
