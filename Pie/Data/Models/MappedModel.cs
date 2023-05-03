using Mapster;

namespace Pie.Data.Models
{
    public abstract class MappedModel : IRegister
    {
        public abstract void Register(TypeAdapterConfig config);
    }
}
