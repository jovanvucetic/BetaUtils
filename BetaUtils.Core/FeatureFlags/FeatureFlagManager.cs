using BetaUtils.Core.Configuration;

namespace BetaUtils.Core.FeatureFlags
{
    public class FeatureFlagManager : IFeatureFlagManager
    {
        private const string FeatureFlagSectionName = "Features";
        private readonly IConfigurationRepository _configurationRepository;

        public FeatureFlagManager(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        /// <summary>
        /// This method will check for the feature by its name in the "Features" section of configuration and tell if the feature is enabled. 
        /// If feature cannot be found by name in the "Features" section, feature will be disabled by default.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns>Boolean value that tells if feature is enabled</returns>
        public bool IsFeatureEnabled(string featureName)
        {
            return _configurationRepository.GetBooleanValueOrDefault($"{FeatureFlagSectionName}:{featureName}");
        }
    }
}