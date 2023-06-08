namespace Leave_Management.Web.Models
{
    public class LeaveAllocationEditVM : LeaveAllocationVM
    {
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public EmployeeListVM? Employee { get; set; }
    }
}