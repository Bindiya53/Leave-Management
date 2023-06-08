using Leave_Management.Web.Constants;
using Leave_Management.Web.Contracts;
using Leave_Management.Web.Data;
using Leave_Management.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Leave_Management.Leave_Management.Leave_Management.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly IdentityDataContext _context;
        private readonly ILeaveRequestRepository _leaveRequestRepo;

        public LeaveRequestsController(IdentityDataContext context, ILeaveRequestRepository leaveRequestRepo)
        {
            _context = context;
            _leaveRequestRepo = leaveRequestRepo;
        }


        [Authorize(Roles = Roles.Administrator )]
        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var model =await  _leaveRequestRepo.GetAdminLeaveRequestList();
            return View(model);
        }

        public async Task<ActionResult> MyLeave()
        {
            var model =await  _leaveRequestRepo.GetMyLeaveDetails();
            return View(model);
        }


        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model  = await _leaveRequestRepo.GetLeaveRequestAsync(id);
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            var model = new LeaveRequestCreateVM
            {
               LeaveType =  new SelectList(_context.LeaveTypes,"Id", "Name")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
               await  _leaveRequestRepo.ChangeApprovalStatus(id, approved);
            }
            catch(Exception ex)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
               await  _leaveRequestRepo.CancelLeaveRequest(id);
            }
            catch(Exception ex)
            {
                throw;
            }
            return RedirectToAction(nameof(MyLeave));
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isValidRequest = await _leaveRequestRepo.CreateLeaveRequest(model);
                    if(isValidRequest)
                    {
                        return RedirectToAction(nameof(MyLeave));
                    }
                    ModelState.AddModelError(string.Empty, "You've exceeded allocation with this request.");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error has occurred, please try later");
            }
            model.LeaveType = new SelectList(_context.LeaveTypes,"Id", "Name", model.LeaveTypeId);
            return View(model);
        }

        // GET: LeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,LeaveTypeId,DateRequested,RequestComments,Approved,Cancelled,RequestingEmployeeId")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.Id))
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
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LeaveRequest'  is null.");
            }
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
          return (_context.LeaveRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
