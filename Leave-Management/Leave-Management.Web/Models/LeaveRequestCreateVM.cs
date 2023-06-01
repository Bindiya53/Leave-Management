using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Leave_Management.Web.Models
{
    public class LeaveRequestCreateVM
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public SelectList LeaveType{get; set;}
        public string RequestComments { get; set; }
    }
}