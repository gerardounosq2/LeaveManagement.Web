using AutoMapper;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.Web.Repositories
{
   public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
   {
      private readonly IMapper mapper;
      private readonly IHttpContextAccessor contextAccessor;
      private readonly UserManager<Employee> userManager;

      public LeaveRequestRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<Employee> userManager)
         : base(context)
      {
         this.mapper = mapper;
         this.contextAccessor = contextAccessor;
         this.userManager = userManager;
      }

      public async Task CreateLeaveRequest(LeaveRequestCreateVm model)
      {
         var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);

         var leaveRequest = mapper.Map<LeaveRequest>(model);

         leaveRequest.DateRequested = DateTime.Now;
         leaveRequest.RequestingEmployeeId = user.Id;

         await AddAsync(leaveRequest);
      }
   }
}