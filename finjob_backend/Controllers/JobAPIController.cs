using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [Route("api/JobAPI")]
    [ApiController]
    public class JobAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IJobRepository _dbJob;
        private readonly ICompanyRepository _dbCompany;
        private readonly IMapper _mapper;

        public JobAPIController(IJobRepository dbJob, IMapper mapper, ICompanyRepository dbCompany)
        {
            this._response = new();
            _dbJob = dbJob;
            _dbCompany = dbCompany;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetJobList()
        {
            try
            {
                IEnumerable<Job> jobList = await _dbJob.GetAllAsync();
                _response.Result = _mapper.Map<List<JobDTO>>(jobList);
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

        [HttpGet("{id:int}", Name = "GetJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetJob(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var job = await _dbJob.GetAsync(x => x.Id == id);
                if (job == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<JobDTO>(job);
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
        public async Task<ActionResult<APIResponse>> CreateJob([FromBody] JobCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                if (await _dbCompany.GetAsync(u => u.Id == createDTO.CompanyID) == null)
                {
                    ModelState.AddModelError("CustomError", "Company ID is Invalid!");
                    return BadRequest();
                }

                Job job = _mapper.Map<Job>(createDTO);

                await _dbJob.CreateAsync(job);

                _response.Result = _mapper.Map<JobDTO>(job);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetJob", new { id = job.Id }, _response); ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteJob(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var job = await _dbJob.GetAsync(u => u.Id == id);
                if (job == null)
                {
                    return NotFound();
                }
                await _dbJob.RemoveAsync(job);
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

        [HttpPut("{id:int}", Name = "UpdateJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateJob(int id, [FromBody] JobUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _dbCompany.GetAsync(u => u.Id == updateDTO.CompanyID) == null)
                {
                    ModelState.AddModelError("CustomError", "Company ID is Invalid!");
                    return BadRequest();
                }

                Job job = _mapper.Map<Job>(updateDTO);

                await _dbJob.UpdateAsync(job);
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
