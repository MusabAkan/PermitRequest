using PermitRequest.Application.Concrete;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Interfaces;

namespace PermitRequest.Application.Features.Factories
{
    public class LeaveRequestFactory
    {
        public static LeaveRequest Create(AdUser user, DateTime startDate, DateTime endDate, LeaveType leaveType, string reason, Guid? managerId, Guid? managerOfManagerId)
        {

            if (startDate.Date >= endDate.Date)
                throw new ExceptionMessage("Başlangıç tarih bitiş tarihden büyük yada eşit olmamalıdır..");

            if (reason.Length > 150)
                throw new ExceptionMessage("Sebep açıklamsında karakter sayısı 150'den fazla olmamalıdır");

            static IWorkflowFactory CreateWorkflowFactory(UserType userType, LeaveType leaveType)
            {
                switch (userType)
                {
                    case UserType.WhiteCollarEmployee:
                        return new WhiteCollarEmployeeWorkflowFactory();
                    case UserType.BlueCollarEmployee:
                        if (leaveType == LeaveType.AnnualLeave)
                            return new BlueCollarEmployeeAnnualLeaveWorkflowFactory();
                        else if (leaveType == LeaveType.ExcusedAbsence)
                            return new BlueCollarEmployeeExcusedAbsenceWorkflowFactory();
                        else
                            throw new ArgumentOutOfRangeException();
                    case UserType.Manager:
                        return new ManagerWorkflowFactory();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            var result = CreateWorkflowFactory(user.UserType, leaveType).CreateWorkflow();

            LeaveRequest entity = new()
            {
                
                Reason = reason,
                WorkflowStatus = result.Item1,
                CreatedBy = user,
                LeaveType = leaveType,
            };

            switch (result.Item2)
            {
                case "ManagerOfManagerCase":
                    entity.AssignedUserId = managerOfManagerId;
                    break;
                case "ManagerCase":
                    entity.AssignedUserId = managerId;
                    break;
                case "EmployeeManagerCase":
                    entity.AssignedUserId = user.Id;
                    break;
                default:
                    entity.AssignedUserId = null;
                    break;
            }

           

            return entity;

        }
    }
}
