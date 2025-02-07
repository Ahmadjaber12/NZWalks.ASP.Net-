using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFull_Api.Data;
using RestFull_Api.Models.Domains;
using RestFull_Api.Models.Domains.DTO;
using RestFull_Api.Reposotry;

namespace RestFull_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        public readonly IMapper mapper;
        public readonly NZWalks cntx;
        public readonly IWalkRepo walkRepo;
        public WalksController(IMapper mapper,NZWalks cntx,IWalkRepo walkRepo) 
        { 
                this.mapper = mapper;
                this.cntx = cntx;
                this.walkRepo = walkRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddWalkDTO walkDTO)
        {
           var walk= mapper.Map<Walk>(walkDTO);
           await walkRepo.CreateAsync(walk);
          
           return Ok(mapper.Map<WalkDTO>(walk));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn ,[FromQuery] string? filterQuery, string? SortBy, bool isAscending )
        {
            var walks = await walkRepo.GetALLAsync(filterOn,filterQuery,SortBy,isAscending);
            return Ok(mapper.Map<List<WalkDTO>>(walks));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk=await walkRepo.GetWalkById(id);
            if(walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkDTO updateWalk)
        {
            var walkModel= mapper.Map<Walk>(updateWalk);
           walkModel= await walkRepo.UpdateWalkAsync(id, walkModel);
            if( walkModel == null )
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkModel));

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var deletedWalk=await walkRepo.DeleteWalkAsync(id);
            if (deletedWalk == null)
                return NotFound();
            return Ok(mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
