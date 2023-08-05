using AutoMapper;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using MediatR;
using System.Linq;

namespace Assignment.Providers.Handlers.Queries
{
    
    public class GetAllEmployerQuery : IRequest<IEnumerable<EmployerDTO>>
    {
        
    }

    public class GetAllEmployerQueryHandler : IRequestHandler<GetAllEmployerQuery, IEnumerable<EmployerDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllEmployerQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployerDTO>> Handle(GetAllEmployerQuery  request, CancellationToken cancellationToken)
        {   
            var entities = await Task.FromResult(_repository.Employer.GetAll());            
            return _mapper.Map<IEnumerable<EmployerDTO>>(entities);
        }
    }
}