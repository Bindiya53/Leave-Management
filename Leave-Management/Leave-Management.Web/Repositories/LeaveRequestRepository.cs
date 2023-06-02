using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;
using Leave_Management.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Leave_Management.Web.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly IdentityDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public LeaveRequestRepository(IdentityDataContext dbContext,
                                      IMapper mapper,
                                      IHttpContextAccessor httpContextAccessor,
                                      UserManager<IdentityUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            
            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;
            await AddAsync(leaveRequest);
        }
    }
}