using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Data.Models;
using LeaveManagement.Web.Data.ViewModels;

namespace LeaveManagement.Web.Configurations
{
   public class MappingProfiles : Profile
   {
      public MappingProfiles()
      {
         CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();
         CreateMap<Employee, EmployeeListVM>().ReverseMap();
         CreateMap<Employee, EmployeeAllocationVm>().ReverseMap();
         CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
         CreateMap<LeaveAllocation, LeaveAllocationEditVm>().ReverseMap();
         CreateMap<LeaveRequest, LeaveRequestCreateVm>().ReverseMap();
      }
   }
}