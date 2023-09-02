using ComputerStore;
using System.Collections.Generic;

public class Computer
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } // Change the data type to decimal
    public string ImagePath { get; set; }

    // Updated properties for customization
    public List<ComputerComponent> Components { get; set; } = new List<ComputerComponent>();

    public void SetComponent(ComputerComponent component)
    {
        // Update or add the selected component
        Components.RemoveAll(c => c.Name == component.Name);
        Components.Add(component);
    }

    public decimal GetTotalPrice() // Change the return type to decimal
    {
        // Calculate the total price based on selected components
        decimal totalPrice = Price;
        foreach (var component in Components)
        {
            //totalPrice += component.Price;
        }
        return totalPrice;
    }
}
