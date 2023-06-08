using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;

namespace Leave_Management.Web.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(IdentityDataContext dbContext) : base(dbContext)
        {
            
        }
    }
}