using System;
using System.Collections.Generic;
using System.Linq;
using CourtApi.com.pillartechnology.court;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;

namespace CourtApi.Controllers
{
    [Route("court/[controller]")]
    [ApiController]
    public class DocketController : ControllerBase
    {
        private readonly CaseRepository _repository;

        public DocketController(CaseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{docketNumber}")]
        public ActionResult<IList<Case>> Get(string docketNumber)
        {
            return (List<Case>) _repository.FindByDocketNumber(docketNumber);
        }

        [HttpDelete("{docketNumber}")]
        public void Delete(string docketNumber)
        {
            _repository.Delete(docketNumber);
        }
        
        [HttpGet]
        public ActionResult<IList<Case>> GetAll()
        {
            return (List<Case>) _repository.FindAll();
        }
        
        [HttpPost]
        public void Post(Case @case)
        {
            _repository.Save(@case);
        }

    }
}