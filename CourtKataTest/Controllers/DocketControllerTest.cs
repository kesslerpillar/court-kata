using System.Collections.Generic;
using CourtApi.com.pillartechnology.court;
using CourtApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CourtKataTest.Controllers
{
    [TestClass]
    public class DocketControllerTest
    {
        private DocketController _controller;
        private List<Case> _cases;
        private Mock<CasePersistable> _mockPersistable;

        [TestInitialize]
        public void SetUp()
        {
            _mockPersistable = new Mock<CasePersistable>();
            _controller = new DocketController(_mockPersistable.Object);

            _cases = new List<Case> ();

            _mockPersistable.Setup(p => p.FindByDocketNumber("testDocketNumber")).Returns(_cases);
            _mockPersistable.Setup(p => p.FindAll()).Returns(_cases);
        }
        
        [TestMethod]
        public void LookupByDocketNumber()
        {
            _cases.Add(new Case());
            
            var result = _controller.LookupByDocketNumber("testDocketNumber");
            
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, result.Value.Count);
        }
        
        [TestMethod]
        public void RemoveByDocketNumber()
        {
            _cases.Add(new Case
            {
                DocketNumber = "testDocketNumber"
            });
            
            _controller.RemoveByDocketNumber("testDocketNumber");
            
            _mockPersistable.Verify(p => p.Delete("testDocketNumber"));
        }
        
        [TestMethod]
        public void LookupAll()
        {
            _cases.Add(new Case
            {
                DocketNumber = "testDocketNumberOne"
            });
            
            _cases.Add(new Case
            {
                DocketNumber = "testDocketNumberTwo"
            });

            var result = _controller.LookupAll();
            
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, result.Value.Count);
        }
        
        [TestMethod]
        public void Add()
        {
            var @case = new Case();
            
            _controller.Add(@case);
            
            _mockPersistable.Verify(p => p.Save(@case));
        }
    }
}