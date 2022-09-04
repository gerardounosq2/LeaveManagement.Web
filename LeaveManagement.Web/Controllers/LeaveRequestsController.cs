using LeaveManagement.Web.Constants;
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

      [Authorize(Roles = Roles.Administrator)]
      public async Task<IActionResult> Index() => View(await requestRepository.GetAdminLeaveRequestList());

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

      public IActionResult Create()
      {
         var model = new LeaveRequestCreateVm
         {
            LeaveTypes = new SelectList(context.LeaveTypes, "Id", "Name")
         };
         return View(model);
      }

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

      public async Task<ActionResult> MyLeave() => View(await requestRepository.GetMyLeaveDetails());
   }
}
