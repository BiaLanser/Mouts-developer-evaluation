using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SaleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ListSaleResponse>), 200)]
        public async Task<ActionResult<IEnumerable<ListSaleResponse>>> GetAllSales()
        {
            var query = new ListSaleQuery();
            var sales = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<ListSaleResponse>
            {
                Success = true,
                Message = "Sales retrieved successfully",
                Data = _mapper.Map<ListSaleResponse>(sales)
            });
        }


        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateSaleResponse>> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var sale = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetSaleBySaleNumber), new { id = sale.SaleNumber }, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResponse>(sale)
            });
        }


        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("{saleNumber}")]
        [ProducesResponseType(typeof(GetSaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSaleResponse>> GetSaleBySaleNumber(int saleNumber, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { SaleNumber = saleNumber };
            var validator = new GetSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = new GetSaleQuery(saleNumber);
            var sale = await _mediator.Send(query);

            if (sale == null)
                return NotFound(new { message = "Sale not found" });

            return Ok(new ApiResponseWithData<GetSaleResponse>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = _mapper.Map<GetSaleResponse>(sale)
            });
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPatch("cancel/{saleNumber}")]
        [ProducesResponseType(typeof(CancelSaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSale(int saleNumber, CancellationToken cancellationToken)
        {
            var request = new CancelSaleRequest { SaleNumber = saleNumber };
            var validator = new CancelSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new CancelSaleQuery(saleNumber);
            var sale = await _mediator.Send(command);

            if (sale == null)
                return NotFound(new { message = "Sale not found" });

            return Ok(new ApiResponseWithData<CancelSaleResponse>
            {
                Success = true,
                Message = "Sale canceled successfully",
                Data = _mapper.Map<CancelSaleResponse>(sale)
            });
        }
        
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPut("{saleNumber}")]
        [ProducesResponseType(typeof(UpdateSaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateSaleResponse>> UpdateSale(int saleNumber, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.SaleNumber = saleNumber;
            var updatedSale = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<UpdateSaleResponse>
            {
                Success = true,
                Message = "Sale updated successfully",
                Data = _mapper.Map<UpdateSaleResponse>(updatedSale)
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{saleNumber}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale(int saleNumber)
        {
            var request = new DeleteSaleRequest { SaleNumber = saleNumber};
            var validator = new DeleteSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new DeleteSaleQuery(saleNumber);
            await _mediator.Send(command);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Sale deleted successfully"
            });

        }

    }
}