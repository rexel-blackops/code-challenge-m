using System;

namespace CodeChallenge.Models
{
    // Note from RexGex: These fields should match exactly with their API, but you may need to recheck as time goes by
    public class Task4GetUserInfoResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentCode { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? HiredDate { get; set; }
        public double? LastUserLocationLat { get; set; }
        public double? LastUserLocationLon { get; set; }
    }
}
