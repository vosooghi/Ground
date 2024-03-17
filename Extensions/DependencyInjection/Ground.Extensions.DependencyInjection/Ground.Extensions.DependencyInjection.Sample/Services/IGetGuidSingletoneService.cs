using Ground.Extensions.DependencyInjection.Abstractions;

namespace Ground.Extensions.DependencyInjection.Sample.Services
{
    public interface IGetGuidSingletoneService : ISingletoneLifetime
    {
        Guid Execute();
    }
}
