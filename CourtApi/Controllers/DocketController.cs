using System.Collections.Generic;
using System.Net;
using CourtApi.com.pillartechnology.court;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CourtApi.Controllers
{
    [Route("court/[controller]")]
    [ApiController]
    public class DocketController : ControllerBase
    {
        private readonly CasePersistable _repository;

        public DocketController(CasePersistable repository)
        {
            _repository = repository;
        }

        [HttpGet("{docketNumber}")]
        public ActionResult<IList<Case>> LookupByDocketNumber(string docketNumber)
        {
            var lookupByDocketNumber = (List<Case>) _repository.FindByDocketNumber(docketNumber);
            if (lookupByDocketNumber.Count > 0)
            {
                return lookupByDocketNumber;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{docketNumber}")]
        public void RemoveByDocketNumber(string docketNumber)
        {
            _repository.Delete(docketNumber);
        }
        
        [HttpGet]
        public ActionResult<IList<Case>> LookupAll()
        {
            return (List<Case>) _repository.FindAll();
        }
        
        [HttpPost]
        public void Add(Case @case)
        {
            _repository.Save(@case);
        }

    }
}