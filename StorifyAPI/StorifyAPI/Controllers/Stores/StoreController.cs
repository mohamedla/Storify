using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;
using StorifyAPI.ModelBinders;

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
        public IActionResult GetAllStores()
        {
            var stores = _repositoryManager.Store.GetAllStores(false);

            var storesDTO = _mapper.Map<IEnumerable<StoreDTO>>(stores);

            return Ok(storesDTO);
        }

        [HttpGet("{id}", Name = "StoreById")]
        public IActionResult GetStore(Guid id)
        {
            var store = _repositoryManager.Store.GetStore(id, false);

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
        public IActionResult GetStoreCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogInfo("Client Sent Empty List Of Ids");
                return BadRequest("The List Of Store Ids Is Empty");
            }

            var stores = _repositoryManager.Store.GetStoresByIds(ids, false);

            if (ids.Count() != stores.Count())
            {
                _logger.LogInfo($"Some ids are not valid in connection");
                return NotFound();
            }

            var storesDTO = _mapper.Map<IEnumerable<StoreDTO>>(stores);
            return Ok(storesDTO);
        }

        [HttpPost("")]
        public IActionResult AddStore([FromBody] StoreCreateDTO storeDTO)
        {
            if(storeDTO == null)
            {
                _logger.LogError("Store Create DTO Object Sent from client is null");
                return BadRequest("Store Is Empty");
            }

            var store = _mapper.Map<Store>(storeDTO);

            _repositoryManager.Store.CreateStore(store);
            _repositoryManager.Save();

            var returnStore = _mapper.Map<StoreDTO>(store);

            return CreatedAtRoute("StoreById", new { Id = returnStore.Id }, returnStore);
        }

        [HttpPost("collection")]
        public IActionResult AddStoreCollection([FromBody] IEnumerable<StoreCreateDTO> storesDTO)
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

            _repositoryManager.Save();

            var returnStores = _mapper.Map<IEnumerable<StoreDTO>>(stores);

            var ids = string.Join(",", returnStores.Select(s => s.Id));

            return CreatedAtRoute("GetStoreCollection", new { ids }, returnStores);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStore(Guid id)
        {
            var store = _repositoryManager.Store.GetStore(id, false);
            if (store == null)
            {
                _logger.LogError($"No Store With id: {id} found in DB");
                return NotFound();
            }

            _repositoryManager.Store.DeleteStore(store);
            _repositoryManager.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStore( Guid id , [FromBody] StoreUpdateDTO storeDTO)
        {
            if (storeDTO == null)
            {
                _logger.LogError("Store Create DTO Object Sent from client is null");
                return BadRequest("Store Is Empty");
            }

            var store = _repositoryManager.Store.GetStore(id, true);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {id} Exist In The Database");
                return NotFound();
            }

            _mapper.Map(storeDTO, store);
            _repositoryManager.Save();

            return NoContent();
        }
    }
}
