using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {   private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        // Create a Region
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Add(AddRegionViewModel addRegionViewModel)
        { 
                var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()

            {

                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7221/api/Regions"),
                Content=new StringContent(JsonSerializer.Serialize(addRegionViewModel),Encoding.UTF8,"application/json"),

            };
            var httpResponse= await client.SendAsync(httpRequestMessage);
            httpResponse.EnsureSuccessStatusCode();
            var res=await httpResponse.Content.ReadFromJsonAsync<RegionDto>();
            if (res is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }
        public async Task<IActionResult> Index()
        {
            List<RegionDto> responseList = new List<RegionDto>();
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponse = await client.GetAsync("https://localhost:7221/api/Regions");

                httpResponse.EnsureSuccessStatusCode();// if it is not success response it will throw an Exception


                responseList.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(responseList);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var client= httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7221/api/Regions/{Id.ToString()}");
            if (response is not null)
            { 
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto regionDto)
        {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7221/api/Regions/{regionDto.Id.ToString()}"),
                Content=new StringContent(JsonSerializer.Serialize(regionDto),Encoding.UTF8,"application/json")
            };

            var httpResp=await client.SendAsync(request);
            httpResp.EnsureSuccessStatusCode();
            var Response=await httpResp.Content.ReadFromJsonAsync<RegionDto>();//extract data from httpResp

            if (Response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto region)
        {
            try
            {
                var client = httpClientFactory.CreateClient();


                var httpResp = await client.DeleteAsync($"https://localhost:7221/api/Regions/{region.Id.ToString()}");
                httpResp.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

              
            }
            return View("Edit");
        }
    }
}
