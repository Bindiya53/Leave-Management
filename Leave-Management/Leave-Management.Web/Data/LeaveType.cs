using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Web.Data
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; }

        [Display(Name = "Default Number Of Days")]
        public int DefaultDays { get; set; }
    }
}