using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Messages
{
    public class GetByIdCommand<TDto>: MessageBase
    {
        public int Id { get; set; }
        public GetByIdCommand() { }

        public GetByIdCommand(int id)
        {
            Id = id;
        }
    }
}
