using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Api.Utilities;
using EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeeDetail;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployees;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport;
using MediatR;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EmployeeVM>>> GetAllEmployees()
        {
            var dtos = await _mediator.Send(new GetEmployeesQuery());
            return Ok(dtos);
        }


        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<ActionResult<EmployeeDetailsVM>> GetEmployeeById(int id)
        {
            var query = new GetEmployeeDetailsQuery { Id = id };
            return Ok(await _mediator.Send(query));
        }

        [HttpPost(Name = "AddEmployee")]
        public async Task<ActionResult<int>> Add([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            var id = await _mediator.Send(createEmployeeCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
        {
            await _mediator.Send(updateEmployeeCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteEmployeeCommand = new DeleteEmployeeCommand() { EmployeeId = id };
            await _mediator.Send(deleteEmployeeCommand);
            return NoContent();
        }

        [HttpGet("export", Name = "ExportEmployees")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents()
        {
            var fileDto = await _mediator.Send(new GetEmployeesExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.EmployeeExportFileName);
        }
    }
}
