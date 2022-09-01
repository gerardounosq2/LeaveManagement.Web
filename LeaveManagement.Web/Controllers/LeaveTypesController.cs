using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
   [Authorize(Roles = Roles.Administrator)]
   public class LeaveTypesController : Controller
   {
      private readonly ILeaveTypeRepository leaveTypeRepository;
      private readonly IMapper mapper;
      private readonly ILeaveAllocationRepository leaveAllocationRepository;

      public LeaveTypesController(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
      {
         this.leaveTypeRepository = leaveTypeRepository;
         this.mapper = mapper;
         this.leaveAllocationRepository = leaveAllocationRepository;
      }

      public async Task<IActionResult> Index() => View(mapper.Map<IEnumerable<LeaveTypeViewModel>>(await leaveTypeRepository.GetAllAsync()));

      public async Task<IActionResult> Details(int? id)
      {
         var leaveType = await leaveTypeRepository.GetByIdAsync(id);

         if (leaveType == null)
            return NotFound();

         var leaveTypeVm = mapper.Map<LeaveTypeViewModel>(leaveType);
         return View(leaveTypeVm);
      }

      public IActionResult Create() => View();

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(LeaveTypeViewModel leaveTypeViewModel)
      {
         if (ModelState.IsValid)
         {
            var leaveType = mapper.Map<LeaveType>(leaveTypeViewModel);
            await leaveTypeRepository.AddAsync(leaveType);
            return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeViewModel);
      }

      public async Task<IActionResult> Edit(int? id)
      {
         var leaveType = await leaveTypeRepository.GetByIdAsync(id);
         if (leaveType == null)
            return NotFound();

         var leaveTypeVm = mapper.Map<LeaveTypeViewModel>(leaveType);
         return View(leaveTypeVm);
      }

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
               await leaveTypeRepository.UpdateAsync(leaveType);
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!await leaveTypeRepository.Exists(leaveTypeVm.Id))
                  return NotFound();
               else
                  throw;
            }
            return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeVm);
      }

      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         await leaveTypeRepository.DeleteAsync(id);
         return RedirectToAction(nameof(Index));
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> AllocateLeave(int id)
      {
         await leaveAllocationRepository.LeaveAllocation(id);
         return RedirectToAction(nameof(Index));
      }
   }
}