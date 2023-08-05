using MediatR;
using Assignment.Contracts.DTO;
using Assignment.Contracts.Data;
using Assignment.Core.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Assignment.Contracts.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Assignment.Providers.Handlers.Queries
{
    public class SignInUserByUserNameQuery : IRequest<UserTokenDTO>
    {
        public string UserName { get; }
        public string PassWord { get; }

        //
        public SignInUserByUserNameQuery(string userName, string password)
        {
            UserName = userName;
            PassWord = password;
        }
    }

    public class SignInUserByUserNameQueryHandler : IRequestHandler<SignInUserByUserNameQuery, UserTokenDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;////Ioption to be changes 
   


        public SignInUserByUserNameQueryHandler(IUnitOfWork repository, IMapper mapper, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            
        }

        public async Task<UserTokenDTO> Handle(SignInUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var userInfo=new UserTokenDTO();
            var user = await Task.FromResult(_repository.User.GetAll().Where(con=>con.Username.Equals(request.UserName)).FirstOrDefault());
             var employeeDetail = await Task.FromResult(_repository.Employee.GetAll().Where(x=>x.UserId == user.Id).FirstOrDefault());
            if (user == null)
            {
                throw new EntityNotFoundException($"No User found for  {request.UserName}");
            }
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.PassWord);
            if(PasswordVerificationResult.Success!=result)
            {
                throw new InvalidcredentialsException($"Invalid credentials");
            }
            
//create instace for jwtsecurity token
                var tokenHandler = new JwtSecurityTokenHandler();
//Adding key i.e our secret key in appseting
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
//Below lines of code will generate new token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("userId", request.UserName),
                    new Claim(ClaimTypes.Role,user.Roles) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
              var token= tokenHandler.CreateToken(tokenDescriptor);
              userInfo.Token=tokenHandler.WriteToken(token);
              userInfo.Roles=user.Roles;
              //userInfo.EmpDetailsID=employeeDetail.UserId;
               userInfo.EmpDetailsID=employeeDetail==null?1:employeeDetail.EmployeeDetailId;
              

              return userInfo ?? throw new EntityNotFoundException($"Faild to generate the token");
        }

     
    }
}