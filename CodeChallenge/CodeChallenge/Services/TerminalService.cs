using CodeChallenge.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public class TerminalService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _moleApiEndpoint;
        private readonly string _assetsApiEndpoint;
        private readonly string _assetsHistoryApiEndpoint;
        private readonly string _userInfoApiEndpoint;

        public TerminalService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _moleApiEndpoint = config.GetSection("ApiEndpoints").GetValue<string>("Mole");
            _assetsApiEndpoint = config.GetSection("ApiEndpoints").GetValue<string>("Assets");
            _assetsHistoryApiEndpoint = config.GetSection("ApiEndpoints").GetValue<string>("AssetsHistoryLog");
            _userInfoApiEndpoint = config.GetSection("ApiEndpoints").GetValue<string>("UserInfoApi");
        }

        public async Task<string> RetrieveApiKey(string secretToken)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_moleApiEndpoint, secretToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new InvalidOperationException("Unable to connect with the mole");
            }
        }

        public async Task<IReadOnlyList<Asset>> RetrieveAssetList(string apiKey)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_assetsApiEndpoint, apiKey);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IReadOnlyList<Asset>>();
            }
            else
            {
                throw new InvalidOperationException("Unable to retrieve the list of assets");
            }
        }

        // Note from RexGex: At the time I'm writing this, I don't know what the return will look like, so I'll just return JSON string for now
        public async Task<string> RetrieveAssetHistoryLog(string apiKey, long? assetId)
        {
            if (!assetId.HasValue)
            {
                throw new InvalidOperationException("Invalid asset ID");
            }

            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_assetsHistoryApiEndpoint, new { apiKey, assetId });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new InvalidOperationException("Unable to retrieve the history log");
            }
        }

        public async Task<Task4GetUserInfoResponse> RetrieveSuspectInfo(string accessCode, string userKey)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_userInfoApiEndpoint, new { accessCode, userKey });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Task4GetUserInfoResponse>();
            }
            else
            {
                throw new InvalidOperationException("Unable to retrieve suspect info");
            }
        }

    }
}
