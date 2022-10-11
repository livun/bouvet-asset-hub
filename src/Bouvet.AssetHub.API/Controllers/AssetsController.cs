
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using LanguageExt.UnsafeValueAccess;
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
        private readonly IMapper _mapper;

        public AssetsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
         
        }

        // GET /assets
        [HttpGet]
        public async Task<IActionResult> GetAssetsAsync()
        {
            var response = await _mediator.Send(new GetAssetsQuery());
            Console.WriteLine(response);
            return Ok(response);
          
            

        }

        // POST /assets
        [HttpPost]
        public async Task<IActionResult> AddAssetAsync(AssetRequestDto dto)
        {
            var result = await _mediator.Send(new CreateAssetCommand { Asset = _mapper.Map<AssetEntity>(dto)});
            if (result.IsSome)
            {
                return Ok("New asset is added!");
            }
            return BadRequest("Could not add asset");


        }
        // PUT /assets
        [HttpPut]
        public async Task<IActionResult> UpdateAssetByIdAsync(List<int> id)
        {
            throw new NotImplementedException();

        }

        // GET /assets/1
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssetbyIdAsync(int id)
        {
            var result = await _mediator.Send( new GetAssetByIdQuery(id));
            if (result.IsSome)
            {
                return Ok(_mapper.Map<AssetEntity, AssetResponseDto>((AssetEntity)result));
            }
            return NotFound();


        }
        // PUT /assets/1
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAssetByIdAsync(int id)
        {
            throw new NotImplementedException();

        }
        // DELETE /assets/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAssetByIdAsync(int id)
        {
            throw new NotImplementedException();

        }
        // GET /assets/1/loans
        [Route("{id}/loans")]
        [HttpGet]
        public async Task<IActionResult> GetLoansByAssetIdAsync(int id)
        {
            throw new NotImplementedException();

        }

       


    }
 

}
