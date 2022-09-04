using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
   [Authorize]
   public class LeaveRequestsController : Controller
   {
      private readonly ApplicationDbContext context;
      private readonly ILeaveRequestRepository requestRepository;

      public LeaveRequestsController(ApplicationDbContext context, ILeaveRequestRepository requestRepository)
      {
         this.context = context;
         this.requestRepository = requestRepository;
      }

      // GET: LeaveRequests
      public async Task<IActionResult> Index()
      {
         var applicationDbContext = context.LeaveRequests.Include(l => l.LeaveType);
         return View(await applicationDbContext.ToListAsync());
      }

      // GET: LeaveRequests/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || context.LeaveRequests == null)
         {
            return NotFound();
         }

         var leaveRequest = await context.LeaveRequests
             .Include(l => l.LeaveType)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (leaveRequest == null)
         {
            return NotFound();
         }

         return View(leaveRequest);
      }

      // GET: LeaveRequests/Create
      public IActionResult Create()
      {
         var model = new LeaveRequestCreateVm
         {
            LeaveTypes = new SelectList(context.LeaveTypes, "Id", "Name")
         };
         return View(model);
      }

      // POST: LeaveRequests/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(LeaveRequestCreateVm model)
      {
         try
         {
            if (ModelState.IsValid)
            {
               await requestRepository.CreateLeaveRequest(model);
               return RedirectToAction(nameof(Index));
            }
         }
         catch (Exception)
         {
            ModelState.AddModelError(string.Empty, "An error has ocurred.");
            return View();
         }
         model.LeaveTypes = new SelectList(context.LeaveTypes, "Id", "Name", model.LeaveTypeId);
         return View(model);
      }

      // GET: LeaveRequests/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || context.LeaveRequests == null)
         {
            return NotFound();
         }

         var leaveRequest = await context.LeaveRequests.FindAsync(id);
         if (leaveRequest == null)
         {
            return NotFound();
         }
         ViewData["LeaveTypeId"] = new SelectList(context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
         return View(leaveRequest);
      }

      // POST: LeaveRequests/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,LeaveTypeId,DateRequested,RequestComments,Approved,Cancelled,RequestingEmployeeId,Id,DateCreated,DateModified")] LeaveRequest leaveRequest)
      {
         if (id != leaveRequest.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               context.Update(leaveRequest);
               await context.SaveChangesAsync();
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
         ViewData["LeaveTypeId"] = new SelectList(context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
         return View(leaveRequest);
      }

      // GET: LeaveRequests/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || context.LeaveRequests == null)
         {
            return NotFound();
         }

         var leaveRequest = await context.LeaveRequests
             .Include(l => l.LeaveType)
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
         if (context.LeaveRequests == null)
         {
            return Problem("Entity set 'ApplicationDbContext.LeaveRequests'  is null.");
         }
         var leaveRequest = await context.LeaveRequests.FindAsync(id);
         if (leaveRequest != null)
         {
            context.LeaveRequests.Remove(leaveRequest);
         }

         await context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool LeaveRequestExists(int id)
      {
         return context.LeaveRequests.Any(e => e.Id == id);
      }
   }
}
