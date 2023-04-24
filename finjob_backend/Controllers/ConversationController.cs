using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [ApiController]
    [Route("api/Conversation")]
    public class ConversationController : Controller
    {
        protected APIResponse _response;
        private readonly IConversationRepository _dbConversation;
        private readonly IMapper _mapper;


        public ConversationController(IConversationRepository dbConversation, IMapper mapper)
        {
            this._response = new();
            _dbConversation = dbConversation;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateConversation([FromBody] ConversationDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                var conversation = await _dbConversation.GetByMembersAsync(createDTO.SenderId, createDTO.ReceiverId);

                if (conversation != null)
                {
                    return BadRequest("Conversation already exists");
                }

                conversation = _mapper.Map<Conversation>(createDTO);

                await _dbConversation.CreateAsync(conversation);

                _response.Result = _mapper.Map<ConversationDTO>(conversation);
                _response.StatusCode = HttpStatusCode.Created;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetConversationByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return BadRequest(userId);
                }

                List<Conversation> conversations = await _dbConversation.GetByUserIdAsync(userId);

                List<ConversationDTO> conversationDTOs = _mapper.Map<List<ConversationDTO>>(conversations);

                _response.Result = conversationDTOs;
                _response.StatusCode = HttpStatusCode.OK;

                return _response;
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
