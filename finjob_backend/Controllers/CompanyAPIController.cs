using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace finjob_backend.Controllers
{
    [Route("api/CompanyAPI")]
    [ApiController]
    public class CompanyAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ICompanyRepository _dbCompany;
        private readonly ILocationRepository _dbLocation;
        private readonly IMapper _mapper;
        public CompanyAPIController(ICompanyRepository dbCompany, ILocationRepository dbLocation, IMapper mapper)
        {
            _dbCompany = dbCompany;
            _dbLocation = dbLocation;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetCompanyList(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                PaginationResult<Company> paginationResult = await _dbCompany.GetAllAsync(filter: null, pageSize: pageSize, pageNumber: pageNumber, includes: x => x.Locations);
                var pagination = new Pagination { PageSize = pageSize, PageNumber = pageNumber, TotalCount = paginationResult.TotalCount, TotalPages = paginationResult.TotalPages };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<CompanyDTO>>(paginationResult.Data);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }

        [HttpGet("{id:int}", Name = "GetCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetCompany(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var company = await _dbCompany.GetAsync(x => x.Id == id, includes: x => x.Locations);
                if (company == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                _response.Result = _mapper.Map<CompanyDTO>(company);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCompany([FromBody] CompanyCreateDTO createDTO)
        {
            try
            {
                if (await _dbCompany.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {

                    ModelState.AddModelError("CustomError", "Company Already exist!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                var locations = await _dbLocation.GetAllAsync(x => createDTO.LocationIds.Contains(x.Id));
                Company company = _mapper.Map<Company>(createDTO);
                company.Locations = locations.Data;

                await _dbCompany.CreateAsync(company);

                _response.Result = _mapper.Map<CompanyDTO>(company);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCompany", new { id = company.Id }, _response); ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> DeleteCompany(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(_response.IsSuccess = false);
                }
                var company = await _dbCompany.GetAsync(u => u.Id == id);
                if (company == null)
                {
                    return NotFound(_response.IsSuccess = false);
                }
                await _dbCompany.RemoveAsync(company);
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

        [HttpPut("{id:int}", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> UpdateCompany(int id, [FromBody] CompanyUpdateDTO updateDTO, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest(_response.IsSuccess = false);
                }

                var company = await _dbCompany.GetAsync(x => x.Id == id, includes: x => x.Locations);
                if (company == null)
                {
                    return NotFound();
                }

                _mapper.Map<CompanyUpdateDTO, Company>(updateDTO, company);

                var locationIds = updateDTO.LocationIds;
                var newLocationPaginationResult = await _dbLocation.GetAllAsync(filter: x => locationIds.Contains(x.Id), pageSize: pageSize, pageNumber: pageNumber);
                var newLocations = newLocationPaginationResult.Data;

                company.Locations = newLocations;

                await _dbCompany.UpdateAsync(company);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
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
