using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MessageBus;

namespace TMA.Web.Controllers
{
    [ApiController]
    public class MovieController: Controller
    {
        private readonly IMessageBus _messageBus;

        public MovieController(IMessageBus messageBus)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        [Route("api/movie/list")]
        public async Task<List<MovieDto>> List()
        {
            var response = await _messageBus.PublishAsync<GetMoviesCommand, ResponseMessage<List<MovieDto>>>(new GetMoviesCommand());

            return response.Value;
        }
    }
}
