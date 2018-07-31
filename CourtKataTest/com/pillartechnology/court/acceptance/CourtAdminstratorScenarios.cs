using System;
using System.IO;
using System.Reflection;
using CourtApi;
using CourtKataTest.com.pillartechnology.court.acceptance.framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourtKataTest.com.pillartechnology.court.acceptance
{
    
    [TestClass]
    public class CourtAdministratorScenarios
    {
        private TestServer _server;
        private Accept _accept;

        [TestInitialize]
        public void SetUp()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            
            _accept = new Accept(_server);
        }

        [TestMethod]
        public void AsCourtAdministrator_IWantToAddNewCaseFile_SoThatTheCourtCanTrackIt()
        {    
            _accept.Given().CaseFileDoesNotExit()
                .WithDocketNumber("testDocketNumber1")
                .Delete()
                .VerifyResourceNotFound();
            
            _accept.When().NewCaseFileIsSubmitted()
                .WithDocketNumber("testDocketNumber1")
                .WithTile("testTitle")
                .AndDescription("testDescription")
                .Successfully();

            _accept.Then().TheDatabaseIsUpdatedWithTheCaseFile()
                .WithDocketNumber("testDocketNumber1")
                .WithTitle("testTitle")
                .WithDescription("testDescription")
                .WithOpenDateBetween(DateTime.Today, DateTime.Now)
                .Verify(1);
        }
        
        [TestMethod]
        public void AsCourtAdministrator_IWantToRetrieveCaseFiles_SoThatCasesCanBeUpdated()
        {
            _accept.Given().AnExistingCaseFile()
                .WithDocketNumber("testDocketNumber2")
                .Add();

            _accept.When().UserRequestsTheCaseFile()
                .WithDocketNumber("testDocketNumber2");

            _accept.Then().TheCaseFileIsReturned();
        }
        
        [TestMethod]
        public void AsCourtAdministrator_IWantClearResponseMessages_SoThatSystemsCanHandleMissingData()
        {
            _accept.Given().CaseFileDoesNotExit()
                .WithDocketNumber("testDocketNumber3")
                .Delete()
                .VerifyResourceNotFound();

            _accept.When().UserRequestsTheCaseFile()
                .WithDocketNumber("testDocketNumber3");
                
            _accept.Then()
                .ResourceNotFoundResponseIsReturned();
        }
        
        [TestCleanup]
        public void TearDownClass()
        {
            _server.Dispose();
        }

    }
}