using System;

namespace CodeChallenge.Models
{
    public class Task3ViewModel
    {
        public int? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        // Note from RexGex: we *may* need this later. I don't know...
        public string UserKeyForEmployeeLookUp
        {
            get
            {
                if (UserId.HasValue && !string.IsNullOrEmpty(UserFirstName) && !string.IsNullOrEmpty(UserLastName))
                {
                    return $"{UserId.Value}-{UserFirstName}-{UserLastName}";
                }

                return "";
            }
        }

        public string UserHash
        {
            get
            {
                if (string.IsNullOrEmpty(UserKeyForEmployeeLookUp))
                {
                    return "";
                }

                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(UserKeyForEmployeeLookUp));
            }
        }
    }
}
