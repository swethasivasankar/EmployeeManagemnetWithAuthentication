using MediatR;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Contracts.Data.Entities;
using FluentValidation;
using System.Text.Json;
using Assignment.Core.Exceptions;
using AutoMapper;

namespace Assignment.Providers.Handlers.Commands
{
    public class CreateEmployerCommand : IRequest<int>
    {
        public EmployerDTO Model { get; }
        public CreateEmployerCommand(EmployerDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateEmployerCommandHandler : IRequestHandler<CreateEmployerCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<EmployerDTO> _validator;
        private readonly IMapper _mapper;


        public CreateEmployerCommandHandler(IUnitOfWork repository, IValidator<EmployerDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper= mapper;
        }

        public async Task<int> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {

            EmployerDTO model = _mapper.Map<EmployerDTO>(request.Model);

            var result = _validator.Validate(model);


            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var employer= _mapper.Map<Employer>(request.Model);
            _repository.Employer.Add(employer);
            await _repository.CommitAsync();

            return employer.EmployerId;
        }
    }
}