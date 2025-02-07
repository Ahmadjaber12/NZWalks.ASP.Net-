using RestFull_Api.Data;
using RestFull_Api.Models.Domains;

namespace RestFull_Api.Reposotry
{
    public class LocalImageRepo : IImageUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        public readonly NZWalks nZWalks;
        public LocalImageRepo(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,NZWalks cntx)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = httpContextAccessor;
            nZWalks = cntx;
        }

        public async Task<Image> Upload(Image image)
        {
            var LocalPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", 
               $"{image.FileName}{image.FileExtension}");

            // Upload Image to the folder
            using var stream = new FileStream(LocalPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await nZWalks.Images.AddAsync(image);
            await nZWalks.SaveChangesAsync();
            return image;
        }
    }
}
