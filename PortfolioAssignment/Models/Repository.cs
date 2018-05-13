using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace PortfolioAssignment.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Html_Url { get; set; }

        public static List<Repository> GetRepositories()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("users/eyesicedover/starred?direction=asc&per_page=3");
            var response = new RestResponse();

            request.AddHeader("User-Agent", "eyesicedover");

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);

            var repos = JsonConvert.DeserializeObject<List<Repository>>(jsonResponse.ToString());
            return repos;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
