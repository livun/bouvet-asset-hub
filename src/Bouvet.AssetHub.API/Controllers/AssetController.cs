using Bouvet.AssetHub.Data;
using Bouvet.AssetHub.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly DataContext context;

        public AssetController(DataContext context)
        {
            this.context = context;
        }
        [Route("assets/")]
        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await context.Assets.ToListAsync();
            if( assets is not null ) return Ok(assets);
            return BadRequest();

        }
        [Route("asset/")]
        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetDto dto)
        {

            
            var asset = new AssetEntity
            {
                SerialNumber = dto.serialNumber,
                Status = Status.Registered,
                Category = Enum.Parse<Category>(dto.category)
            };
            await context.Assets.AddAsync(asset);
            await context.SaveChangesAsync();
            return Ok("Asset added");

        }

    }
    public record AssetDto(int serialNumber, string category);
}
