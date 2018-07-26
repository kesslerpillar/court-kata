using System;
using CourtApi.com.pillartechnology.court;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourtKataTest.com.pillartechnology.court
{
    [TestClass]
    public class CaseRepositoryTest
    {
        private readonly DateTime _openDateOne = DateTime.Now;
        private readonly DateTime _openDateTwo = DateTime.Now;
        private readonly DateTime _openDateThree = DateTime.Now;

        [TestMethod]
        public void FindAll()
        {
            var dbContext = SetUpThreeCases("test-find-all");
            
            using (var context = new CaseContext(dbContext))
            {
                var repository = new CaseRepository(context);

                var result = repository.FindAll();
                
                Assert.AreEqual(3, result.Count);
            }
        }

        [TestMethod]
        public void Save()
        {
            var dbContext = SetUpThreeCases("test-save");

            using (var context = new CaseContext(dbContext))
            {
                var repository = new CaseRepository(context);

                repository.Save(new Case
                {
                    Id = "4",
                    DocketNumber =  "test docket number four",
                    Title = "test title four",
                    Description = "test description four",
                    OpenDate = DateTime.Now
                });
                
                var result = repository.FindAll();
                
                Assert.AreEqual(4, result.Count);
            }
        }

        [TestMethod]
        public void Delete()
        {
            var dbContext = SetUpThreeCases("test-delete");

            using (var context = new CaseContext(dbContext))
            {
                var repository = new CaseRepository(context);
                
                var before = repository.FindAll();
                
                Assert.AreEqual(3, before.Count);

                repository.Delete("test docket number two");
         
                var after = repository.FindAll();
                
                Assert.AreEqual(2, after.Count);

            }
        }

        [TestMethod]
        public void FindByDocketNumber()
        {
            var dbContext = SetUpThreeCases("test-find-by-docket-number");

            using (var context = new CaseContext(dbContext))
            {
                var repository = new CaseRepository(context);
         
                var result = repository.FindByDocketNumber("test docket number two");
                
                Assert.AreEqual(1, result.Count);

                Assert.AreEqual("2", result[0].Id);
                Assert.AreEqual("test docket number two", result[0].DocketNumber);
                Assert.AreEqual("test title two", result[0].Title);
                Assert.AreEqual("test description two", result[0].Description);
                Assert.AreEqual(_openDateTwo, result[0].OpenDate);

            }
        }
        
        private DbContextOptions<CaseContext> SetUpThreeCases(string databaseName)
        {
            var dbContext = new DbContextOptionsBuilder<CaseContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            
            using (var context = new CaseContext(dbContext))
            {
                var repository = new CaseRepository(context);

                repository.Save(new Case
                {
                    Id = "1",
                    DocketNumber =  "test docket number one",
                    Title = "test title one",
                    Description = "test description one",
                    OpenDate = _openDateOne
                });
                
                repository.Save(new Case
                {
                    Id = "2",
                    DocketNumber =  "test docket number two",
                    Title = "test title two",
                    Description = "test description two",
                    OpenDate = _openDateTwo
                });
                
                repository.Save(new Case
                {
                    Id = "3",
                    DocketNumber =  "test docket number three",
                    Title = "test title three",
                    Description = "test description three",
                    OpenDate = _openDateThree
                });
            }

            return dbContext;
        }
    }
}