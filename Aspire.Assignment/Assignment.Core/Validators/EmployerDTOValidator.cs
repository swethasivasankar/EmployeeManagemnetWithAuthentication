using Assignment.Contracts.DTO;
using FluentValidation;

namespace Assignment.Core.Validators
{
    public class EmployerDTOValidator : AbstractValidator<EmployerDTO>
    {
        public EmployerDTOValidator()
        {
            RuleFor(x => x.TypeOfBusiness).NotEmpty().WithMessage("Type of business is required");
            RuleFor(x => x.EmployerEmailId).NotEmpty().WithMessage("EmailId is required");
             RuleFor(x => x.EmployerEmailId)  
              .Matches(@"^[a-zA-Z0-9.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Please enter a valid email")  
              .NotEmpty(); 
        }
    }
}
