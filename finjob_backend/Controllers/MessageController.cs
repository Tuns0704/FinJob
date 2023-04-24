using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;
using finjob_backend.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace finjob_backend.Controllers
{
    [ApiController]
    [Route("api/Message")]
    public class MessageController : Controller
    {
        protected APIResponse _response;
        private readonly IMessageRepository _dbMessage;
        private readonly IMapper _mapper;


        public MessageController(IMessageRepository dbMessage, IMapper mapper)
        {
            this._response = new();
            _dbMessage = dbMessage;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateMessage([FromBody] MessageDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Message message = _mapper.Map<Message>(createDTO); ;

                await _dbMessage.CreateAsync(message);

                _response.Result = _mapper.Map<MessageDTO>(message);
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

        [HttpGet("{conversationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetMessageByCoversationId(int conversationId)
        {
            try
            {
                var messages = await _dbMessage.GetByConversationIdAsync(conversationId);

                if (messages == null)
                {
                    return BadRequest();
                }

                _response.Result = _mapper.Map<List<MessageDTO>>(messages);
                _response.StatusCode = HttpStatusCode.OK;
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
