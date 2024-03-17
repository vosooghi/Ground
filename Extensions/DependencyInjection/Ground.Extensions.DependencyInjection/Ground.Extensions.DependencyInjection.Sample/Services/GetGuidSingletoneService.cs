using Ground.Extensions.DependencyInjection.Abstractions;

namespace Ground.Extensions.DependencyInjection.Sample.Services
{
    public class GetGuidSingletoneService:IGetGuidSingletoneService
    {
        private Guid _guid { get; set;}
        public GetGuidSingletoneService()
        {
            _guid = Guid.NewGuid();
        }
        public Guid Execute() => _guid;
    }
}
