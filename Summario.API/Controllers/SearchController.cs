
using Microsoft.AspNetCore.Mvc;
using Summario.Services.Interfaces;

namespace Summario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            var result = await _searchService.SearchAsync(query);
            return Ok(result);
        }
    }
}
