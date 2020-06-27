
namespace TMA.Data.Common
{
    public interface IIdentitySupporter<TId> where TId: struct
    {
        TId Id { get; set; }
    }
}
