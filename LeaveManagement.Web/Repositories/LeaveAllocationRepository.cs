using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
   public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
   {
      private readonly ApplicationDbContext context;
      private readonly UserManager<Employee> userManager;
      private readonly ILeaveTypeRepository leaveTypeRepository;

      public LeaveAllocationRepository(ApplicationDbContext context, UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository)
         : base(context)
      {
         this.context = context;
         this.userManager = userManager;
         this.leaveTypeRepository = leaveTypeRepository;
      }

      public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
      {
         return await context
            .LeaveAllocations
            .AnyAsync(r => r.EmployeeId == employeeId
               && r.LeaveTypeId == leaveTypeId
               && r.Period == period);
      }

      public async Task LeaveAllocation(int leaveTypeId)
      {
         var employees = await userManager.GetUsersInRoleAsync(Roles.User);
         var period = DateTime.Now.Year;
         var leaveType = await leaveTypeRepository.GetByIdAsync(leaveTypeId);
         var leaveAllocations = new List<LeaveAllocation>();

         foreach (var employee in employees)
         {
            if (await AllocationExists(employee.Id, leaveTypeId, period))
               continue;

            var allocation = new LeaveAllocation
            {
               LeaveTypeId = leaveTypeId,
               Period = period,
               EmployeeId = employee.Id,
               NumberOfDays = leaveType.DefaultDays,
               DateCreated = DateTime.Today,
               DateModified = DateTime.Now
            };
            leaveAllocations.Add(allocation);
         }
         await AddRangeAsync(leaveAllocations);
      }
   }
}