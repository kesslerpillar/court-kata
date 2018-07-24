using System;
using System.Collections.Generic;
using System.Net.Http;
using CourtApi.com.pillartechnology.court;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CourtKataTest.com.pillartechnology.court.acceptance.framework
{
    public class CaseRetriever
    {
        private string _title;
        private string _description;
        private string _docketNumber;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private readonly string _rootUrl;
        private readonly HttpClient _httpClient;

        public CaseRetriever(string rootUrl)
        {
            _rootUrl = rootUrl;
            _httpClient = new HttpClient();
        }
        
        public CaseRetriever WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public CaseRetriever WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CaseRetriever WithDocketNumber(string docketNumber)
        {
            _docketNumber = docketNumber;
            return this;
        }

        public CaseRetriever WithOpenDateBetween(DateTime startDateTime, DateTime endDateTime)
        {
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            return this;
        }

        public void Verify(int expectedCaseCount)
        {
            var response = _httpClient.GetAsync(_rootUrl + "/docket/" + _docketNumber).Result;
       
            Assert.IsTrue(response.IsSuccessStatusCode);

            var result = response.Content.ReadAsStringAsync().Result;

            var cases = JsonConvert.DeserializeObject<IList<Case>>(result);
            
            Assert.AreEqual(expectedCaseCount, cases.Count);

            foreach (var @case in cases)
            {
                Assert.AreEqual(_title, @case.Title);
                Assert.AreEqual(_description, @case.Description);
                Assert.AreEqual(_docketNumber, @case.DocketNumber);
                Assert.IsTrue(@case.OpenDate > _startDateTime && @case.OpenDate < _endDateTime);
            }
        }

        public CaseRetriever Delete()
        {
            var httpResponseMessage = _httpClient.DeleteAsync(_rootUrl + "/docket/" + _docketNumber).Result;
            
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            return this;
        }
    }
}