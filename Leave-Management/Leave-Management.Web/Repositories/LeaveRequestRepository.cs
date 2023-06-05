using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;
using Leave_Management.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Leave_Management.Web.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly IdentityDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        internal DbSet<LeaveRequest> _dbSet;
        public LeaveRequestRepository(IdentityDataContext dbContext,
                                      IMapper mapper,
                                      IHttpContextAccessor httpContextAccessor,
                                      UserManager<IdentityUser> userManager,
                                      ILeaveAllocationRepository leaveAllocationRepo) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _leaveAllocationRepo = leaveAllocationRepo;
            this._dbSet = _dbContext.Set<LeaveRequest>();
        }

        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;
            if(approved)
            {
                var allocation = await _leaveAllocationRepo.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequestId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;
                await _leaveAllocationRepo.UpdateAsync(allocation);
            }
            await UpdateAsync(leaveRequest);
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            
            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;
            await AddAsync(leaveRequest);
        }

        public async Task<AdminLeaveRequestVM> GetAdminLeaveRequestList()
        {
            var leaveRequests = await _dbContext.LeaveRequests.Include( x => x.LeaveType).ToListAsync();
            var model = new AdminLeaveRequestVM
            {
                TotalRequest = leaveRequests.Count,
                ApprovedRequest = leaveRequests.Count(x => x.Approved == true),
                RejectedRequest = leaveRequests.Count(x => x.Approved ==  false),
                PendingRequest =  leaveRequests.Count(x => x.Approved == null),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };

            foreach(var leaveRequest in model.LeaveRequests)
            {
                leaveRequest.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
            }

            return model;
        }

        public async Task<List<LeaveRequest>> GetAllAsync(string employeeId)
        {
            return _dbContext.LeaveRequests.Where(x => x.RequestingEmployeeId == employeeId).ToList();
        }

        public async Task<LeaveRequestVM?> GetLeaveRequestAsync(int? id)
        {
            var leaveRequests = await _dbContext.LeaveRequests.Include( x => x.LeaveType).FirstOrDefaultAsync(x => x.Id == id);

            if(leaveRequests == null)
            {
                return null;
            }
            var model =  _mapper.Map<LeaveRequestVM>(leaveRequests);
            model.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(leaveRequests?.RequestingEmployeeId));
            return model;
        }

        public async Task<EmployeeLeaveRequestVM> GetMyLeaveDetails()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var allocation = (await _leaveAllocationRepo.GetEmployeeAllocations(user.Id)).LeaveAllocations;
            var requests = _mapper.Map<List<LeaveRequestVM>>(await GetAllAsync(user.Id));
            var model = new EmployeeLeaveRequestVM(allocation, requests);

            return model;
        }
    }
}