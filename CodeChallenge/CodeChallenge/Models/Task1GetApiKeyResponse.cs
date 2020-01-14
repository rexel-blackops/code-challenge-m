using System;

namespace CodeChallenge.Models
{
    // #Note from RexGex: I'm not sure if this class matches exactly to the JSON response from the API provided by our mole. Please verify and update as necessary.
    public class Task1GetApiKeyResponse
    {
        public DateTime ExpirationDate { get; set; }        
        public string ApiKey { get; set; }
    }
}
