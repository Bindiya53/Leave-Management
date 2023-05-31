using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Web.Models
{
    public class LeaveAllocationVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name ="Number Of Days")]
        [Required]
        [Range(1, 50, ErrorMessage="Invalid number entered")]
        public int NumberOfDays { get; set; }

        [Display(Name ="Allocation Period")]
        [Required]
        public int Period { get; set; }
        public LeaveTypeVM? LeaveType { get; set; }
    }
}