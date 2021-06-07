using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                imageName = userName + Path.GetExtension(postedFile.FileName);

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
    }
}
