﻿using Azure;
using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        private readonly string _url;
        private readonly ILogger _logger;
        HttpClient client = new HttpClient();
        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            _url = url;
            _logger = logger;
        }
        async Task<List<string>> GetTradeAsync()
        {
            _logger.LogInfo("Connecting to the Restful server using HTTP");
            List<string> tradesString = null;

            HttpResponseMessage response = await client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                tradesString = JsonSerializer.Deserialize<List<string>>(content);
                _logger.LogInfo("Received trade strings of length = " + tradesString.Count);
            }
            return tradesString;
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<List<string>> task = Task.Run(() => GetTradeAsync());
            task.Wait();

            List<string> tradeList = task.Result;
            return tradeList;
        }
    }

}