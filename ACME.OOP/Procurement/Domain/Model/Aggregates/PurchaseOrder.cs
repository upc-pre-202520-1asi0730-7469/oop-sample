using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents a purchase order aggregate root for the Procurement bounded context.
/// </summary>
/// <remarks>
/// A purchase order is created by a buyer to request products from a supplier. It contains details such as the order number, supplier information, order date, currency, and a list of ordered items.
/// Each item in the order is represented by a <see cref="PurchaseOrderItem"/> which includes the product ID, quantity, and unit price.
/// The aggregate can calculate the total order amount by summing the totals of all its items.
/// </remarks>
/// <param name="orderNumber">The unique order number for the purchase order.</param>
/// <param name="supplierId">The <see cref="SupplierId"/> identifier of the supplier.</param>
/// <param name="orderDate">The date the order was placed.</param>
/// <param name="currency">The currency code (ISO 4217) for the order, e.g., "USD".</param>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="orderNumber"/> or <paramref name="supplierId"/> is null.</exception>
/// <exception cref="ArgumentException">Thrown if <paramref name="currency"/> is not a valid 3-letter ISO code.</exception>
public class PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    private readonly List<PurchaseOrderItem> _items = new();
    
    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentNullException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3
                                      ? throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency))
                                      : currency.ToUpper();

    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Adds a new item to the purchase order.
    /// </summary>
    /// <param name="productId">The <see cref="ProductId"/> of the product being ordered.</param>
    /// <param name="quantity">The quantity of the product being ordered. Must be greater than zero.</param>
    /// <param name="unitPriceAmount">The unit price of the product as a decimal amount. Must be non-negative.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public PurchaseOrder AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (unitPriceAmount < 0) throw new ArgumentOutOfRangeException(nameof(unitPriceAmount), "Unit price must be a non-negative value.");
        
        var unitPrice = new Money(unitPriceAmount, Currency);
        var newItem = new PurchaseOrderItem(productId, quantity, unitPrice);
        _items.Add(newItem);
        return this;
    }

    /// <summary>
    /// Calculates the total amount for the entire purchase order by summing the totals of all items.
    /// </summary>
    /// <returns>A <see cref="Money"/> value object representing the total amount for the purchase order with the appropriate currency.</returns>
    public Money CalculateOrderTotal()
    {
        var totalAmount = _items.Sum(item => item.CalculateItemTotal().Amount);
        return new Money(totalAmount, Currency);
    }
}