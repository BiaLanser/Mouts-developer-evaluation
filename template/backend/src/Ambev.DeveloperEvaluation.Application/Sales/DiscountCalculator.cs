using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class DiscountCalculator
    {
        public (decimal totalSaleAmount, decimal totalDiscount) Calculate(IEnumerable<SaleItem> saleItems)
        {
            if (saleItems == null || !saleItems.Any())
            {
                throw new ArgumentException("Sale items cannot be null or empty", nameof(saleItems));
            }

            decimal totalSaleAmount = 0;
            decimal totalDiscount = 0;

            foreach (var saleItem in saleItems)
            {
                decimal itemTotalAmount = saleItem.Quantity * saleItem.UnitPrice;

                if (saleItem.Quantity >= 4 && saleItem.Quantity <= 9)
                {
                    saleItem.TotalAmount = itemTotalAmount * 0.9m;
                    totalDiscount += itemTotalAmount * 0.1m;
                }
                else if (saleItem.Quantity >= 10 && saleItem.Quantity <= 20)
                {
                    saleItem.TotalAmount = itemTotalAmount * 0.8m;
                    totalDiscount += itemTotalAmount * 0.2m;
                }
                else
                {
                    saleItem.TotalAmount = itemTotalAmount;
                }

                totalSaleAmount += saleItem.TotalAmount;
            }

            return (Math.Round(totalSaleAmount, 2), Math.Round(totalDiscount, 2));
        }
    }
}
