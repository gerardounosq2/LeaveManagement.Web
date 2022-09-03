using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Web.Controllers
{
   public class EmployeesController : Controller
   {
      private readonly UserManager<Employee> userManager;

      private readonly IMapper mapper;
      private readonly ILeaveAllocationRepository allocationRepository;
      private readonly ILeaveTypeRepository leaveTypeRepository;

      public EmployeesController(UserManager<Employee> userManager, IMapper mapper, ILeaveAllocationRepository allocationRepository, ILeaveTypeRepository leaveTypeRepository)
      {
         this.userManager = userManager;
         this.mapper = mapper;
         this.allocationRepository = allocationRepository;
         this.leaveTypeRepository = leaveTypeRepository;
      }

      public async Task<IActionResult> Index()
      {
         var employees = await userManager.GetUsersInRoleAsync(Roles.User);
         return View(mapper.Map<List<EmployeeListVM>>(employees));
      }

      public async Task<IActionResult> ViewAllocations(string id)
      {
         var allocations = await allocationRepository.GetEmployeeAllocations(id);
         return View(allocations);
      }

      public async Task<IActionResult> EditAllocation(int id)
      {
         var leaveAllocation = await allocationRepository.GetEmployeeAllocation(id);
         if (leaveAllocation == null)
            return NotFound();

         return View(leaveAllocation);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> EditAllocation(int id, LeaveAllocationEditVm model)
      {
         try
         {
            if (ModelState.IsValid)
            {
               if (await allocationRepository.UpdateEmployeeAllocation(model))
               {
                  return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId });
               }
            }
         }
         catch (Exception)
         {
            ModelState.AddModelError(string.Empty, "An error has ocurred.");
            return View();
         }
         model.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(model.EmployeeId));
         model.LeaveType = mapper.Map<LeaveTypeViewModel>(await leaveTypeRepository.GetByIdAsync(model.LeaveTypeId));
         return View(model);
      }
   }
}
