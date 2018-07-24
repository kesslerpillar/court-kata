using System;
using CourtKataTest.com.pillartechnology.court.acceptance.framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourtKataTest.com.pillartechnology.court.acceptance
{
    [TestClass]
    public class CourtAdministratorScenarios
    {
        [TestMethod]
        public void AsCourtAdministrator_IWantToAddNewCaseFile_SoThatTheCourtCanTrackIt()
        {
            var accept = new Accept();
            
            accept.Given().CaseFileDoesNotExit()
                .WithDocketNumber("testDocketNumber")
                .Delete()
                .Verify(0);
            
            accept.When().NewCaseFileIsSubmitted()
                .WithDocketNumber("testDocketNumber")
                .WithTile("testTitle")
                .AndDescription("testDescription")
                .Successfully();

            accept.Then().TheDatabaseIsUpdatedWithTheCaseFile()
                .WithDocketNumber("testDocketNumber")
                .WithTitle("testTitle")
                .WithDescription("testDescription")
                .WithOpenDateBetween(DateTime.Today, DateTime.Now)
                .Verify(1);
        }

    }
}