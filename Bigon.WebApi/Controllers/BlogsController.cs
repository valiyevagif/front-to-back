using AutoMapper;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostAddCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostAddCommentCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostEditCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostPublishCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand;
using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostCommentsQuery;
using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery;
using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetBySlugQuery;
using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Extensions;
using Bigon.WebApi.Mapping;
using Bigon.WebApi.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.Annotations;

namespace Bigon.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public BlogsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] BlogPostGetAllRequest request)
        {
            var response = await mediator.Send(request);

            var dto = mapper.Map<PagedResponse<BlogPostDto>>(response, cfg =>
            {
                cfg.Items["host"] = Request.GetHost();
                Request.AppendHeaderTo(cfg.Items, "dateFormat");
            });

            return Ok(dto);
        }


        // https://www.tektutorialshub.com/asp-net-core/asp-net-core-route-constraints

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] BlogPostGetByIdRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{slug}")]
        //[Authorize("admin.blogs.details")]
        public async Task<IActionResult> GetBySlug([FromRoute] BlogPostGetBySlugRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment([FromBody]BlogPostAddCommentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> Comments([FromRoute] BlogPostCommentsRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize("admin.blogs.create")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromForm] BlogPostAddRequest request)
        {
            var response = await mediator.Send(request);

            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [Authorize("admin.blogs.edit")]
        public async Task<IActionResult> Edit(int id, [FromForm] BlogPostEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("{postId}/publish")]
        [Authorize("admin.blogs.publish")]
        public async Task<IActionResult> Publish([FromRoute] BlogPostPublishRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize("admin.blogs.delete")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] BlogPostRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
