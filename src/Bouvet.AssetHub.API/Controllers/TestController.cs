using System.Net;
using Bouvet.AssetHub.API.Entities;
using Bouvet.AssetHub.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IAssetRepository _repository;
        private readonly ILogger<TestController> _logger;

        public TestController(IAssetRepository repository, ILogger<TestController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Asset>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            var assets = await _repository.GetAssets();
            return Ok(assets);
        }

        [HttpGet("{id:length(24)}", Name = "GetAsset")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> GetAssetById(string id)
        {
            var asset = await _repository.GetAsset(id);

            if (asset == null)
            {
                _logger.LogError($"Asset with id: {id}, not found.");
                return NotFound();
            }

            return Ok(asset);
        }

        [Route("[action]/{category}", Name = "GetAssetByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Asset>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetByCategory(string category)
        {
            var assets = await _repository.GetAssetByCategory(category);
            return Ok(assets);
        }

        [Route("[action]/{name}", Name = "GetAssetByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Asset>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetByName(string name)
        {
            var items = await _repository.GetAssetByName(name);
            if (items == null)
            {
                _logger.LogError($"Assets with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> CreateAsset([FromBody] Asset asset)
        {
            await _repository.CreateAsset(asset);

            return CreatedAtRoute("GetAsset", new { id = asset.Id }, asset);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsset([FromBody] Asset asset)
        {
            return Ok(await _repository.UpdateAsset(asset));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteAsset")]        
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAssetById(string id)
        {
            return Ok(await _repository.DeleteAsset(id));
        }
    }
}