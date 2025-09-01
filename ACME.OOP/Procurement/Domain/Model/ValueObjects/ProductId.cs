namespace ACME.OOP.Procurement.Domain.Model.ValueObjects;

/// <summary>
/// Value object representing a unique identifier for a product.
/// </summary>
public record ProductId
{
    public Guid Id { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="ProductId"/> with the specified GUID.
    /// </summary>
    /// <param name="id">The GUID representing the product ID.</param>
    /// <exception cref="ArgumentException">Thrown when the provided GUID is empty.</exception>
    public ProductId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Product ID cannot be an empty GUID.", nameof(id));
        Id = id;
    }
    
    /// <summary>
    /// Generates a new <see cref="ProductId"/> with a new GUID.
    /// </summary>
    /// <returns>A new instance of <see cref="ProductId"/>.</returns>
    public static ProductId New() => new (Guid.NewGuid());
    
    /// <summary>
    /// Returns the string representation of the product ID.
    /// </summary>
    /// <returns>The string representation of the GUID.</returns>
    public override string ToString() => Id.ToString();
}