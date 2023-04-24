using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [Route("api/LocationAPI")]
    [ApiController]
    public class LocationAPIController : Controller
    {
        protected APIResponse _response;
        private readonly ILocationRepository _dbLocation;
        private readonly IMapper _mapper;

        public LocationAPIController(ILocationRepository dbLocation, IMapper mapper)
        {
            this._response = new();
            this._dbLocation = dbLocation;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetLocationList()
        {
            try
            {
                var paginationResult = await _dbLocation.GetAllAsync();
                _response.Result = _mapper.Map<List<LocationDTO>>(paginationResult.Data);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var location = await _dbLocation.GetAsync(x => x.Id == id);


                if (location == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                _response.Result = _mapper.Map<LocationDTO>(location);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateLocation([FromBody] LocationCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Location location = _mapper.Map<Location>(createDTO);

                await _dbLocation.CreateAsync(location);

                _response.Result = _mapper.Map<LocationDTO>(location);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetLocation", new { id = location.Id }, _response); ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteLocation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var location = await _dbLocation.GetAsync(u => u.Id == id);
                if (location == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                await _dbLocation.RemoveAsync(location);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateLocation(int id, [FromBody] LocationUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest(_response.IsSuccess = false);
                }

                Location location = _mapper.Map<Location>(updateDTO);

                await _dbLocation.UpdateAsync(location);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
