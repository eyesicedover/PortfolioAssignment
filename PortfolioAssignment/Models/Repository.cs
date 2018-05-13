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
            var request = new RestRequest("search/repositories?q=user:eyesicedover&sort=stars&order=desc");
            var response = new RestResponse();

            request.AddHeader("User-Agent", "eyesicedover");

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

            var repos = JsonConvert.DeserializeObject<List<Repository>>(jsonResponse["items"].ToString());
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
