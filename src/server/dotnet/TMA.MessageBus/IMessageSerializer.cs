namespace TMA.MessageBus
{
    public interface IMessageSerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string obj);
    }
}
