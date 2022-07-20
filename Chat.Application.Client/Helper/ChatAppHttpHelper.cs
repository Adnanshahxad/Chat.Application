using System.Threading.Tasks;
using Domain.Configs;
using Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Chat.Application.Client.Helper;

public class WebServiceHttpHelper
{
    public WebServiceHttpHelper(IOptions<ClientAppSettings> settings)
    {
        RestClient = new RestClient(settings.Value.ChatAppWebServiceUrl);
    }

    private IRestClient RestClient { get; }

    public async Task<ResultModel> LoginAsync(object model)
    {
        var request = CreateRequest("/api/accounts/login");

        request.AddJsonBody(model, "application/json");
        request.Method = Method.POST;
        var response = await RestClient.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<ResultModel>(response.Content);
    }

    public async Task<ResultModel> RegisterAsync(object model)
    {
        var request = CreateRequest("/api/accounts/register");

        request.AddJsonBody(model, "application/json");
        request.Method = Method.POST;
        var response = await RestClient.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<ResultModel>(response.Content);
    }

    private static IRestRequest CreateRequest(string resource)
    {
        return new RestRequest(resource).AddHeader("Accept", "application/json");
    }
}