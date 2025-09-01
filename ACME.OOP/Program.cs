using ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

var supplierAddress = new Address("Supplier Street", "12345", "Supplier City", 
    "Supplier Region", "Supplier Postal Code", "Supplier Country");

var supplier = new Supplier("SP001", "Supplier Name", supplierAddress);

var purchaseOrder = new PurchaseOrder("PO001", new SupplierId(supplier.Identifier), DateTime.Now, "USD");
purchaseOrder.AddItem(ProductId.New(), 10, 25.99m)
             .AddItem(ProductId.New(), 20, 19.99m);

Console.Write($"Purchase Order ID: {purchaseOrder.OrderNumber} ");
Console.Write($"created for Supplier ID: {purchaseOrder.SupplierId.Identifier} ");
Console.WriteLine($"in {purchaseOrder.Currency}");
foreach (var item in purchaseOrder.Items) 
    Console.WriteLine($"Order Item Total: {item.CalculateItemTotal()}");
Console.WriteLine($"Order Total: {purchaseOrder.CalculateOrderTotal()}");
    