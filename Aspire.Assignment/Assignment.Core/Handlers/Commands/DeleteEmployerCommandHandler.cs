using MediatR;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Contracts.Data.Entities;
using FluentValidation;
using System.Text.Json;
using Assignment.Core.Exceptions;

namespace Assignment.Providers.Handlers.Commands
{
    public class DeleteEmployerCommand : IRequest<int>
    {
        public int EmployerId { get; }

        public DeleteEmployerCommand(int empId)
        {
            EmployerId = empId;
        }

        
    }

    public class DeleteEmployerCommandHandler : IRequestHandler<DeleteEmployerCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeleteEmployerCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteEmployerCommand request, CancellationToken cancellationToken)
        {

            var employer = await Task.FromResult(_repository.Employer.Get(request.EmployerId));
           if (employer == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.EmployerId}");
            }
            _repository.Employer.Delete(employer.EmployerId);
             await _repository.CommitAsync();

            return employer.EmployerId;
        }
    }
}