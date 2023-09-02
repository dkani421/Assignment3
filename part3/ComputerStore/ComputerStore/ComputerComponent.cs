using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerStore
{
    public class ComputerComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PriceOption> PriceOptions { get; set; } = new List<PriceOption>();
    }
    public class PriceOption
    {
        public string OptionName { get; set; }
        public decimal Price { get; set; }
    }
}