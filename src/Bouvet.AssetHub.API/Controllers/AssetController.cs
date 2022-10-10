
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator _mediator;
    
        public AssetsController(IMediator mediator)
        {
            _mediator = mediator;
         
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var response = await _mediator.Send(new GetAssetsQuery());
            Console.WriteLine(response);
            return Ok(response);
            //if (response.Any() is false)
            //    return NotFound("No assets in database.");
            //return Ok(response);
            

        }
        
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAssetbyId(int id)
        //{
        //    var response = await _mediator.Send(new GetAssetByIdQuery(id));
           
            //return Ok(res); 
            //if (response.Any() is false)
            //    return NotFound("No assets in database.");
            //return Ok(response);


        //}
        //[Route("/")]
        //[HttpPost]
        //public async Task<IActionResult> AddAsset(AssetDto dto)
        //{


        //    var asset = new AssetEntity
        //    {
        //        SerialNumber = dto.serialNumber,
        //        Status = Status.Registered,
        //        //Category = Enum.Parse<Category>(dto.category)
        //    };
        //    await context.Assets.AddAsync(asset);
        //    await context.SaveChangesAsync();
        //    return Ok("Asset added");

        //}

    }
    public record AssetDto(int serialNumber, string category);


}
