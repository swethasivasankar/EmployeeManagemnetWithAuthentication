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
    public class UpdateEmployerCommand : IRequest<int>
    {
        public EmployerDTO Model { get;}

        public UpdateEmployerCommand(EmployerDTO model)
        {
            this.Model = model;
        }

        
    }

    public class UpdateEmployerCommandHandler : IRequestHandler<UpdateEmployerCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployerDTO> _validator;



        public UpdateEmployerCommandHandler(IUnitOfWork repository,IValidator<EmployerDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator= validator;
            _mapper =mapper;
        }

        public async Task<int> Handle(UpdateEmployerCommand request, CancellationToken cancellationToken)
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
            _repository.Employer.Update(employer);
            await _repository.CommitAsync();

            return employer.EmployerId;
        }
    }
}