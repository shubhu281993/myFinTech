using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace myFinTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDataContext _dataContext;

        public WatchListController(ILogger<WatchListController> logger, IDataContext mongoDataContext)
        {
            _dataContext = mongoDataContext;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogDebug("GetAll items.");

            try
            {
                var items = _dataContext.WatchListCombo.GetAll();

                if (items == null || !items.Any())
                {
                    _logger.LogWarning("GetAll() - NOT FOUND");
                    return NotFound();
                }

                return Ok(items); //Rap result with Ok (200) status code
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get() - EXCEPTION. {ex.ToString().Replace("\r\n", "").Replace("\n", "").Replace("\r", "")}");

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}