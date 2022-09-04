using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
   public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
   {
      private readonly ApplicationDbContext context;
      private readonly UserManager<Employee> userManager;
      private readonly ILeaveTypeRepository leaveTypeRepository;
      private readonly IMapper mapper;

      public LeaveAllocationRepository(ApplicationDbContext context, UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
         : base(context)
      {
         this.context = context;
         this.userManager = userManager;
         this.leaveTypeRepository = leaveTypeRepository;
         this.mapper = mapper;
      }

      public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
      {
         return await context
            .LeaveAllocations
            .AnyAsync(r => r.EmployeeId == employeeId
               && r.LeaveTypeId == leaveTypeId
               && r.Period == period);
      }

      public async Task<LeaveAllocationEditVm> GetEmployeeAllocation(int leaveAllocationId)
      {
         var allocation = await context.LeaveAllocations
            .Include(r => r.LeaveType)
            .FirstOrDefaultAsync(r => r.Id == leaveAllocationId);
         
         if (allocation == null)
            return null;

         var employee = await userManager.FindByIdAsync(allocation.EmployeeId);

         var model = mapper.Map<LeaveAllocationEditVm>(allocation);

         model.Employee = mapper.Map<EmployeeListVM>(employee);

         return model;
      }

      public async Task<EmployeeAllocationVm> GetEmployeeAllocations(string employeeId)
      {
         var allocations = await context
            .LeaveAllocations
            .Include(r => r.LeaveType)
            .Where(r => r.EmployeeId == employeeId)
            .ToListAsync();
         var employee = await userManager.FindByIdAsync(employeeId);

         var employeeAllocation = mapper.Map<EmployeeAllocationVm>(employee);

         employeeAllocation.LeaveAllocations = mapper.Map<IEnumerable<LeaveAllocationVM>>(allocations);

         return employeeAllocation;
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

      public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVm model)
      {
         var leaveAllocation = await GetByIdAsync(model.Id);
         if (leaveAllocation == null)
            return false;

         leaveAllocation.Period = model.Period;
         leaveAllocation.NumberOfDays = model.NumberOfDays;
         await UpdateAsync(leaveAllocation);
         return true;
      }

      public async Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId)
      {
         return await context
            .LeaveAllocations
            .FirstOrDefaultAsync(r => r.EmployeeId == employeeId 
               && r.LeaveTypeId == leaveTypeId);         
      }
   }
}