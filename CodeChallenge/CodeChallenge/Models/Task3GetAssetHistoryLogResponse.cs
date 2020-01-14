using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Task3GetAssetHistoryLogResponse
    {
        public DateTime ChangedDateTime { get; set; }
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
