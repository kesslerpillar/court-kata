using System.Collections.Generic;

namespace CourtApi.com.pillartechnology.court
{
    public interface CasePersistable
    {
        IList<Case> FindAll();
        IList<Case> FindByDocketNumber(string docketNumber);
        void Save(Case @case);
        void Delete(string docketNumber);
    }
}