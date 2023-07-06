namespace BetaUtils.Core.FeatureFlags
{
    public interface IFeatureFlagManager
    {
        bool IsFeatureEnabled(string featureName);
    }
}
