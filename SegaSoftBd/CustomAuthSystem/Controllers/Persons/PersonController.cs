using AutoMapper;
using CustomAuthSystem.CustomActionFilters;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RequestResponseDtos;
using CustomAuthSystem.ServicesInterface;
using CustomAuthSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthSystem.Controllers.Persons
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PersonController : ApiBaseController
    {
        private readonly IPersonManagementService _personManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly IMapper _mapper;

        public PersonController(IPersonManagementService personManagementService,
            IApplicationTime applicationTime,
            IMapper mapper)
        {
            _personManagementService = personManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
        }

        [HttpPost("CreatePerson")]
        [ValidateModel]
        public async Task<ActionResult<PersonDto>> CreatePersnAsync([FromBody] PersonCreateDtoRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var person = _mapper.Map<Person>(request);
            person.Id = Guid.NewGuid();
            person.CreatedDate = _applicationTime.GetCurrentTime();
            await _personManagementService.AddPersonAsync(person);

            var result = _mapper.Map<PersonDto>(person);
            return CreatedAtAction(nameof(PersonGetByIdAsync), new { Id = person.Id }, result);
        }

        [HttpGet("GetPersonList")]
        public async Task<ActionResult<IList<PersonDto>>> GetPersonListAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5, [FromQuery] string? search = null)
        {
            var (Items, CurrentPage, TotalPages, TotalItems) = await _personManagementService.GetPersonAsync(pageIndex, pageSize, search);

            if (Items == null)
            {
                return NotFound(new { Message = "The person was not found!" });
            }
            if (Items.Count == 0)
            {
                return Ok(new { Message = "The person List Is Empty!" });
            }
            var regionDto = _mapper.Map<IList<PersonDto>>(Items);
            return Ok(new
            {
                TotalItems,
                CurrentPage,
                TotalPages,
                Items = regionDto
            });
        }


        [HttpGet(), Route("GetByIdPerson/{id:guid}")]
        public async Task<ActionResult<PersonDto>> PersonGetByIdAsync([FromRoute] Guid id)
        {
            var region = await _personManagementService.GetByIdPersonAsync(id);
            if (region == null)
            {
                return NotFound(new { Message = "The person was not found!" });
            }
            var regionDto = _mapper.Map<PersonDto>(region);
            return Ok(regionDto);
        }


        [HttpPut, Route("PersonUpdate/{id:guid}")]
        [ValidateModel]
        public async Task<ActionResult<PersonDto>> PersonUpdateAsync([FromRoute] Guid id, [FromBody] PersonUpdateRequestDto request)
        {
            var person = await _personManagementService.GetByIdPersonAsync(id);
            if (person == null)
            {
                return NotFound(new { Message = "The region was not found!" });
            }

            var personUpdate = _mapper.Map(request, person);
            personUpdate.LastModifiedDate = _applicationTime.GetCurrentTime();
            await _personManagementService.UpdatePersonAsync(personUpdate);
            var result = _mapper.Map<PersonDto>(personUpdate);
            return Ok(new { Massege = "Person Update succesfully!", Result = result });
        }

        [HttpDelete, Route("DeletePerson/{id:guid}")]
        public async Task<ActionResult<PersonDto>> PersonDeleteAsync([FromRoute] Guid id)
        {
            var person = await _personManagementService.GetByIdPersonAsync(id);
            if (person == null)
            {
                return NotFound(new { Message = "The person was not found!" });
            }
            var result = _mapper.Map<PersonDto>(person);
            await _personManagementService.DeletePersonAsync(person);
            return Ok(new { Message = "Person has been succesfully deleted!", Result = result });
        }
    }
}