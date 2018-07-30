using System;
using CourtApi;
using CourtKataTest.com.pillartechnology.court.acceptance.framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
                .WithDocketNumber("testDocketNumber")
                .Delete()
                .VerifyResourceNotFound();
            
            _accept.When().NewCaseFileIsSubmitted()
                .WithDocketNumber("testDocketNumber")
                .WithTile("testTitle")
                .AndDescription("testDescription")
                .Successfully();

            _accept.Then().TheDatabaseIsUpdatedWithTheCaseFile()
                .WithDocketNumber("testDocketNumber")
                .WithTitle("testTitle")
                .WithDescription("testDescription")
                .WithOpenDateBetween(DateTime.Today, DateTime.Now)
                .Verify(1);
        }
        
        [TestMethod]
        public void AsCourtAdministrator_IWantToRetrieveCaseFiles_SoThatCasesCanBeUpdated()
        {
            _accept.Given().AnExistingCaseFile()
                .WithDocketNumber("testDocketNumber")
                .Add();

            _accept.When().UserRequestsTheCaseFile()
                .WithDocketNumber("testDocketNumber");

            _accept.Then().TheCaseFileIsReturned();
        }
        
        [TestMethod]
        public void AsCourtAdministrator_IWantClearResponseMessages_SoThatSystemsCanHandleMissingData()
        {
            _accept.Given().CaseFileDoesNotExit()
                .WithDocketNumber("testDocketNumber")
                .Delete()
                .VerifyResourceNotFound();

            _accept.When().UserRequestsTheCaseFile()
                .WithDocketNumber("testDocketNumber");
                
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