using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment;
using EmployeeManagement.Application.Features.Departments.Queries.GetDepartments;
using MediatR;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllDepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DepartmentVM>>> GetAllDepartments()
        {
            var dtos = await _mediator.Send(new GetDepartmentsQuery());
            return Ok(dtos);
        }

      

        [HttpPost(Name = "AddDepartment")]
        public async Task<ActionResult<CreateDepartmentCommandResponse>> AddDepartment([FromBody] CreateDepartmentCommand createDepartmentCommand)
        {
            var response = await _mediator.Send(createDepartmentCommand);
            return Ok(response);
        }
    }
}
