using Kanakku.Shared;

namespace Kanakku.Application.Models.DailyOperation
{
    public class DailyOperationFilterDto
    {
        DateTime? workedFrom;
        public DateTime? WorkedFrom
        {
            get => workedFrom;
            set
            {
                if (value != workedFrom)
                {
                    QuickFilter = null;
                }

                workedFrom = value;
            }
        }

        DateTime? workedTo;
        public DateTime? WorkedTo
        {
            get => workedTo;
            set
            {
                if (value != workedTo)
                {
                    QuickFilter = null;
                }

                workedTo = value;
            }
        }

        DateFilter? quickFilter = DateFilter.ThisMonth;
        public DateFilter? QuickFilter
        {
            get => quickFilter;
            set
            {
                if (value != quickFilter)
                {
                    workedTo = null;
                    workedFrom = null;
                }

                quickFilter = value;
            }
        }
        public IEnumerable<Guid> WorkedBy { get; set; }
        public IEnumerable<int> Products { get; set; }
        public IEnumerable<int> Operations { get; set; }
        public int[] Sizes { get; set; }
    }
}
