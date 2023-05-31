using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Web.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Leave Type Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Default Number Of Days")]
        [Range (1,25, ErrorMessage ="Please Enter A Valid Number")]
        [Required]
        public int DefaultDays { get; set; }
    }
}
