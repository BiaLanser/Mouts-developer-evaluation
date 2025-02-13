using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] CartSortOrder _order = CartSortOrder.IdAsc)
        {
            var query = new ListCartsQuery { Page = _page, Size = _size, Order = _order };
            var carts = await _mediator.Send(query);
            return Ok(carts);
        }


       // [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetCartRequest { Id = id };
            var validator = new GetCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = new GetCartQuery(id);
            var cart = await _mediator.Send(query);

            if (cart == null)
                return NotFound(new { message = "Cart not found" });

            return Ok(cart);
        }

        //[Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateCartCommand>(request);
            var response = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

       // [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (id != request.Id)
                return BadRequest(new { message = "Cart ID mismatch" });

            var command = _mapper.Map<UpdateCartCommand>(request);
            var response = await _mediator.Send(command);

            if (response == null)
                return NotFound(new { message = "Cart not found" });

            return Ok(response);
        }
        
        //[Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteCartRequest { Id = id };
            var validator = new DeleteCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _mediator.Send(new DeleteCartQuery(id));

            if (!result.Success)
                return NotFound(new { message = "Cart not found" });

            return NoContent();
        }

    }
}
