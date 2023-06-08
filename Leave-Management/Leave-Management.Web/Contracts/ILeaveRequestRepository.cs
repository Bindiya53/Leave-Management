using Leave_Management.Web.Data;
using Leave_Management.Web.Models;

namespace Leave_Management.Web.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestVM> GetMyLeaveDetails();
        Task<List<LeaveRequestVM>> GetAllAsync(string employeeId);
        Task<LeaveRequestVM?> GetLeaveRequestAsync(int? id);
        Task<AdminLeaveRequestVM> GetAdminLeaveRequestList();
        Task CancelLeaveRequest(int leaveRequestId);
        Task ChangeApprovalStatus(int leaveRequestId, bool approved);
    }
}