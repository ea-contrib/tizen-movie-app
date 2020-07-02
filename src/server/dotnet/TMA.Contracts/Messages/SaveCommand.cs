namespace TMA.Contracts.Messages
{
    public class SaveCommand<TDto>: MessageBase
    {
        public SaveCommand()
        {
        }

        public SaveCommand(TDto data)
        {
            Data = data;
        }

        public TDto Data { get; set; }
    }
}