using System.ComponentModel.DataAnnotations;

namespace Leave_Management.Web.Models
{
    public class AdminLeaveRequestVM
    {
        [Display(Name = "Total Number Of Requests")]
        public int TotalRequest { get; set; }

        [Display(Name = "Pending Requests")]
        public int PendingRequest { get; set; }

        [Display(Name = "Rejected Requests")]
        public int RejectedRequest { get; set; }

        [Display(Name = "Approved Requests")]
        public int ApprovedRequest { get; set; }
        public List<LeaveRequestVM> LeaveRequests { get; set; }
    }
}