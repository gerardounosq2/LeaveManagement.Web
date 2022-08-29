using AutoMapper;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using LeaveManagement.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
   public class LeaveTypesController : Controller
   {
      private readonly ILeaveTypeRepository repository;
      private readonly IMapper mapper;

      public LeaveTypesController(ILeaveTypeRepository repository, IMapper mapper)
      {
         this.repository = repository;
         this.mapper = mapper;
      }

      // GET: LeaveTypes
      public async Task<IActionResult> Index() => View(mapper.Map<IEnumerable<LeaveTypeViewModel>>(await repository.GetAllAsync()));

      // GET: LeaveTypes/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         var leaveType = await repository.GetByIdAsync(id);

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
            await repository.AddAsync(leaveType);
            return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeViewModel);
      }

      // GET: LeaveTypes/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         var leaveType = await repository.GetByIdAsync(id);
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
               await repository.UpdateAsync(leaveType);
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!await repository.Exists(leaveTypeVm.Id))
                  return NotFound();
               else
                  throw;
            }
            return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeVm);
      }

      // POST: LeaveTypes/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         await repository.DeleteAsync(id);
         return RedirectToAction(nameof(Index));
      }
   }
}