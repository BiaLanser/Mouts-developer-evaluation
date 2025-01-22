using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "id asc")
        {
            var carts = await _cartService.GetAllCarts(_page, _size, _order);
            return Ok(carts);      
        }


        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cart = await _cartService.GetCartById(id);
            if (cart == null)
                return NotFound();

            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Date = cart.Date,
                Products = cart.Products.Select(cp => new CartProductDto
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity
                }).ToList()
            };

            return Ok(cartDto);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCartDto createCartDto)
        {
            if (createCartDto == null)
                return BadRequest();

            var cart = new Cart
            {
                UserId = createCartDto.UserId,
                Products = createCartDto.Products.Select(p => new CartProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList(),
                Date = DateTime.UtcNow
            };

            var createdCart = await _cartService.AddCart(cart);

            var cartDto = new CartDto
            {
                Id = createdCart.Id,
                UserId = createdCart.UserId,
                Date = createdCart.Date,
                Products = cart.Products.Select(cp => new CartProductDto
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity
                }).ToList()
            };
            return CreatedAtAction(nameof(GetById), new { id = createdCart.Id }, cartDto);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCartDto createCartDto)
        {
            if (createCartDto == null)
                return BadRequest();

            var cartToUpdate = new Cart
            {
                UserId = createCartDto.UserId,
                Products = createCartDto.Products.Select(p => new CartProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList(),
                Date = DateTime.UtcNow
            };

            var updatedCart = await _cartService.UpdateCart(id, cartToUpdate);

            var cartDto = new CartDto
            {
                Id = updatedCart.Id,
                UserId = updatedCart.UserId,
                Date = updatedCart.Date,
                Products = cartToUpdate.Products.Select(cp => new CartProductDto
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity
                }).ToList()
            };

            if (updatedCart == null)
                return NotFound();

            return Ok(cartDto);
        }

        [Authorize(Roles = "Admin, Manager")]
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
