using ComputerStore;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Computer
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } 
    public string ImagePath { get; set; }

    // Updated properties for customization
    public List<ComputerComponent> Components { get; set; } = new List<ComputerComponent>();

    public void SetComponent(ComputerComponent component)
    {
        // Update or add the selected component
        Components.RemoveAll(c => c.Name == component.Name);
        Components.Add(component);
    }

    public decimal GetTotalPrice()
    {
        // Calculate the total price based on selected components and their selected price options
        decimal totalPrice = Price;

        foreach (var component in Components)
        {
            var selectedOption = component.PriceOptions.FirstOrDefault(option => option.Selected);
            if (selectedOption != null)
            {
                totalPrice += selectedOption.Price;
            }
        }

        return totalPrice;
    }
}
