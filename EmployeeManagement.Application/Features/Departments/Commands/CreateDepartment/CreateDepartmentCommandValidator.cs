using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.Contracts.Persistence;
using FluentValidation;

namespace EmployeeManagement.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        private IDepartmentRepository departmentRepository;

        public CreateDepartmentCommandValidator(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public CreateDepartmentCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(p => p.Alias)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(8).WithMessage("{PropertyName} must not exceed 8 characters.");

            RuleFor(e => e)
                .MustAsync(IsNameUnique)
                .WithMessage("A department with the same name exists.");

            RuleFor(e => e)
                .MustAsync(IsAliasUnique)
                .WithMessage("A department with the same alias exists.");
        }

        private async Task<bool> IsNameUnique(CreateDepartmentCommand e, CancellationToken token)
        {
            return await departmentRepository.IsDepartmentNameUnique(e.Name);
        }

        private async Task<bool> IsAliasUnique(CreateDepartmentCommand e, CancellationToken token)
        {
            return await departmentRepository.IsDepartmentAliasUnique(e.Alias);
        }
    }
}
