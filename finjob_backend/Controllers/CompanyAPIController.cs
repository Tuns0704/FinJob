using AutoMapper;
using Azure;
using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finjob_backend.Controllers
{
    [Route("api/CompanyAPI")]
    [ApiController]
    public class CompanyAPIController : ControllerBase
    {
        private readonly ICompanyRepository _dbCompany;
        private readonly IMapper _mapper;
        public CompanyAPIController(ICompanyRepository dbCompany, IMapper mapper)
        {
            _dbCompany = dbCompany;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanyList()
        {
            IEnumerable<Company> companyList = await _dbCompany.GetAllAsync();
            return Ok(_mapper.Map<List<CompanyDTO>>(companyList));
        }

        [HttpGet("{id:int}", Name ="GetCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var company = await _dbCompany.GetAsync(x => x.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CompanyDTO>(company));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompanyDTO>> CreateCompany([FromBody] CompanyCreateDTO createDTO)
        {
            if(await _dbCompany.GetAsync(u => u.Name.ToLower()== createDTO.Name.ToLower()) != null) 
            {

                ModelState.AddModelError("CustomError","Company Already exist!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Company model = _mapper.Map<Company>(createDTO);

            await _dbCompany.CreateAsync(model);
            return CreatedAtRoute("GetCompany", new { id = model.Id }, model); ;
        }

        [HttpDelete("{id:int}", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var company = await _dbCompany.GetAsync(u => u.Id == id);
            if(company == null)
            {
                return NotFound();
            }
            await _dbCompany.RemoveAsync(company);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }

            Company model = _mapper.Map<Company>(updateDTO);

            await _dbCompany.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
