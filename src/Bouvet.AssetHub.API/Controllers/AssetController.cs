using Bouvet.AssetHub.Domain.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AssetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [Route("/")]
        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var response = await mediator.Send(new GetAssets.Request());
            if (response.Any() is false)
                return NotFound("No assets in database.");
            return Ok(response);
            

        }
        [Route("/")]
        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetDto dto)
        {


            var asset = new AssetEntity
            {
                SerialNumber = dto.serialNumber,
                Status = Status.Registered,
                //Category = Enum.Parse<Category>(dto.category)
            };
            await context.Assets.AddAsync(asset);
            await context.SaveChangesAsync();
            return Ok("Asset added");

        }

    }
    public record AssetDto(int serialNumber, string category);
}
