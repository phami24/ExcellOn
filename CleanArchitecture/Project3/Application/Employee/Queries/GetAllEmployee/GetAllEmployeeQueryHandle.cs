﻿using Application.DTOs.Employee;
using Domain.Interfaces;
using MediatR;

namespace Application.Employee.Queries.GetAllEmployee
{
    public class GetAllEmployeesQueryHandle : IRequestHandler<GetAllEmployeeQuery, ICollection<GetAllEmployeeDto>>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeesQueryHandle(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ICollection<GetAllEmployeeDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.All();
            var employeesDto = new List<GetAllEmployeeDto>();
            foreach (Domain.Entities.Employee e in employees)
            {
                var employeeDto = new GetAllEmployeeDto()
                {
                    Id = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Avatar = e.Avatar,
                    DepartmentId = e.DepartmentId,
                    Dob = e.Dob,
                    Email = e.Email,
                    Phone = e.Phone
                };
                employeesDto.Add(employeeDto);
            }
            return employeesDto;
        }
    }
}