using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [Route("api/CompanyAPI")]
    [ApiController]
    public class CompanyAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ICompanyRepository _dbCompany;
        private readonly IMapper _mapper;
        public CompanyAPIController(ICompanyRepository dbCompany, IMapper mapper)
        {
            _dbCompany = dbCompany;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetCompanyList()
        {
            try
            {
                IEnumerable<Company> companyList = await _dbCompany.GetAllAsync();
                _response.Result = _mapper.Map<List<CompanyDTO>>(companyList);
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
                    return BadRequest();
                }
                var company = await _dbCompany.GetAsync(x => x.Id == id);
                if (company == null)
                {
                    return NotFound();
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

                Company company = _mapper.Map<Company>(createDTO);

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
                    return BadRequest();
                }
                var company = await _dbCompany.GetAsync(u => u.Id == id);
                if (company == null)
                {
                    return NotFound();
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
        public async Task<ActionResult<APIResponse>> UpdateCompany(int id, [FromBody] CompanyUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Company model = _mapper.Map<Company>(updateDTO);

                await _dbCompany.UpdateAsync(model);
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

        [HttpPatch("{id:int}", Name = "UpdatePartialCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdatePartialCompany(int id, JsonPatchDocument<CompanyUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var company = await _dbCompany.GetAsync(u => u.Id == id, tracked: false);

            CompanyUpdateDTO companyDTO = _mapper.Map<CompanyUpdateDTO>(company);

            if (company == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(companyDTO, ModelState);

            Company model = _mapper.Map<Company>(companyDTO);

            await _dbCompany.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
