using AutoMapper;

namespace Business.Thesaurus.Mapping.Configuration
{
    /// <summary>
    /// AutoMapper configuration class
    /// </summary>
    public static class MappingConfiguration
    {
        /// <summary>
        /// Retrieve all AutoMapper profiles in IMapper.
        /// </summary>
        /// <returns></returns>
        public static IMapper Get()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                // All other mapping configuration profilesn be added here
                mc.AddThesaurusMappingProfiles();
            });

            IMapper mapper = mappingConfig.CreateMapper();

            return mapper;
        }

        /// <summary>
        /// All mapping profiles.
        /// </summary>
        /// <param name="mc">IMapperConfigurationExpression</param>
        private static void AddThesaurusMappingProfiles(this IMapperConfigurationExpression mc)
        {
            mc.AddProfile(new WordProfile());
        }
    }
}
