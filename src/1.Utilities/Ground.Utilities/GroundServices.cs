

using Ground.Extensions.Caching.Abstractions;
using Ground.Extensions.ObjectMappers.Abstractions;
using Ground.Extensions.Serializers.Abstractions;
using Ground.Extensions.Translations.Abstractions;
using Ground.Extensions.UsersManagement.Abstractions;
using Microsoft.Extensions.Logging;

namespace Ground.Utilities
{

    public class GroundServices
    {
        public readonly ITranslator Translator;
        public readonly ICacheAdapter CacheAdapter;
        public readonly IMapperAdapter MapperFacade;
        public readonly ILoggerFactory LoggerFactory;
        public readonly IJsonSerializer Serializer;
        public readonly IUserInfoService UserInfoService;

        public GroundServices(ITranslator translator,
                ILoggerFactory loggerFactory,
                IJsonSerializer serializer,
                IUserInfoService userInfoService,
                ICacheAdapter cacheAdapter,
                IMapperAdapter mapperFacade)
        {
            Translator = translator;
            LoggerFactory = loggerFactory;
            Serializer = serializer;
            UserInfoService = userInfoService;
            CacheAdapter = cacheAdapter;
            MapperFacade = mapperFacade;
        }
    }
}
