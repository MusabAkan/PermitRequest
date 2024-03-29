﻿using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Specifications
{
    public class AdUserSpec : Specification<AdUser>
    {
        public AdUserSpec(Guid? userId)
        {
            Query.Where(i => i.Id == userId);
        }
        public AdUserSpec()
        {
            Query.Where(i => i.ManagerId == null);
        }
    }
}
