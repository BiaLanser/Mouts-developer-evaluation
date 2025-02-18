using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductByCategory;
using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        [ProducesResponseType(typeof(ListProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] ProductSortOrder _order = ProductSortOrder.IdAsc)
        {
            var query = new ListProductsQuery { Page = _page, Size = _size, Order = _order };
            var products = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<ListProductResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = _mapper.Map<ListProductResponse>(products)
            });
        }
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
        {
            var request = new GetProductRequest { Id = id };
            var validator = new GetProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = new GetProductQuery(id);
            var product = await _mediator.Send(query);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = _mapper.Map<GetProductResponse>(product)
            });
        }
        
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetProductById), new { id = response.Id }, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = _mapper.Map<CreateProductResponse>(response)
            });
        }
        

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (id != request.Id)
                return BadRequest(new { message = "Product ID mismatch" });

            var command = _mapper.Map<UpdateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound(new { message = "Product not found" });

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = _mapper.Map<UpdateProductResponse>(response)
            });
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _mediator.Send(new DeleteProductQuery(id));

            if (!result.Success)
                return NotFound(new { message = "Product not found" });

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ListCategoriesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new ListCategoriesQuery());

            return Ok(new ApiResponseWithData<ListCategoriesResponse>
            {
                Success = true,
                Message = "Categories retrieved successfully",
                Data = _mapper.Map<ListCategoriesResponse>(categories)
            });
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(ListCategoriesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductByCategory(string category, CancellationToken cancellationToken, [FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] ProductSortOrder _order = ProductSortOrder.IdAsc)
        {
            var request = new GetProductByCategoryRequest { Category = category };
            var validator = new GetProductByCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = new GetProductByCategoryQuery { Category = category, Page = _page, Size = _size, Order = _order };
            var products = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<GetProductByCategoryResponse>
            {
                Success = true,
                Message = "Products by category retrieved successfully",
                Data = _mapper.Map<GetProductByCategoryResponse>(products)
            });
        }
    }

}