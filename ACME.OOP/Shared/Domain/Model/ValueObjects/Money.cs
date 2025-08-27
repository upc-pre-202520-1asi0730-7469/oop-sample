namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount and currency.
/// </summary>
public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Money"/> Value Object.
    /// </summary>
    /// <param name="amount">the monetary amount, must be greater than or equal to zero.</param>
    /// <param name="currency">the currency code, must be a valid 3-letter ISO code.</param>
    /// <exception cref="ArgumentException">Thrown when amount is negative or currency is invalid.</exception>
    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency));
        if (amount < 0)
            throw new ArgumentException("Amount must be greater than or equal to zero.", nameof(amount));
        Amount = amount;
        Currency = currency.ToUpper();
    }
    
    /// <summary>
    /// Returns a string representation of the Money object.
    /// </summary>
    /// <returns>A string in the format "Amount Currency".</returns>
    public override string ToString() => $"{Amount} {Currency}";
}