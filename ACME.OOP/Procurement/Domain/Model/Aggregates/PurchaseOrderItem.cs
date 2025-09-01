using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// Represents an item in a purchase order aggregate for the Procurement bounded context.
/// </summary>
/// <param name="productId">The <see cref="ProductId"/> identifier of the product being ordered.</param>
/// <param name="quantity">The quantity of the product being ordered. Must be greater than zero.</param>
/// <param name="unitPrice">The unit price of the product as a <see cref="Money"/> value object.</param>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="productId"/> or <paramref name="unitPrice"/> is null.</exception>
/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="quantity"/> is less than or equal to zero.</exception>
public class PurchaseOrderItem(ProductId productId, int quantity, Money unitPrice)
{
    public ProductId ProductId { get; } = productId ?? throw new ArgumentNullException(nameof(productId));
    public int Quantity { get; } = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity));
    public Money UnitPrice { get; } = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));

    /// <summary>
    /// Calculates the total price for this purchase order item by multiplying the unit price by the quantity.
    /// </summary>
    /// <returns>A <see cref="Money"/> value object representing the total price for this item.</returns>
    public Money CalculateItemTotal() => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}