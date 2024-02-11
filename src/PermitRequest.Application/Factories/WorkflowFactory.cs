using Ardalis.Specification;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Exceptions;

namespace PermitRequest.Application.Factories
{
    public class WorkflowFactory
    {
        internal interface IWorkflowFactory
        {
            WorkflowFactoryDto Create();
        }
        private class BlueCollarEmployeeAnnualLeaveWorkflowFactory(IEnumerable<AdUser> Users) : IWorkflowFactory
        {
            WorkflowFactoryDto IWorkflowFactory.Create()
            {
                return new(Workflow.Pending, GetAssignedUserId(Users, "ManagerOfManagerCase"));
            }
        }
        private class BlueCollarEmployeeExcusedAbsenceWorkflowFactory(IEnumerable<AdUser> Users) : IWorkflowFactory
        {
            WorkflowFactoryDto IWorkflowFactory.Create()
            {
                return new(Workflow.Pending, GetAssignedUserId(Users, "ManagerCase"));
            }
        }
        private class ManagerWorkflowFactory() : IWorkflowFactory
        {
            WorkflowFactoryDto IWorkflowFactory.Create()
            {
                return new(Workflow.None, null);
            }
        }
        private class WhiteCollarEmployeeWorkflowFactory(IEnumerable<AdUser> Users) : IWorkflowFactory
        {
            WorkflowFactoryDto IWorkflowFactory.Create()
            {
                return new(Workflow.Pending, GetAssignedUserId(Users, "EmployeeManagerCase"));
            }
        }
        private static Guid? GetAssignedUserId(IEnumerable<AdUser> Users, string cases)
        {
            Guid? managerId = null;

            switch (cases)
            {
                case "ManagerOfManagerCase":
                    foreach (var user in Users.Where(user => user.ManagerId == null))
                        managerId = user.Id;
                    break;
                default:
                    foreach (var user in Users.Where(user => user.LeaveRequests != null))
                        managerId = user.ManagerId;
                    break;
            }
            return managerId;
        }
        internal static IWorkflowFactory Workflows(UserType userType, LeaveType leaveType, IEnumerable<AdUser> users)
        {
            switch (userType)
            {
                case UserType.WhiteCollarEmployee:
                    return new WhiteCollarEmployeeWorkflowFactory(users);
                case UserType.BlueCollarEmployee when leaveType == LeaveType.AnnualLeave:
                    return new BlueCollarEmployeeAnnualLeaveWorkflowFactory(users);
                case UserType.BlueCollarEmployee when leaveType == LeaveType.ExcusedAbsence:
                    return new BlueCollarEmployeeExcusedAbsenceWorkflowFactory(users);
                case UserType.Manager:
                    return new ManagerWorkflowFactory();
                default:
                    throw new ExceptionMessage("WorkFlowFactory hata aldı!!");
            }
        }
    }
}
