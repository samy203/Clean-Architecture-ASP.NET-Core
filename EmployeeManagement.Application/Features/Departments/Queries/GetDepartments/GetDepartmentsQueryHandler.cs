using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagment.Domain.Entities;
using MediatR;

namespace EmployeeManagement.Application.Features.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentVM>>
    {
        private readonly IAsyncRepository<Department> departmentRepository;
        private readonly IMapper mapper;

        public GetDepartmentsQueryHandler(IAsyncRepository<Department> departmentRepository, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        public async Task<List<DepartmentVM>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var allCategories = (await departmentRepository.ListAllAsync()).OrderBy(x => x.Name);
            return mapper.Map<List<DepartmentVM>>(allCategories);
        }
    }
}
