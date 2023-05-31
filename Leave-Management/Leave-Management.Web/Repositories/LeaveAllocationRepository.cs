using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;
using Microsoft.AspNetCore.Identity;
using Leave_Management.Web.Constants;
using Leave_Management.Web.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Leave_Management.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly IdentityDataContext _dbContext;
        private readonly IMapper _mapper;
        internal DbSet<LeaveType> _dbSet;
        public LeaveAllocationRepository(IdentityDataContext dbContext,
                                         UserManager<IdentityUser> userManager,
                                         ILeaveTypeRepository leaveTypeRepo,
                                         IMapper mapper) : base(dbContext)
        {
            _userManager = userManager;
            _leaveTypeRepo = leaveTypeRepo;
            _dbContext = dbContext;
            _mapper = mapper;
            this._dbSet = _dbContext.Set<LeaveType>();
            
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return _dbContext.LeaveAllocations.Any( x => x.EmployeeId == employeeId
                                                     && x.LeaveTypeId == leaveTypeId
                                                     && x.Period == period );
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await _dbContext.LeaveAllocations
            .Include(x =>x.LeaveType)
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();

            var employee = await _userManager.FindByIdAsync(employeeId);
            var employeeAllocationModel = _mapper.Map<EmployeeAllocationVM>(employee);
            employeeAllocationModel.LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations);

            return employeeAllocationModel;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id)
        {
            var allocation = await _dbContext.LeaveAllocations
            .Include(x =>x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);

            if(allocation == null)
            {
                return null;
            }

            var employee = await _userManager.FindByIdAsync(allocation.EmployeeId);
            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);
            model.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(allocation.EmployeeId));

            return model;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepo.GetAsync(leaveTypeId);
            var allocation = new List<LeaveAllocation>();

            foreach(var employee in employees)
            {
                if(await AllocationExists(employee.Id,leaveTypeId, period))
                continue;
                allocation.Add( new LeaveAllocation()
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays
                });
            }
            await AddRangeAsync(allocation);
        }

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM leaveAllocationEditVM)
        {
            var leaveAllocation = await GetAsync(leaveAllocationEditVM.Id);
            if(leaveAllocation ==  null)
            {
                return false;
            }
            leaveAllocation.Period = leaveAllocationEditVM.Period;
            leaveAllocation.NumberOfDays = leaveAllocationEditVM.NumberOfDays;
            await UpdateAsync(leaveAllocation);
            return true;
        }
    }
}