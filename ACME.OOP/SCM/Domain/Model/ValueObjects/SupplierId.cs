namespace ACME.OOP.SCM.Domain.Model.ValueObjects;

/// <summary>
/// Value Object representing a Supplier Identifier in the Supply Chain Management bounded context.
/// </summary>
///
/// <remarks>
/// This class encapsulates the unique identifier for a supplier, ensuring immutability and value-based equality.
/// It is designed to be used within the Supply Chain Management domain to uniquely identify suppliers.
/// </remarks>
public record SupplierId
{
    public string Identifier { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SupplierId"/> class.
    /// </summary>
    /// <param name="identifier">The unique identifier for the supplier.</param>
    /// <exception cref="ArgumentException">Thrown when the identifier is null or whitespace.</exception>
    public SupplierId(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Supplier Id cannot be null or whitespace.", nameof(identifier));
        Identifier = identifier;
    }
}