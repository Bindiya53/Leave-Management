using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Leave_Management.Web.Data;
using AutoMapper;
using Leave_Management.Web.Models;
using Leave_Management.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using Leave_Management.Web.Constants;

namespace Leave_Management.Web.Controllers_
{
    [Authorize(Roles = Roles.Administrator )]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly IMapper _mapper;

        public LeaveTypeController(ILeaveTypeRepository leaveTypeRepo,
                                     IMapper mapper,
                                     ILeaveAllocationRepository leaveAllocationRepo)
        {
            _leaveTypeRepo = leaveTypeRepo;
            _mapper = mapper;
            _leaveAllocationRepo = leaveAllocationRepo;
        }

        // GET: LeaveType
        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveTypeRepo.GetAllAsync();
            return View(leaveTypes);
        }

        // GET: LeaveType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var leaveType = await _leaveTypeRepo.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var leaveTypeVM = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(leaveTypeVM);
        }

        // GET: LeaveType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeVM leaveTypeVM)
        {
            if (ModelState.IsValid)
            {
                var leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
                await _leaveTypeRepo.AddAsync(leaveType);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        // GET: LeaveType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var leaveType = await _leaveTypeRepo.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var leaveTypeVM = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(leaveTypeVM);
        }

        // POST: LeaveType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeVM leaveTypeVM)
        {
            if (id != leaveTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
                    await _leaveTypeRepo.UpdateAsync(leaveType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _leaveTypeRepo.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        // POST: LeaveType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypeRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(int id)
        {
            await _leaveAllocationRepo.LeaveAllocation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
