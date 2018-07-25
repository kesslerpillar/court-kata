using System.Collections.Generic;
using System.Linq;

namespace CourtApi.com.pillartechnology.court
{

    public class CaseRepository : CasePersistable
    {
        private readonly IDictionary<string, Case> _repository;

        public CaseRepository()
        {
            _repository = new Dictionary<string, Case>();
        }

        public IList<Case> FindAll()
        {
            return _repository.Select(keyValuePair => keyValuePair.Value).ToList();
        }

        public IList<Case> FindByDocketNumber(string docketNumber)
        {
            var cases = new List<Case>();
            
            if (_repository.ContainsKey(docketNumber))
            {
                cases.Add(_repository[docketNumber]);
            }
            return cases;
        }

        public void Save(Case @case)
        {
            _repository[@case.DocketNumber] = @case;
        }

        public void Delete(string docketNumber)
        {
            if (_repository.ContainsKey(docketNumber))
            {
                _repository.Remove(docketNumber);
            }
        }
    }
}