using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [Route("api/PositionAPI")]
    [ApiController]
    public class PositionAPIController : Controller
    {
        protected APIResponse _response;
        private readonly IPositionRepository _dbPosition;
        private readonly IMapper _mapper;

        public PositionAPIController(IPositionRepository dbPosition, IMapper mapper)
        {
            this._response = new();
            this._dbPosition = dbPosition;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPositionList()
        {
            try
            {
                IEnumerable<Position> positionList = await _dbPosition.GetAllAsync();
                _response.Result = _mapper.Map<List<PositionDTO>>(positionList);
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

        [HttpGet("{id:int}", Name = "GetPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPosition(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var position = await _dbPosition.GetAsync(x => x.Id == id);
                if (position == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<PositionDTO>(position);
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
        public async Task<ActionResult<APIResponse>> CreatePosition([FromBody] PositionCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Position position = _mapper.Map<Position>(createDTO);

                await _dbPosition.CreateAsync(position);

                _response.Result = _mapper.Map<PositionDTO>(position);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPosition", new { id = position.Id }, _response); ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeletePosition")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeletePosition(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var position = await _dbPosition.GetAsync(u => u.Id == id);
                if (position == null)
                {
                    return NotFound();
                }
                await _dbPosition.RemoveAsync(position);
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

        [HttpPut("{id:int}", Name = "UpdatePosition")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePosition(int id, [FromBody] PositionUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Position position = _mapper.Map<Position>(updateDTO);

                await _dbPosition.UpdateAsync(position);
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
