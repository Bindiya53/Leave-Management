using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Leave_Management.Web.Constants;
using AutoMapper;
using Leave_Management.Web.Models;
using Leave_Management.Web.Contracts;

namespace Leave_Management.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly IMapper _mapper;
        
        public EmployeesController(UserManager<IdentityUser> userManager,
                                    IMapper mapper,
                                    ILeaveAllocationRepository leaveAllocationRepo,
                                    ILeaveTypeRepository leaveTypeRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _leaveAllocationRepo = leaveAllocationRepo;
            _leaveTypeRepo = leaveTypeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var employees =await _userManager.GetUsersInRoleAsync(Roles.User);
            var model = _mapper.Map<List<EmployeeListVM>>(employees);
            return View(model);
        }

        public async Task<IActionResult> ViewAllocations(string id)
        {
            var model = await _leaveAllocationRepo.GetEmployeeAllocations(id);
            return View(model);
        }

        public async Task<IActionResult> EditAllocation(int id)
        {
            var model = await _leaveAllocationRepo.GetEmployeeAllocation(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(int id,LeaveAllocationEditVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(await _leaveAllocationRepo.UpdateEmployeeAllocation(model))
                    {
                        return RedirectToAction(nameof(ViewAllocations), new {id = model.EmployeeId});
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error has occured, please try later");
            }
            model.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(model.EmployeeId));
            model.LeaveType = _mapper.Map<LeaveTypeVM>(await _leaveTypeRepo.GetAsync(model.LeaveTypeId));
            return View(model);
        }


    }
}