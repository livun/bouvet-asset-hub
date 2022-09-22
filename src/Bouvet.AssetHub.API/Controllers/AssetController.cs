//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Bouvet.AssetHub.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AssetsController : ControllerBase
//    {
//        private readonly DataContext context;
//        private readonly IMediator mediator;

//        public AssetsController(DataContext context, IMediator mediator)
//        {
//            this.context = context;
//            this.mediator = mediator;
//        }
//        [Route("/")]
//        [HttpGet]
//        public async Task<IActionResult> GetAssets()
//        {
//            var result = await mediator.Send
//            var assets = await context.Assets.ToListAsync();
//            if( assets is not null ) return Ok(assets);
//            return BadRequest();

//        }
//        [Route("/")]
//        [HttpPost]
//        public async Task<IActionResult> AddAsset(AssetDto dto)
//        {

            
//            var asset = new AssetEntity
//            {
//                SerialNumber = dto.serialNumber,
//                Status = Status.Registered,
//                //Category = Enum.Parse<Category>(dto.category)
//            };
//            await context.Assets.AddAsync(asset);
//            await context.SaveChangesAsync();
//            return Ok("Asset added");

//        }

//    }
//    public record AssetDto(int serialNumber, string category);
//}
