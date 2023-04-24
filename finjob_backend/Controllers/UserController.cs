using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace finjob_backend.Controllers
{
    [Route("api/UserAPI")]
    [ApiController]
    public class UserController : Controller
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._response = new();
            this._mapper = mapper;
            _userManager = userManager;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> GetUserList(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                var users = await _userManager.Users
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var userDtos = new List<UserResponseDTO>();
                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    var userDto = _mapper.Map<UserResponseDTO>(user);
                    userDto.Role = role;
                    userDtos.Add(userDto);
                }
                var totalUsers = await _userManager.Users.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);
                var pagination = new Pagination { PageSize = pageSize, PageNumber = pageNumber, TotalCount = totalUsers, TotalPages = totalPages };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<UserResponseDTO>>(userDtos);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<UserDTO>(user);
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


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = result.Errors.Select(e => e.Description).ToList();
                    return _response;
                }

                _response.Result = _mapper.Map<UserDTO>(user);
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateUser(string id, UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = userUpdateDTO.Name;
                user.UserName = userUpdateDTO.UserName;
                user.NormalizedUserName = userUpdateDTO.UserName.ToUpper();
                user.Email = userUpdateDTO.Email;
                user.NormalizedEmail = userUpdateDTO.Email.ToUpper();
                user.Avatar = userUpdateDTO.Avatar;

                var resultUpdate = await _userManager.UpdateAsync(user);
                if (resultUpdate.Succeeded)
                {
                    _response.Result = _mapper.Map<UserDTO>(user);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = resultUpdate.Errors.Select(e => e.Description).ToList();
                    return _response;
                }
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
