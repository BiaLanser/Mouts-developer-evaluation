using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _cartService.GetAllCarts();

            var cartDtos = carts.Select(cart => new
            {
                cart.Id,
                cart.UserId,
                cart.Date,
                Products = cart.Products.Select(cp => new CartProductDto
                {
                    Id = cp.Id,
                    ProductId = cp.ProductId 
                }).ToList()
            }).ToList();

            return Ok(cartDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cart = await _cartService.GetCartById(id);
            if (cart == null)
                return NotFound();

            var cartDto = new
            {
                cart.Id,
                cart.UserId,
                cart.Date,
                Products = cart.Products.Select(cp => new CartProductDto
                {
                    Id = cp.Id,
                    ProductId = cp.ProductId 
                }).ToList()
            };

            return Ok(cartDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cart cart)
        {
            if (cart == null)
                return BadRequest();

            var createdCart = await _cartService.AddCart(cart);
            return CreatedAtAction(nameof(GetById), new { id = createdCart.Id }, createdCart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cart cart)
        {
            if (cart == null || id != cart.Id)
                return BadRequest();

            var updatedCart = await _cartService.UpdateCart(id, cart);
            if (updatedCart == null)
                return NotFound();

            return Ok(updatedCart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _cartService.DeleteCart(id);
            if (!success)
                return NotFound();

            return Ok(new { message = "Cart deleted successfully." });
        }

    }
}
S