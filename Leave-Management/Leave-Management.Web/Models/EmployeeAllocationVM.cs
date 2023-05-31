using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Web.Models
{
    public class EmployeeAllocationVM : EmployeeListVM
    {
        public List<LeaveAllocationVM> LeaveAllocations {get;set;}
    }
}