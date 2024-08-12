using AutoMapper;
using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Services;
using ChatApp.Services.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
        {
            var messages = await _messageService.ListAllAsync(paginationModel);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<List<MessageModel>>(messages),
                StatusCode = 200,
                TotalCount = await _messageService.CountAsync()
            };
        }

        [HttpGet("{id}")]
        public async Task<ReturnModel> Get(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<MessageModel>(message),
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<ReturnModel> Post([FromBody] MessageCreateModel messageCreateModel)
        {
            var newMessage = _mapper.Map<Message>(messageCreateModel);
            var messageResult = await _messageService.AddAsync(newMessage);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<MessageModel>(messageResult),
                StatusCode = 201
            };
        }

        [HttpPut]
        public async Task<ReturnModel> Put([FromBody] MessageUpdateModel messageUpdateModel)
        {
            var message = _mapper.Map<Message>(messageUpdateModel);
            var updatedMessage = await _messageService.UpdateAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<MessageModel>(updatedMessage),
                StatusCode = 200
            };
        }

        [HttpDelete("{id}")]
        public async Task<ReturnModel> Delete(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            await _messageService.DeleteAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = message,
                StatusCode = 200
            };
        }
    }
}