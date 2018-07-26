using System.Collections.Generic;
using System.Linq;

namespace CourtApi.com.pillartechnology.court
{

    public class CaseRepository : CasePersistable
    {
        private readonly CaseContext _context;

        public CaseRepository(CaseContext context)
        {
            _context = context;
        }

        public IList<Case> FindAll()
        {
            return _context.Cases.ToList();
        }

        public IList<Case> FindByDocketNumber(string docketNumber)
        {
            return _context.Cases
                .Where(c => c.DocketNumber == docketNumber)
                .ToList();
        }

        public void Save(Case @case)
        {
            _context.Cases.Add(@case);
            _context.SaveChanges();
        }

        public void Delete(string docketNumber)
        {
            foreach (var @case in FindByDocketNumber(docketNumber))
            {
                _context.Cases.Remove(@case);
            }
            
            _context.SaveChanges();
        }
    }
}