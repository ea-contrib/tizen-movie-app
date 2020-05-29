namespace TMA.Common.Interfaces
{
    public interface IRequestAware
    {
        object this[string key] { get; set; }
    }
}
