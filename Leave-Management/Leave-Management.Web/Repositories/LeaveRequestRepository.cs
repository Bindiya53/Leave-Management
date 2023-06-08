using AutoMapper;
using AutoMapper.QueryableExtensions;
using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;
using Leave_Management.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;
        internal DbSet<LeaveRequest> _dbSet;
        private readonly AutoMapper.IConfigurationProvider _configuration;
        public LeaveRequestRepository(IdentityDataContext dbContext,
                                      IMapper mapper,
                                      IHttpContextAccessor httpContextAccessor,
                                      UserManager<IdentityUser> userManager,
                                      ILeaveAllocationRepository leaveAllocationRepo,
                                      AutoMapper.IConfigurationProvider _configuration,
                                      IEmailSender emailSender) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _leaveAllocationRepo = leaveAllocationRepo;
            this._configuration = _configuration;
            _emailSender = emailSender;
            this._dbSet = _dbContext.Set<LeaveRequest>();
        }

        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Cancelled = true;
            await UpdateAsync(leaveRequest);

            var user = await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);

            await _emailSender.SendEmailAsync(user.Email,$"Leave Request Cancelled", $"Your Leave Request from" +
             $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been Cancelled Successfully");
        }

        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;
            if(approved)
            {
                var allocation = await _leaveAllocationRepo.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;
                await _leaveAllocationRepo.UpdateAsync(allocation);
            }
            await UpdateAsync(leaveRequest);

            var user = await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);
            var approvalStatus = approved ? "Approved" : "Declined";

            await _emailSender.SendEmailAsync(user.Email,$"Leave Request {approvalStatus}", $"Your Leave Request from" +
             $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}");
        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
           // var leaveAllocation = await _leaveAllocationRepo.GetEmployeeAllocation(user.Id, model.LeaveTypeId);

            // if(leaveAllocation == null)
            // {
            //     return false;
            // }

            int daysRequested = (int)(model.EndDate.Value - model.StartDate.Value).TotalDays;

            // if(daysRequested > leaveAllocation.NumberOfDays)
            // {
            //     return false;
            // }
            
            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;
            await AddAsync(leaveRequest);

            await _emailSender.SendEmailAsync(user.Email,$"Leave Request Submitted", $"Your Leave Request from" +
             $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been submitted for approval");

            return true;
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

        public async Task<List<LeaveRequestVM>> GetAllAsync(string employeeId)
        {
            return await _dbContext.LeaveRequests
            .Where(x => x.RequestingEmployeeId == employeeId)
            .ProjectTo<LeaveRequestVM>(_configuration)
            .ToListAsync();
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
            var requests = await GetAllAsync(user.Id);
            var model = new EmployeeLeaveRequestVM(allocation, requests);

            return model;
        }
    }
}