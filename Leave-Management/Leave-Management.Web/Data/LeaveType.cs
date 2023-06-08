using System.ComponentModel.DataAnnotations;

namespace Leave_Management.Web.Data
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; }

        [Display(Name = "Default Number Of Days")]
        public int DefaultDays { get; set; }
    }
}