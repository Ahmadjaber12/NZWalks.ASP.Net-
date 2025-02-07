using RestFull_Api.Models.Domains;

namespace RestFull_Api.Reposotry
{
    public interface IWalkRepo
    {
       Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetALLAsync(string? filterOn=null , string? filterQuery=null
            ,string? SortBy=null , bool isAscending=true);
        Task<Walk?> GetWalkById(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id,Walk walk);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
