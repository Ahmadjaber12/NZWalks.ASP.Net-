using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFull_Api.Data;
using RestFull_Api.Models.Domains;
using RestFull_Api.Models.Domains.DTO;
using RestFull_Api.Reposotry;

namespace RestFull_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class RegionsController : ControllerBase
    {
        public readonly NZWalks cntx;
        public readonly IRegionInterface regionRepo;
        public readonly IMapper mapper;
        public readonly IConfiguration configuration;
        public RegionsController(NZWalks cntx,IRegionInterface regionRepo ,IMapper mapper,IConfiguration configuration)
        {
            this.regionRepo = regionRepo;
            this.mapper = mapper;
            this.cntx = cntx;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            var regions =await regionRepo.GetRegions();
            Console.WriteLine("configuration isss...."+configuration["JWT:Audience"]);

            return Ok( mapper.Map<List<RegionDTO>>(regions));

        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetRegionwithid([FromRoute] Guid Id)
        {
            Console.WriteLine("Id isss"+Id);
            var region =await regionRepo.GetRegionwithID(Id);
            var regionD = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return  regionD!=null ?  Ok(regionD) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDTO regionDTO)
        {
            var region=mapper.Map<Region>(regionDTO);

           await regionRepo.CreateRegion(region);

            var regionDto = mapper.Map<RegionDTO>(region);
            return CreatedAtAction("GetRegionwithID",new{ id=regionDto.Id},regionDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task< IActionResult> Update(Guid id,[FromBody] UpdateDto regionDTO) {
            // Map DTO to Domain Model
            var reg = mapper.Map<Region>(regionDTO);
            var region = await regionRepo.UpdateRegion(id, reg);

            // convert Region to RegionDTO
            var r = mapper.Map<RegionDTO> (region);
            return Ok(r);
            }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete (Guid Id)
        {
            var regionModel=await regionRepo.GetRegionwithID(Id);
            if (regionModel != null)
            {
                 cntx.Regions.Remove(regionModel);
                await cntx.SaveChangesAsync();
                return Ok();
            }
            else
                return NotFound();
        }
    
    }
}
