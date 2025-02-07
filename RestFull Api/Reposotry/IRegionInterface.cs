using Microsoft.AspNetCore.Mvc;
using RestFull_Api.Models.Domains;

namespace RestFull_Api.Reposotry
{
    public interface IRegionInterface
    {
        Task<List<Region>> GetRegions();

        Task<Region?> GetRegionwithID(Guid regionID);

        Task<Region> CreateRegion(Region region);

        Task<Region?> UpdateRegion(Guid id,Region region);

    }
}
