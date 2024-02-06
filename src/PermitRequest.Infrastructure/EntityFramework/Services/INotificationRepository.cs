﻿using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Services
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }
}