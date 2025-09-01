using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.SCM.Domain.Model.Aggregates;

/// <summary>
/// Supplier Aggregate Root for the Supply Chain Management (SCM) bounded context.
/// </summary>
/// <remarks>
/// The Supplier aggregate root represents a supplier entity within the SCM domain.
/// It encapsulates the supplier's identity, name, and address.
/// The Supplier aggregate is responsible for managing its own state and ensuring
/// the integrity of its invariants.
/// </remarks>
/// <param name="identifier">The unique identifier for the supplier.</param>
/// <param name="name">The name of the supplier.</param>
/// <param name="address">The <see cref="Address"/> value object representing the supplier's address.</param>
public class Supplier(string identifier, string name, Address address)
{
    public string Identifier { get; } = identifier ?? throw new ArgumentNullException(nameof(identifier));
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
    public Address Address { get; } = address ?? throw new ArgumentNullException(nameof(address));
}
