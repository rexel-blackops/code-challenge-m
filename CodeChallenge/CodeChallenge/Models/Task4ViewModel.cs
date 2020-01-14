using System;

namespace CodeChallenge.Models
{
    public class Task4ViewModel
    {
        public int? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserImageUrl { get; set; }
        public string UserJobTitle { get; set; }
        public string UserDepartmentCode { get; set; }
        public double? LastUserLocationLat { get; set; }
        public double? LastUserLocationLon { get; set; }
        public DateTime? HiredDate { get; set; }
        public string LastLatLon
        {
            get
            {
                if (LastUserLocationLat.HasValue && LastUserLocationLon.HasValue)
                {
                    return LastUserLocationLat.ToString() + "," + LastUserLocationLon.ToString();
                }
                return "Unknown";
            }
        }
    }
}
