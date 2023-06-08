namespace Leave_Management.Web.Models
{
    public class EmployeeAllocationVM : EmployeeListVM
    {
        public List<LeaveAllocationVM> LeaveAllocations {get;set;}
    }
}