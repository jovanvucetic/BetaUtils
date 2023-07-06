using BetaUtils.Core.Configuration;
using BetaUtils.Core.FeatureFlags;
using Moq;

namespace BetaUtils.Core.Tests.FeatureFlags
{
    [TestClass]
    public class FeatureFlagManagerTests
    {
        [TestMethod]
        public void Constructor_AllParametersProvided_InstanceCreatedSuccessfully()
        {
            var target = new FeatureFlagManagerTestContext().CreateFeatureFlagManager();
            Assert.IsNotNull(target);
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void IsFeatureEnabled_AnyCase_ValueReturnedFromConfiguration(bool isFeatureEnabled)
        {
            var inputValue = "qwerty";
            var context = new FeatureFlagManagerTestContext();

            context.ConfigurationRepository.Setup(x => x.GetBooleanValueOrDefault($"Features:{inputValue}", false)).Returns(isFeatureEnabled);

            var featureFlagManager = context.CreateFeatureFlagManager();

            var result = featureFlagManager.IsFeatureEnabled(inputValue);

            Assert.AreEqual(isFeatureEnabled, result);
        }

        private class FeatureFlagManagerTestContext
        {
            internal Mock<IConfigurationRepository> ConfigurationRepository;

            public FeatureFlagManagerTestContext()
            {
                ConfigurationRepository = new Mock<IConfigurationRepository>();
            }

            internal FeatureFlagManager CreateFeatureFlagManager()
               => new FeatureFlagManager(ConfigurationRepository.Object);
        }
    }
}