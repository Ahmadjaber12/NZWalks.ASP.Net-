using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFull_Api.Models.Domains;
using RestFull_Api.Models.Domains.DTO;
using RestFull_Api.Reposotry;

namespace RestFull_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {   public readonly IImageUpload imageUpload;
        public ImageController(IImageUpload imageUpload)
        {
            this.imageUpload = imageUpload;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)

        {
            ValidateFile(imageUploadRequestDTO);

            if (ModelState.IsValid) 
            {
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileDescription = imageUploadRequestDTO.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileName = imageUploadRequestDTO.FileName,
                    FileSize = imageUploadRequestDTO.File.Length
                };
                await imageUpload.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private async void ValidateFile(ImageUploadRequestDTO image)
        {
            var AllowedExt = new string[] { ".jpg", ".png", ".jped" };
            if (!AllowedExt.Contains(Path.GetExtension(image.File.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extention");
            
            }
            if (image.File.Length > 10485760) {

                ModelState.AddModelError("file", "file size is more than 10MB");
            }
           

        }
    }
}
