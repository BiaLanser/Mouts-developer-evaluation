using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Ambev.DeveloperEvaluation.WebApi.Features
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleController> _logger;
        private readonly ICartService _cartService;
        public SaleController(ISaleService saleService, IProductRepository productRepository, ISaleRepository saleRepository, ICartService cartService,ILogger<SaleController> logger)
        {
            _saleService = saleService;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _cartService = cartService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAllSales()
        {
            try
            {
                var sales = await _saleService.GetAllSales();

                if (!sales.Any())
                    return NoContent();

                var salesDto = sales.Select(sale => new SaleDto
                {
                    SaleNumber = sale.SaleNumber,
                    Date = sale.SaleDate,
                    CustomerId = sale.CustomerId,
                    Branch = sale.Branch,
                    IsCancelled = sale.IsCancelled,
                    SaleItems = sale.Items.Select(item => new SaleItemDTO
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        TotalAmount = item.TotalAmount
                    }).ToList(),
                    Discount = sale.Discount,
                    TotalSaleAmount = sale.TotalSaleAmount
                }).ToList();

                return Ok(salesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all sales");
                return StatusCode(500, "Internal error while fetching sales");
            }
        }


        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<ActionResult<SaleDto>> CreateSale([FromBody] CreateSaleDto createSale)
        {
            try
            {
                var cart = await _cartService.GetCartById(createSale.CartId);
                if (cart == null)
                    return NotFound("Cart not found");

                var sale = new Sale
                {
                    CustomerId = createSale.CustomerId,
                    Branch = createSale.Branch,
                    SaleDate = DateTime.UtcNow,
                    IsCancelled = false,
                    CartId = createSale.CartId
                };
                
                foreach (var cp in cart.Products)
                {
                    _logger.LogInformation($"Processing cart product: ProductId {cp.ProductId}, Quantity {cp.Quantity}");

                    var product = await _productRepository.GetProductById(cp.ProductId);
                    if (product != null)
                    {
                        var saleItem = new SaleItem
                        {
                            ProductId = cp.ProductId,
                            Quantity = cp.Quantity,
                            UnitPrice = product.Price,
                            TotalAmount = cp.Quantity * product.Price
                        };
                        sale.Items.Add(saleItem);
                    }
                } 


                var createdSale = await _saleService.AddSale(sale);

                var saleDto = new SaleDto
                {
                    SaleNumber = createdSale.SaleNumber,
                    Date = createdSale.SaleDate,
                    CustomerId = createdSale.CustomerId,
                    Branch = createdSale.Branch,
                    IsCancelled = createdSale.IsCancelled,
                    SaleItems = createdSale.Items.Select(item => new SaleItemDTO
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        TotalAmount = item.TotalAmount
                    }).ToList(),
                    Discount = createdSale.Discount,
                    TotalSaleAmount = createdSale.TotalSaleAmount
                };

                return CreatedAtAction(nameof(GetSaleBySaleNumber), new { saleNumber = createdSale.SaleNumber }, saleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to create sale");
                return StatusCode(500, "Internal error while processing the sale");
            }

        }


        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("{saleNumber}")]
        public async Task<ActionResult<SaleDto>> GetSaleBySaleNumber(int saleNumber)
        {
            var sale = await _saleService.GetSaleBySaleNumber(saleNumber);

            if (sale == null)
                return NotFound();

            var saleDto = new SaleDto
            {
                SaleNumber = sale.SaleNumber,
                Date = sale.SaleDate,
                CustomerId = sale.CustomerId,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled,
                SaleItems = sale.Items.Select(item => new SaleItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalAmount = item.TotalAmount
                }).ToList(),
                Discount = sale.Discount,
                TotalSaleAmount = sale.TotalSaleAmount
            };

            return Ok(saleDto);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPatch("cancel/{saleNumber}")]
        public async Task<IActionResult> CancelSale(int saleNumber)
        {
            var sale = await _saleService.CancelSale(saleNumber);

            if (sale == null)
                return NotFound();

            var saleDto = new SaleDto
            {
                SaleNumber = sale.SaleNumber,
                Date = sale.SaleDate,
                CustomerId = sale.CustomerId,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled,
                SaleItems = sale.Items.Select(item => new SaleItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalAmount = item.TotalAmount
                }).ToList(),
                Discount = sale.Discount,
                TotalSaleAmount = sale.TotalSaleAmount
            };

            return Ok(saleDto);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPut("{saleNumber}")]
        public async Task<ActionResult<SaleDto>> UpdateSale(int saleNumber, [FromBody] CreateSaleDto updateSaleDto)
        {
            var sale = await _saleRepository.GetSaleBySaleNumber(saleNumber);

            if (sale == null)
                return NotFound();

            sale.CustomerId = updateSaleDto.CustomerId;
            sale.Branch = updateSaleDto.Branch;

            if (sale.CartId != updateSaleDto.CartId)
            {
                sale.Items.Clear();

                var cart = await _cartService.GetCartById(updateSaleDto.CartId);
                if (cart == null)
                    return NotFound("Cart not found");

                foreach (var cp in cart.Products)
                {
                    var product = await _productRepository.GetProductById(cp.ProductId);
                    if (product != null)
                    {
                        var saleItem = new SaleItem
                        {
                            ProductId = cp.ProductId,
                            ProductName = product.Title,
                            Quantity = cp.Quantity,
                            UnitPrice = product.Price,
                            TotalAmount = cp.Quantity * product.Price
                        };
                        sale.Items.Add(saleItem);
                    }
                    else
                    {
                        _logger.LogWarning($"Product with ID {cp.ProductId} not found");
                        continue;
                    }
                }
            }

            var updatedSale = await _saleService.UpdateSale(sale);

            var saleDto = new SaleDto
            {
                SaleNumber = updatedSale.SaleNumber,
                Date = updatedSale.SaleDate,
                CustomerId = updatedSale.CustomerId,
                Branch = updatedSale.Branch,
                IsCancelled = updatedSale.IsCancelled,
                SaleItems = updatedSale.Items.Select(item => new SaleItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalAmount = item.TotalAmount
                }).ToList(),
                Discount = updatedSale.Discount,
                TotalSaleAmount = updatedSale.TotalSaleAmount
            };

            return Ok(saleDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{saleNumber}")]
        public async Task<IActionResult> DeleteSale(int saleNumber)
        {
            var sale = await _saleRepository.GetSaleBySaleNumber(saleNumber);

            if (sale == null)
                return NotFound();

            await _saleService.DeleteSale(saleNumber);

            return NoContent();
        }


    }
} 