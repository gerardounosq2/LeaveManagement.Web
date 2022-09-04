using AutoMapper;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
   public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
   {
      private readonly IMapper mapper;
      private readonly IHttpContextAccessor contextAccessor;
      private readonly UserManager<Employee> userManager;
      private readonly ILeaveAllocationRepository allocationRepository;
      private readonly ApplicationDbContext context;

      public LeaveRequestRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<Employee> userManager, ILeaveAllocationRepository allocationRepository)
         : base(context)
      {
         this.mapper = mapper;
         this.contextAccessor = contextAccessor;
         this.userManager = userManager;
         this.allocationRepository = allocationRepository;
         this.context = context;
      }

      public async Task CreateLeaveRequest(LeaveRequestCreateVm model)
      {
         var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);

         var leaveRequest = mapper.Map<LeaveRequest>(model);

         leaveRequest.DateRequested = DateTime.Now;
         leaveRequest.RequestingEmployeeId = user.Id;

         await AddAsync(leaveRequest);
      }

      public async Task<IEnumerable<LeaveRequest>> GetAllAsync(string employeeId)
      {
         return await context.LeaveRequests.Where(r => r.RequestingEmployeeId == employeeId).ToListAsync();
      }

      public async Task<EmployeeLeaveRequestVm> GetMyLeaveDetails()
      {
         var user = await userManager.GetUserAsync(contextAccessor?.HttpContext?.User);

         var allocations = (await allocationRepository.GetEmployeeAllocations(user.Id)).LeaveAllocations;
         var requests = mapper.Map<IEnumerable<LeaveRequestVm>>(await GetAllAsync(user.Id));
         return new EmployeeLeaveRequestVm(allocations, requests);
      }

      public async Task<AdminViewRequestsVm> GetAdminLeaveRequestList()
      {
         var requests = await context.LeaveRequests.Include(r => r.LeaveType).ToListAsync();
         var model = new AdminViewRequestsVm
         {
            TotalRequests = requests.Count(),
            ApprovedRequests = requests.Count(r => r.Approved == true),
            PendingRequests = requests.Count(r => r.Approved == null),
            RejectedRequests = requests.Count(r => r.Approved == false),
            LeaveRequests = mapper.Map<IEnumerable<LeaveRequestVm>>(requests)
         };
         foreach (var request in model.LeaveRequests)
         {
            request.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(request.RequestingEmployeeId));
         }
         return model;
      }

      public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
      {
         var leaveRequest = await GetByIdAsync(leaveRequestId);
         leaveRequest.Approved = approved;

         if (approved)
         {
            var allocation = await allocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.EndDate).TotalDays;
            allocation.NumberOfDays -= daysRequested;
            await allocationRepository.UpdateAsync(allocation);
         }
         await UpdateAsync(leaveRequest);
      }

      public async Task<LeaveRequestVm> GetLeaveRequestAsync(int? id)
      {
         var leaveRequest = await context.LeaveRequests.Include(r => r.LeaveType).FirstOrDefaultAsync(r => r.Id == id);

         if (leaveRequest == null)
            return null;

         var model = mapper.Map<LeaveRequestVm>(leaveRequest);
         model.Employee=mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(leaveRequest?.RequestingEmployeeId));
         return model;
      }
   }
}