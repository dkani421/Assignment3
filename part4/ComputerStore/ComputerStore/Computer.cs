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

    public decimal GetTotalPrice()
    {
        return Price;
    }
}
