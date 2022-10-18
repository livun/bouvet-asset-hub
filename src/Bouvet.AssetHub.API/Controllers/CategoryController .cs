
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

        //// GET /categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDto>>> GetCategoriesAsync()
        {
            var result = await _mediator.Send(new GetCategoriesQuery());
            return new ResultHelper<List<CategoryResponseDto>>().OkOrNotFound(result);
        }
        // GET /categories/1/assets
        [Route("{id}/assets")]
        [HttpGet]
        public async Task<ActionResult<List<AssetResponseDto>>> GetAssetsByCategoryAsync(int id)
        {
            var result = await _mediator.Send(new GetAssetsByCategoryQuery(id));
            return new ResultHelper<List<AssetResponseDto>>().OkOrNotFound(result);
        }

        // POST /categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> AddCategoryAsync(CreateCategoryCommand dto)
        {
            var result = await _mediator.Send(dto);
            return new ResultHelper<CategoryResponseDto>().OkOrBadRequest(result, "Could not add category!");
        }

        // DELETE /categories/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<CategoryResponseDto>> DeleteAssetByIdAsync(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            return new ResultHelper<CategoryResponseDto>().OkOrBadRequest(result, "Category cannot be deleted!");
        }
        // PUT /categories/1
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<CategoryResponseDto>> UpdateAssetAsync(UpdateCategoryDto dto, int id)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(id, dto.Name));
            return new ResultHelper<CategoryResponseDto>().OkOrNotFound(result);
        }


    }





}

