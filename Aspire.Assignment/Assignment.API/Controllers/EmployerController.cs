using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Providers.Handlers.Commands;
using Assignment.Providers.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployerDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
       // [Authorize]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllEmployerQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromBody] EmployerDTO model)
        {
            try
            {
                var command = new CreateEmployerCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(EmployerDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetEmployerByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Put([FromBody] EmployerDTO model)
        {
            try
            {
                var command = new UpdateEmployerCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }


        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(EmployerDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteEmployerCommand(id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
       }


    }
}