using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Text;
using CourtApi.com.pillartechnology.court;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CourtKataTest.com.pillartechnology.court.acceptance.framework
{
    public class CaseSubmitter
    {
        private string _docketNumber;
        private string _title;
        private string _description;
        private readonly string _rootUrl;
        private readonly HttpClient _httpClient;

        public CaseSubmitter(string rootUrl)
        {
            _rootUrl = rootUrl;
            _httpClient = new HttpClient();
        }

        public CaseSubmitter WithDocketNumber(string docketNumber)
        {
            _docketNumber = docketNumber;
            return this;
        }

        public CaseSubmitter WithTile(string title)
        {
            _title = title;
            return this;
        }

        public CaseSubmitter AndDescription(string description)
        {
            _description = description;
            return this;
        }

        public void Successfully()
        {
            var @case = new Case
            {
                DocketNumber = _docketNumber,
                Title = _title,
                Description = _description,
                OpenDate = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(@case);
            
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = _httpClient.PostAsync(_rootUrl + "/docket", stringContent).Result;
            
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);
        }
    }
}