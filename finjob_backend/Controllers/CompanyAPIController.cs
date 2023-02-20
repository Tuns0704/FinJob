using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace finjob_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetCompanyList()
        {
            return Ok(CompanyStore.CompanyList);
        }
        [HttpGet("{id:int}", Name ="GetCompany")]
        public ActionResult<CompanyDTO> GetCompany(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var company = CompanyStore.CompanyList.FirstOrDefault(x => x.Id == id);
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
            if(CompanyStore.CompanyList.FirstOrDefault(u => u.Name.ToLower()== companyDTO.Name.ToLower()) != null) 
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
            companyDTO.Id = CompanyStore.CompanyList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            CompanyStore.CompanyList.Add(companyDTO);
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
            var company = CompanyStore.CompanyList.FirstOrDefault(u => u.Id == id);
            if(company == null)
            {
                return NotFound();
            }
            CompanyStore.CompanyList.Remove(company);
            return NoContent();
        }
        [HttpPut("id:int", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateCompany(int id, [FromBody] CompanyDTO companyDTO) 
        {
            if(companyDTO == null)
            {
                return BadRequest();
            }
            var company = CompanyStore.CompanyList.FirstOrDefault(u => u.Id == id);
            company.Name = companyDTO.Name;
            company.Description = companyDTO.Description;
            company.Scale = companyDTO.Scale;
            company.Location = companyDTO.Location;
            return NoContent();
        }
    }
}
