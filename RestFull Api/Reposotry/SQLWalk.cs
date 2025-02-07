using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RestFull_Api.Data;
using RestFull_Api.Models.Domains;

namespace RestFull_Api.Reposotry
{
    public class SQLWalk : IWalkRepo

    {
        public readonly NZWalks cntx;
        public SQLWalk(NZWalks cntx)
        {
            this.cntx = cntx;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await cntx.Walks.AddAsync(walk);
            await cntx.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walk=await cntx.Walks.FirstOrDefaultAsync(x => x.Id==id);
             cntx.Walks.Remove(walk);
             cntx.SaveChanges();
            return walk;    
        }

        public async Task<List<Walk>> GetALLAsync(string? filterOn=null,string? filterQuery=null, string? SortBy = null, bool isAscending = true)

        {   
            var walks= cntx.Walks.Include(x => x.Difficulity).Include(x => x.Region).AsQueryable();


            if (string.IsNullOrWhiteSpace( filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) ==false)

            //Filtering

            {
                if (filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))

                    walks= walks.Where(x => x.Name.Contains(filterQuery));
            }
            

            //Sorting
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if(SortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                    walks= isAscending ? walks.OrderBy(x => x.Name) :walks.OrderByDescending (x => x.Name);

                if (SortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))

                        walks= isAscending ? walks.OrderBy(x => x.LengthInKM) :walks.OrderByDescending(x=>x.LengthInKM);
            }
            return await walks.ToListAsync();


        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
           return await cntx.Walks.Include("Regions").Include("Difficulity").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id,Walk walk)
        {
            var walkUpdated = await cntx.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkUpdated != null) { 
                return null;
            }
            walkUpdated.Name= walk.Name;
            walkUpdated.Description= walk.Description;
            walkUpdated.LengthInKM = walk.LengthInKM;
            walkUpdated.WalkImageUrl=walk.WalkImageUrl;
            walkUpdated.RegionId=walk.RegionId;
            walkUpdated.DifficulityId=walk.DifficulityId;
            await cntx.SaveChangesAsync();
            return walkUpdated;
        }
    }
}
