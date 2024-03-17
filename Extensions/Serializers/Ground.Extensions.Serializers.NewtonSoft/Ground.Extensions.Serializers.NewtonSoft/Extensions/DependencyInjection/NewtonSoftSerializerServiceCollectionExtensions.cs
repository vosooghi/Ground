using Ground.Extensions.Serializers.Abstractions;
using Ground.Extensions.Serializers.NewtonSoft.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    public static class NewtonSoftSerializerServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundNewtonSoftSerializer(this IServiceCollection services)
            => services.AddSingleton<IJsonSerializer, NewtonSoftSerializer>();
    }
}
