using ComputerStore;
using System.Collections.Generic;
using System;

public class Computer
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string ImagePath { get; set; }

    // Updated properties for customization
    public List<ComputerComponent> Components { get; set; } = new List<ComputerComponent>();

    public void SetComponent(ComputerComponent component)
    {
        // Update or add the selected component
        Components.RemoveAll(c => c.Name == component.Name);
        Components.Add(component);
    }

    public int GetTotalPrice()
    {
        // Calculate the total price based on selected components
        int totalPrice = Price;
        foreach (var component in Components)
        {
            totalPrice += (int)Math.Round(component.Price);
        }
        return totalPrice;
    }
}
