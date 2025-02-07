using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFull_Api.Data;
using RestFull_Api.Models.Domains;

namespace RestFull_Api.Reposotry
{
    public class SQLRegion : IRegionInterface
    {   
        public readonly NZWalks CNTX;
        public SQLRegion(NZWalks cntx)
        {
            CNTX=cntx;
        }

        public async Task<Region> CreateRegion(Region region)
        {
          await CNTX.Regions.AddAsync(region);
          await CNTX.SaveChangesAsync();
          return region;
        }

        public async Task<List<Region>> GetRegions()
        {
           return await CNTX.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionwithID(Guid regionID)
        {
           return await CNTX.Regions.FirstOrDefaultAsync(x => x.Id==regionID);
         
        }

        public async Task<Region?> UpdateRegion(Guid id,Region region)
        {
            var existRegion = await CNTX.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (existRegion == null)
            {
                return null;
            }
            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImageUrl = region.RegionImageUrl;
            await CNTX.SaveChangesAsync();
            return existRegion;
        }
    }
}
