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
    [Route("api/JobAPI")]
    [ApiController]
    public class JobAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IJobRepository _dbJob;
        private readonly ICompanyRepository _dbCompany;
        private readonly IPositionRepository _dbPosition;
        private readonly ILocationRepository _dbLocation;
        private readonly IMapper _mapper;

        public JobAPIController(IJobRepository dbJob, IMapper mapper, ICompanyRepository dbCompany, IPositionRepository dbPosition, ILocationRepository dbLocation)
        {
            this._response = new();
            _dbJob = dbJob;
            _dbCompany = dbCompany;
            _dbPosition = dbPosition;
            _dbLocation = dbLocation;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetJobList(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Job> jobList = await _dbJob.GetAllAsync(filter: null, pageSize: pageSize, pageNumber: pageNumber, includes: new Expression<Func<Job, object>>[]
                                  {
                                      x => x.Positions,
                                      x => x.Locations,
                                      x => x.Company
                                  });
                Pagination pagination = new() { pageNumber = pageNumber, pageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
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
                    return BadRequest(_response.IsSuccess = false);
                }
                var job = await _dbJob.GetAsync(filter: x => x.Id == id, includes: new Expression<Func<Job, object>>[]
                                  {
                                      x => x.Positions,
                                      x => x.Locations,
                                      x => x.Company
                                  });
                if (job == null)
                {
                    return NotFound(_response.IsSuccess = false);
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
                var locations = await _dbLocation.GetAllAsync(x => createDTO.LocationIds.Contains(x.Id));
                var positions = await _dbPosition.GetAllAsync(x => createDTO.PositionIds.Contains(x.Id));

                Job job = _mapper.Map<Job>(createDTO);

                job.Locations = locations;
                job.Positions = positions;

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
                    return BadRequest(_response.IsSuccess = false);
                }
                var job = await _dbJob.GetAsync(u => u.Id == id);
                if (job == null)
                {
                    return NotFound(_response.IsSuccess = false);
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
                    return BadRequest(_response.IsSuccess = false);
                }
                if (await _dbCompany.GetAsync(u => u.Id == updateDTO.CompanyID) == null)
                {
                    ModelState.AddModelError("CustomError", "Company ID is Invalid!");
                    return BadRequest();
                }

                Job existingJob = await _dbJob.GetAsync(x => x.Id == id, includes: new Expression<Func<Job, object>>[]
                {
                    x => x.Positions,
                    x => x.Locations
                });

                if (existingJob == null)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                _mapper.Map(updateDTO, existingJob);

                var newLocations = await _dbLocation.GetAllAsync(x => updateDTO.LocationIds.Contains(x.Id));
                var existingLocations = existingJob.Locations.ToList();

                foreach (var location in existingLocations)
                {
                    if (!newLocations.Any(x => x.Id == location.Id))
                    {
                        existingJob.Locations.Remove(location);
                    }
                }

                foreach (var location in newLocations)
                {
                    if (!existingLocations.Any(x => x.Id == location.Id))
                    {
                        existingJob.Locations.Add(location);
                    }
                }

                var newPositions = await _dbPosition.GetAllAsync(x => updateDTO.PositionIds.Contains(x.Id));
                var existingPositions = existingJob.Positions.ToList();

                foreach (var position in existingPositions)
                {
                    if (!newPositions.Any(x => x.Id == position.Id))
                    {
                        existingJob.Positions.Remove(position);
                    }
                }

                foreach (var position in newPositions)
                {
                    if (!existingPositions.Any(x => x.Id == position.Id))
                    {
                        existingJob.Positions.Add(position);
                    }
                }

                await _dbJob.UpdateAsync(existingJob);
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