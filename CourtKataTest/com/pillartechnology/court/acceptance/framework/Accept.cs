﻿
using Microsoft.AspNetCore.TestHost;

namespace CourtKataTest.com.pillartechnology.court.acceptance.framework
{
    public class Accept
    {
        private const string ROOT_URL = "https://localhost:5001/court";
        private readonly CaseSubmitter _submitter;
        private readonly CaseRetriever _retriever;
        
        public Accept(TestServer testServer)
        {
            _retriever = new CaseRetriever(testServer, ROOT_URL);
            _submitter = new CaseSubmitter(testServer, ROOT_URL);
        }

        public Accept Given()
        {
            return this;
        }

        public CaseRetriever CaseFileDoesNotExit()
        {
            
            return _retriever;
        }

        public Accept When()
        {
            return this;
        }

        public CaseSubmitter NewCaseFileIsSubmitted()
        {
            return _submitter;
        }

        public Accept Then()
        {
            return this;
        }

        public CaseRetriever TheDatabaseIsUpdatedWithTheCaseFile()
        {
            return _retriever;
        }

        public CaseRetriever UserRequestsTheCaseFile()
        {
            return _retriever;
        }

        public void ResourceNotFoundResponseIsReturned()
        {
            _retriever.VerifyResourceNotFound();
        }

        public CaseSubmitter AnExistingCaseFile()
        {
            return _submitter;
        }

        public void TheCaseFileIsReturned()
        {
            _retriever.Verify(1);
        }
    }
}