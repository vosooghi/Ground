using Ground.Extensions.DependencyInjection.Abstractions;

namespace Ground.Extensions.DependencyInjection.Sample.Services
{
    public interface IGetGuidTransientService : ITransientLifetime
    {
        Guid Execute();
    }
}
