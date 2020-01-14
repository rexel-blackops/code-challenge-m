using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Asset
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public int? Quantity { get; set; }
    }
}
