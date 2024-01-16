﻿using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department, int>
    {
    }
}