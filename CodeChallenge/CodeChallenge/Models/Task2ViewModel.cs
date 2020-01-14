using System.Collections.Generic;

namespace CodeChallenge.Models
{
    public class Task2ViewModel
    {
        public IEnumerable<Asset> Top4MostValuableAssets { get; set; }
        public IEnumerable<Asset> Top4MostQuantityAssets { get; set; }
    }
}
