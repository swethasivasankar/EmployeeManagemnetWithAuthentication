using MediatR;
using Assignment.Contracts.DTO;
using Assignment.Contracts.Data;
using Assignment.Core.Exceptions;
using AutoMapper;

namespace Assignment.Providers.Handlers.Queries
{
    public class GetEmployerByIdQuery : IRequest<EmployerDTO>
    {
        public int EmployerId { get; }
        public GetEmployerByIdQuery(int employerId)
        {
            EmployerId = employerId;
        }
    }

    public class GetEmployerByIdQueryHandler : IRequestHandler<GetEmployerByIdQuery, EmployerDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetEmployerByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployerDTO> Handle(GetEmployerByIdQuery request, CancellationToken cancellationToken)
        {
            var employer = await Task.FromResult(_repository.Employer.Get(request.EmployerId));

            if (employer == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.EmployerId}");
            }

            return _mapper.Map<EmployerDTO>(employer);
        }
    }
}