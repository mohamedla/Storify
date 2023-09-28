using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;
using StorifyAPI.ModelBinders;
using StorifyAPI.ActionFilters;

namespace StorifyAPI.Controllers.Stores
{
    [Route("api/Stores")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public StoreController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {

            _logger = logger;
            _repositoryManager = repositoryManager;
            _mapper = mapper;

        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllStoresAsync()
        {
            var stores = await _repositoryManager.Store.GetAllStoresAsync(false);

            var storesDTO = _mapper.Map<IEnumerable<StoreDTO>>(stores);

            return Ok(storesDTO);
        }

        [HttpGet("{id}", Name = "StoreById")]
        public async Task<IActionResult> GetStoreAsync(Guid id)
        {
            var store = await _repositoryManager.Store.GetStoreAsync(id, false);

            if(store == null)
            {
                _logger.LogInfo($"No Store With Id : {id} Exist In The Database");
                return NotFound();
            }
            else
            {
                var storeDTO = _mapper.Map<StoreDTO>(store);
                return Ok(storeDTO);
            }
        }

        [HttpGet("collections/{{ids}}", Name = "GetStoreCollection")]
        public async Task<IActionResult> GetStoreCollectionAsync([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogInfo("Client Sent Empty List Of Ids");
                return BadRequest("The List Of Store Ids Is Empty");
            }

            var stores = await _repositoryManager.Store.GetStoresByIdsAsync(ids, false);

            if (ids.Count() != stores.Count())
            {
                _logger.LogInfo($"Some ids are not valid in connection");
                return NotFound();
            }

            var storesDTO = _mapper.Map<IEnumerable<StoreDTO>>(stores);
            return Ok(storesDTO);
        }

        [HttpPost("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddStoreAsync([FromBody] StoreCreateDTO storeDTO)
        {
            var store = _mapper.Map<Store>(storeDTO);

            _repositoryManager.Store.CreateStore(store);
            await _repositoryManager.SaveAsync();

            var returnStore = _mapper.Map<StoreDTO>(store);

            return CreatedAtRoute("StoreById", new { Id = returnStore.Id }, returnStore);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> AddStoreCollectionAsync([FromBody] IEnumerable<StoreCreateDTO> storesDTO)
        {
            if (storesDTO == null)
            {
                _logger.LogError("Stores Create DTO Object Sent from client is null");
                return BadRequest("Stores Is Empty");
            }

            var stores = _mapper.Map<IEnumerable<Store>>(storesDTO);

            foreach (var store in stores)
            {
                _repositoryManager.Store.CreateStore(store);
            }

            await _repositoryManager.SaveAsync();

            var returnStores = _mapper.Map<IEnumerable<StoreDTO>>(stores);

            var ids = string.Join(",", returnStores.Select(s => s.Id));

            return CreatedAtRoute("GetStoreCollection", new { ids }, returnStores);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationStoreExistsAttribute))]
        public async Task<IActionResult> DeleteStoreAsync(Guid id)
        {
            var store = HttpContext.Items["store"] as Store;

            _repositoryManager.Store.DeleteStore(store);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationStoreExistsAttribute))]
        public async Task<IActionResult> UpdateStoreAsync( Guid id , [FromBody] StoreUpdateDTO storeDTO)
        {
            var store = HttpContext.Items["store"] as Store;

            _mapper.Map(storeDTO, store);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
