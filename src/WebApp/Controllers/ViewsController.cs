using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ViewsController : Controller
    {
        private readonly ViewsService _viewsService;

        public ViewsController(ViewsService viewsService)
        {
            _viewsService = viewsService;
        }

        [Authorize]
        [HttpGet("/getAll")]
        public async Task<IActionResult> GetAllViews()
        {
            var numberOfViews = await _viewsService.GetViews();

            return Ok(numberOfViews);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseViews()
        {
            await _viewsService.UpdateViews();

            return Ok();
        }

    }
}
