﻿using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee, int>
    {
    }
}