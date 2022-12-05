using Kanakku.Shared;

namespace Kanakku.Application.Models.User
{
    public class EmployeeRegistryFilterDto
    {
        public IEnumerable<Guid> Employees { get; set; }

        private EmployeeSalaryDateFilter? dateFilter;
        public EmployeeSalaryDateFilter? DateFilter
        {
            get => dateFilter;
            set
            {
                if (value != dateFilter)
                {
                    From = null;
                    To = null;
                }

                dateFilter = value;
            }
        }

        private DateTime? from;
        public DateTime? From
        {
            get => from;
            set
            {
                if (value != from)
                {
                    DateFilter = null;
                }

                from = value;
            }
        }

        private DateTime? to;
        public DateTime? To
        {
            get => to;
            set
            {
                if (value != to)
                {
                    DateFilter = null;
                }

                to = value;
            }
        }

        public IEnumerable<int> Designations { get; set; }
        public bool ShowBonus { get; set; }
    }
}
