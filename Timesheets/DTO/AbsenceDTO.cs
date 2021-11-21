using System;

namespace Timesheets.DTO
{
    public class AbsenceDTO
    {
        public int Id { get; set; }
        public int Reason { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int Duration { get; set; }
        public bool Discounted { get; set; }
        public string Description { get; set; }
    }
}
