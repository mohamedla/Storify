using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Contracts;

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

        [HttpGet("{id}")]
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
    }
}
