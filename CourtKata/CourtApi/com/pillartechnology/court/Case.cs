using System;

namespace CourtApi.com.pillartechnology.court
{
    public class Case
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocketNumber { get; set; }
        public DateTime OpenDate { get; set; }
    }
}