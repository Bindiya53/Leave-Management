using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Web.Models
{
    public class EmployeeLeaveRequestVM
    {
        public List<LeaveAllocationVM> LeaveAllocations{get; set;}
        public List<LeaveRequestVM> LeaveRequests{get; set;}
        public EmployeeLeaveRequestVM(List<LeaveAllocationVM> leaveAllocations, List<LeaveRequestVM> leaveRequests)
        {
            LeaveAllocations = leaveAllocations;
            LeaveRequests = leaveRequests;
        }
    }
}