
using AutoMapper;
using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Controllers.Helpers;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Repositories;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using LanguageExt.UnsafeValueAccess;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        // GET /categories
        [HttpGet]
        public async Task<ActionResult<List<AssetResponseDto>>> GetCategoriesAsync()
        {
            var result = await _mediator.Send(new GetAssetsQuery());
            return new ResultHelper<List<AssetResponseDto>>().OkOrNotFound(result);
        }

        
        }
        // GET /categories/1/assets
        [Route("{id}/assets")]
        [HttpGet]
        public async Task<IActionResult> GetAssetsByCategoryAsync(int id)
        {
            throw new NotImplementedException();

        }

       


    }
 

}
