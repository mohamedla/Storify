using Contracts;
using Microsoft.AspNetCore.Mvc;
using Stories;

namespace StorifyAPI.Controllers.Stores
{
    [Route("api/Store")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public StoreController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {

            _logger = logger;
            _repositoryManager = repositoryManager;

        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_repositoryManager.Store.GetAllStores(false));
        }
    }
}
