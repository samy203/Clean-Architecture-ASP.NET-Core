using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment;
using EmployeeManagement.Application.Features.Departments.Queries.GetDepartments;
using EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeeDetail;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployees;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport;
using EmployeeManagment.Domain.Entities;

namespace EmployeeManagement.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, EmployeeDetailsVM>().ReverseMap();
            CreateMap<Employee, DeleteEmployeeCommand>().ReverseMap();
            CreateMap<Employee, EmployeeExportDTO>()
                .ForMember(dest => dest.DepartmentName, act => act.MapFrom(s => s.Department.Name)).ReverseMap();


            CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
        }
    }
}
