using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

namespace finjob_backend.Controllers
{
    [Route("api/ApplyJobAPI")]
    [ApiController]
    public class UserApplyJobController : Controller
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IUserApplyJobRepository _dbApplyJob;

        public UserApplyJobController(IMapper mapper, IUserApplyJobRepository dbApplyJob)
        {
            _mapper = mapper;
            _dbApplyJob = dbApplyJob;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetUserApplyJob(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                var paginationResult = await _dbApplyJob.GetAllAsync(
                pageSize: pageSize,
                pageNumber: pageNumber,
                includes: new Expression<Func<UserApplyJob, object>>[]
                {
                    x => x.Job,
                    x => x.User,
                    x => x.Position,
                });

                var pagination = new Pagination
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    TotalCount = paginationResult.TotalCount,
                    TotalPages = paginationResult.TotalPages
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<UserApplyDTO>>(paginationResult.Data);
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

        [HttpGet("{id:int}", Name = "GetApplyJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApplyJob(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var applyJob = await _dbApplyJob.GetAsync(x => x.Id == id);
                if (applyJob == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                _response.Result = _mapper.Map<UserApplyDTO>(applyJob);
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
        public async Task<ActionResult<APIResponse>> ApplyJob([FromBody] UserApplyCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }

                var existingJob = await _dbApplyJob.GetByJobAndUser(createDTO.JobId, createDTO.UserId);
                if (existingJob != null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { ("You have apply this Job!") };
                    return _response;
                }

                UserApplyJob applyJob = _mapper.Map<UserApplyJob>(createDTO);

                await _dbApplyJob.CreateAsync(applyJob);

                _response.Result = _mapper.Map<UserApplyCreateDTO>(applyJob);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetApplyJob", new { id = applyJob.Id }, _response); ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteApplyJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteApplyJob(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var applyJob = await _dbApplyJob.GetAsync(u => u.Id == id);
                if (applyJob == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                await _dbApplyJob.RemoveAsync(applyJob);
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
