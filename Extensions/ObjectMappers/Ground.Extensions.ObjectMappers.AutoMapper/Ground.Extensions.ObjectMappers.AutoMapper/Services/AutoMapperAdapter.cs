using AutoMapper;
using Ground.Extensions.ObjectMappers.Abstractions;
using Microsoft.Extensions.Logging;

namespace Ground.Extensions.ObjectMappers.AutoMapper.Services
{
    /// <summary>
    /// Provides an implementation of the IMapperAdapter interface using AutoMapper.
    /// </summary>
    public class AutoMapperAdapter : IMapperAdapter
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AutoMapperAdapter> _logger;

        public AutoMapperAdapter(IMapper mapper, ILogger<AutoMapperAdapter> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("AutoMapper Adapter Start working");
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            _logger.LogTrace("AutoMapper Adapter Map {source} To {destination} with data {sourcedata}",
                             typeof(TSource),
                             typeof(TDestination),
                             source);

            return _mapper.Map<TDestination>(source);
        }
    }
}
