﻿using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] ProductSortOrder _order = ProductSortOrder.IdAsc)
        {

            var products = await _productService.GetAllProducts(_page, _size, _order);
            return Ok(products);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Title = createProductDto.Title,
                Price = createProductDto.Price,
                Description = createProductDto.Description,
                Category = createProductDto.Category,
                Image = createProductDto.Image,
                Rating = createProductDto.Rating
            };

            await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != product.Id)
                return BadRequest(new { message = "Product ID mismatch" });

            var existingProduct = await _productService.GetProductById(id);
            if (existingProduct == null)
                return NotFound(new { message = "Product not found" });

            await _productService.UpdateProduct(product);
            return Ok(product);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            await _productService.DeleteProduct(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productService.GetCategories();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductByCategory(string category, [FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] ProductSortOrder _order = ProductSortOrder.IdAsc)
        {
            var products = await _productService.GetProductByCategory(category, _page, _size, _order);
            return Ok(products);
        }
    }
}