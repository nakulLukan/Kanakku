using Kanakku.Domain.User;

namespace Kanakku.Domain.Inventory;

public class WorkHistory : DomainBase
{
    public int Id { get; set; }
    public int WorkId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime WorkedOn { get; set; }

    /// <summary>
    /// Total minutes worked on <see cref="WorkedOn"/>
    /// </summary>
    public int WorkDuration { get; set; }

    public Work Work { get; set; }
    public Employee Employee { get; set; }
}
