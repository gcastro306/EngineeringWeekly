using EngineeringWeekly.API.Componets.Interfaces;
using EngineeringWeekly.DTOS;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringWeekly.API.Componets
{
    public class ChuckNorrisJokeGetter : IChuckNorrisJokeGetter
    {
        ILogger<IChuckNorrisJokeGetter> Logger { get; }
        ExternalAPIUrs ExternalAPIUrs { get; }
        string ErrorMessageBase = $"{nameof(ChuckNorrisJokeGetter)}::";
        public ChuckNorrisJokeGetter(ILogger<IChuckNorrisJokeGetter> logger, IOptions<ExternalAPIUrs> externalAPIUrs) 
        {
            Logger = logger;
            ExternalAPIUrs = externalAPIUrs.Value;
        }

        public async Task<Result<string>> GetJoke(string? category)
        {
            try
            {
                var url = string.IsNullOrWhiteSpace(category) ? $"{ExternalAPIUrs.ChuckNorrisAPIURL!}random" : $"{ExternalAPIUrs.ChuckNorrisAPIURL!}random?category={category}";
                var apiClient = new RestClient(url);
                var response = await apiClient.GetAsync(GetRequest());

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var joke = JsonConvert.DeserializeObject<ChuckNorrisJoke> (response.Content);
                        return Result.Ok(joke!.Value!);
                    }
                    else
                    {
                        var errorMessage = $"{ErrorMessageBase}{nameof(GetJoke)}:There are no jokes available";
                        Logger.LogError(errorMessage);
                        return Result.Fail(errorMessage);
                    }
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ChuckNorrisJokeError>(response.Content!);
                    var errorMessage = $"{ErrorMessageBase}{nameof(GetJoke)}:{error.Error}:{error.Message}";
                    Logger.LogError(errorMessage);
                    return Result.Fail(errorMessage);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"{ex.Message}";
                Logger.LogError(errorMessage);
                return Result.Fail(errorMessage);
            }
        }

        public async Task<Result<List<string>>> GetJokeCategories()
        {
            try
            {
                var apiClient = new RestClient($"{ExternalAPIUrs.ChuckNorrisAPIURL!}categories");
                var response = await apiClient.GetAsync(GetRequest());

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content)) {
                        var categories = JsonConvert.DeserializeObject<List<string>>(response.Content);
                        return Result.Ok(categories!);
                    }
                    else
                    {
                        var errorMessage = $"{ErrorMessageBase}{nameof(GetJoke)}:There are no jokes available";
                        Logger.LogError(errorMessage);
                        return Result.Fail(errorMessage);
                    }
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ChuckNorrisJokeError>(response.Content!);
                    var errorMessage = $"{ErrorMessageBase}{nameof(GetJoke)}:{error.Error}:{error.Message}";
                    Logger.LogError(errorMessage);
                    return Result.Fail(errorMessage);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"{ex.Message}";
                Logger.LogError(errorMessage);
                return Result.Fail(errorMessage);
            
            }
        }

        private static RestRequest GetRequest() => new RestRequest()
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
    }
}
