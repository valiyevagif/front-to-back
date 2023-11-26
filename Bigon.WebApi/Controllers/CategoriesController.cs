using Bigon.Business.Modules.CategoriesModule.Commands.CategoryAddCommand;
using Bigon.Business.Modules.CategoriesModule.Commands.CategoryEditCommand;
using Bigon.Business.Modules.CategoriesModule.Commands.CategoryRemoveCommand;
using Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetAllQuery;
using Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] CategoryGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] CategoryGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize("admin.categories.create")]
        public async Task<IActionResult> Add([FromBody] CategoryAddRequest request)
        {
            var response = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [Authorize("admin.categories.edit")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize("admin.categories.delete")]
        public async Task<IActionResult> Remove([FromRoute] CategoryRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
