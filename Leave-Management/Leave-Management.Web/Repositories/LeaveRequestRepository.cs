using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;

namespace Leave_Management.Web.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly IdentityDataContext _dbContext;
        public LeaveRequestRepository(IdentityDataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<LeaveRequest> AddAsync(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<LeaveRequest> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        Task<List<LeaveRequest>> IGenericRepository<LeaveRequest>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<LeaveRequest> IGenericRepository<LeaveRequest>.GetAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}