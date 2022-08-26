using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
   public class LeaveTypesController : Controller
   {
      private readonly ApplicationDbContext context;
      private readonly IMapper mapper;

      public LeaveTypesController(ApplicationDbContext context, IMapper mapper)
      {
         this.context = context;
         this.mapper = mapper;
      }

      // GET: LeaveTypes
      public async Task<IActionResult> Index() => View(mapper.Map<IEnumerable<LeaveTypeViewModel>>(await context.LeaveTypes.ToListAsync()));

      // GET: LeaveTypes/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || context.LeaveTypes == null)
            return NotFound();

         var leaveType = await context.LeaveTypes.FindAsync(id);
         if (leaveType == null)
            return NotFound();

         var leaveTypeVm = mapper.Map<LeaveTypeViewModel>(leaveType);
         return View(leaveTypeVm);
      }

      // GET: LeaveTypes/Create
      public IActionResult Create() => View();

      // POST: LeaveTypes/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(LeaveTypeViewModel leaveTypeViewModel)
      {
         if (ModelState.IsValid)
         {
            var leaveType = mapper.Map<LeaveType>(leaveTypeViewModel);
            context.LeaveTypes.Add(leaveType);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeViewModel);
      }

      // GET: LeaveTypes/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || context.LeaveTypes == null)
            return NotFound();

         var leaveType = await context.LeaveTypes.FindAsync(id);
         if (leaveType == null)
            return NotFound();

         var leaveTypeVm = mapper.Map<LeaveTypeViewModel>(leaveType);
         return View(leaveTypeVm);
      }

      // POST: LeaveTypes/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, LeaveTypeViewModel leaveTypeVm)
      {
         if (id != leaveTypeVm.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               var leaveType = mapper.Map<LeaveType>(leaveTypeVm);
               context.LeaveTypes.Update(leaveType);
               await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!context.LeaveTypes.Any(e => e.Id == leaveTypeVm.Id))
                  return NotFound();
               else
                  throw;
            }
            return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeVm);
      }

      // GET: LeaveTypes/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || context.LeaveTypes == null)
         {
            return NotFound();
         }

         var leaveType = await context.LeaveTypes
             .FirstOrDefaultAsync(m => m.Id == id);
         if (leaveType == null)
         {
            return NotFound();
         }

         return View(leaveType);
      }

      // POST: LeaveTypes/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         if (context.LeaveTypes == null)
         {
            return Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");
         }
         var leaveType = await context.LeaveTypes.FindAsync(id);
         if (leaveType != null)
         {
            context.LeaveTypes.Remove(leaveType);
         }

         await context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }
   }
}