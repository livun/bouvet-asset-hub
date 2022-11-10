using Bouvet.AssetHub.API.Contracts;
using Bouvet.AssetHub.API.Domain.Asset.Services.Commands;
using Bouvet.AssetHub.API.Domain.Asset.Services.Queries;
using Bouvet.AssetHub.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //// GET /categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDto>>> GetCategoriesAsync()
        {
            var result = await _mediator.Send(new GetCategoriesQuery());
            return new ActionResultHelper<List<CategoryResponseDto>>().OkOrNotFound(result, "Currently no categories in table!");
        }
        // GET /categories/1
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<CategoryResponseDto>> GetCategorybyIdAsync(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return new ActionResultHelper<CategoryResponseDto>().OkOrNotFound(result, "Category does not exist!");

        }
        // GET /categories/1/assets
        [Route("{id}/assets")]
        [HttpGet]
        public async Task<ActionResult<List<AssetResponseDto>>> GetAssetsByCategoryAsync(int id)
        {
            var result = await _mediator.Send(new GetAssetsByCategoryQuery(id));
            return new ActionResultHelper<List<AssetResponseDto>>().OkOrNotFound(result, $"No assets by category: {id} in table!");
        }

        // POST /categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> AddCategoryAsync(CreateCategoryDto dto)
        {
            var result = await _mediator.Send(new CreateCategoryCommand(dto.Name));
            return new ActionResultHelper<CategoryResponseDto>().OkOrBadRequest(result, "Could not add category!");
        }

        // DELETE /categories/1
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<CategoryResponseDto>> DeleteCategoryByIdAsync(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            return new ActionResultHelper<CategoryResponseDto>().OkOrBadRequest(result, "Category cannot be deleted!");
        }
        // PUT /categories/1
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<CategoryResponseDto>> UpdateCategoryAsync(UpdateCategoryDto dto, int id)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(id, dto.Name));
            return new ActionResultHelper<CategoryResponseDto>().OkOrBadRequest(result, "Cannot update category!");
        }


    }





}

