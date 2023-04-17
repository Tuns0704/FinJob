using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [Route("api/TopSkillAPI")]
    [Authorize(Roles = "Employee")]
    [ApiController]
    public class TopSkillController : Controller
    {
        protected APIResponse _response;
        private readonly ITopSkillRepository _dbTopSkill;
        private readonly IMapper _mapper;

        public TopSkillController(ITopSkillRepository dbTopSkill, IMapper mapper)
        {
            this._response = new();
            this._dbTopSkill = dbTopSkill;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetTopSkillList(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                IEnumerable<TopSkill> topSkillList = await _dbTopSkill.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                _response.Result = _mapper.Map<List<TopSkillDTO>>(topSkillList);
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

        [HttpGet("{id:int}", Name = "GetTopSkill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTopSkill(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var topSkill = await _dbTopSkill.GetAsync(x => x.Id == id);
                if (topSkill == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                _response.Result = _mapper.Map<TopSkillDTO>(topSkill);
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
        public async Task<ActionResult<APIResponse>> CreateTopSkill([FromBody] TopSkillCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                TopSkill topSkill = _mapper.Map<TopSkill>(createDTO);

                await _dbTopSkill.CreateAsync(topSkill);

                _response.Result = _mapper.Map<TopSkillDTO>(topSkill);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetTopSkill", new { id = topSkill.Id }, _response); ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteTopSkill")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteTopSkill(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var topSkill = await _dbTopSkill.GetAsync(u => u.Id == id);
                if (topSkill == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                await _dbTopSkill.RemoveAsync(topSkill);
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

        [HttpPut("{id:int}", Name = "UpdateTopSkill")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateTopSkill(int id, [FromBody] TopSkillUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest(_response.IsSuccess = false);
                }

                TopSkill topSkill = _mapper.Map<TopSkill>(updateDTO);

                await _dbTopSkill.UpdateAsync(topSkill);
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
