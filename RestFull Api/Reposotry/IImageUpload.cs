using RestFull_Api.Models.Domains;

namespace RestFull_Api.Reposotry
{
    public interface IImageUpload
    {
        Task<Image> Upload(Image image);
    }
}
