using Azure;
using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace finjob_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CompanyAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CompanyDTO>> GetCompanyList()
        {
            return Ok(_db.Companies.ToList());
        }

        [HttpGet("{id:int}", Name ="GetCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CompanyDTO> GetCompany(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var company = _db.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CompanyDTO> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            if(_db.Companies.FirstOrDefault(u => u.Name.ToLower()== companyDTO.Name.ToLower()) != null) 
            {

                ModelState.AddModelError("CustomError","Company Already exist!");
                return BadRequest(ModelState);
            }
            if (companyDTO == null)
            {
                return BadRequest(companyDTO);
            }
            if(companyDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Company model = new()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                Description = companyDTO.Description,
                Scale= companyDTO.Scale,
                Location = companyDTO.Location,
                ImageURL = companyDTO.ImageURL,
            };
            _db.Companies.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetCompany", new {id = companyDTO.Id}, companyDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCompany(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var company = _db.Companies.FirstOrDefault(u => u.Id == id);
            if(company == null)
            {
                return NotFound();
            }
            _db.Companies.Remove(company);
            return NoContent();
        }

        [HttpPut("id:int", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateCompany(int id, [FromBody] CompanyDTO companyDTO) 
        {
            if(companyDTO == null || id != companyDTO.Id)
            {
                return BadRequest();
            }
            Company model = new()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                Description = companyDTO.Description,
                Scale = companyDTO.Scale,
                Location = companyDTO.Location,
                ImageURL = companyDTO.ImageURL,
            };
            _db.Companies.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("id:int", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialCompany(int id, JsonPatchDocument<CompanyDTO>  patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var company = _db.Companies.FirstOrDefault(u => u.Id == id);

            CompanyDTO companyDTO = new()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Scale = company.Scale,
                Location = company.Location,
                ImageURL = company.ImageURL,
            };

            if(company == null) 
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(companyDTO, ModelState);
            Company model = new()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                Description = companyDTO.Description,
                Scale = companyDTO.Scale,
                Location = companyDTO.Location,
                ImageURL = companyDTO.ImageURL,
            };

            _db.Companies.Update(model);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
