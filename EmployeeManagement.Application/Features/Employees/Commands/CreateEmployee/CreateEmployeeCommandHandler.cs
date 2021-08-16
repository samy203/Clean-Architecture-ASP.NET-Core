using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.Contracts.Infrastructure;
using EmployeeManagement.Application.Contracts.Persistence;
using EmployeeManagement.Application.Models.Mail;
using EmployeeManagment.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IAsyncRepository<Employee> employeeRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateEmployeeCommandHandler> logger;

        public CreateEmployeeCommandHandler(IAsyncRepository<Employee> employeeRepository, IMapper mapper, IEmailService emailService, ILogger<CreateEmployeeCommandHandler> logger)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEmployeeValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var employee = mapper.Map<Employee>(request);

            employee = await employeeRepository.AddAsync(employee);

            //Sending email notification to admin address
            var email = new Email() { To = "mostafa.samy203@gmail.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };

            try
            {
                await emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                logger.LogError($"Mailing about event {employee.Id} failed due to an error with the mail service: {ex.Message}");
            }

            return employee.Id;
        }
    }
}
