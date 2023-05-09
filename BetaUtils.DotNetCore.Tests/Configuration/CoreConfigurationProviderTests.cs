using BetaUtils.Core.Exceptions;
using BetaUtils.DotNetCore.Configuration;
using Microsoft.Extensions.Configuration;

namespace BetaUtils.DotNetCore.Tests.Configuration
{
    [TestClass]
    public class CoreConfigurationProviderTests
    {
        [TestMethod]
        public void Constructor_AllParametersProvided_InstanceCreatedSuccessfully()
        {
            var target = new CoreConfigurationProviderTestContext().CreateCoreConfigurationProvider();
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void GetBooleanValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetBooleanValue(expectedInputKey));
        }

        [TestMethod]
        public void GetBooleanValue_ValueCanBeParsed_BooleanValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = true;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetBooleanValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";
            var defaultValue = true;

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetBooleanValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ValueCanBeParsed_BooleanValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = true;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetBooleanValueOrDefault(expectedInputKey, false);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetConfigurationSection_SectionNotFound_MissingConfigurationExceptionThrown()
        {
            var expectedInputKey = "configurationKey1";

            var context = new CoreConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetConfigurationSection<TestConfigurationSection>(expectedInputKey));
        }

        [TestMethod]
        public void GetConfigurationSection_SectionFound_SectionObjectReturnedSuccessfully()
        {
            var expectedInputKey = "sectionName";
            var expectedSectionNameKey = $"{expectedInputKey}:SectionName";
            var expectedSectionDescriptionKey = $"{expectedInputKey}:SectionDescription";
            var expectedSectionNumberKey = $"{expectedInputKey}:SectionNumber";
            var expectedSectionNameValue = "SectionName1";
            var expectedSectionDescriptionValue = "SectionDescription1";
            var expectedSectionNumberValue = 123;

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedSectionNameKey, expectedSectionNameValue);
            context.AddConfigurationValue(expectedSectionDescriptionKey, expectedSectionDescriptionValue);
            context.AddConfigurationValue(expectedSectionNumberKey, expectedSectionNumberValue.ToString());

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetConfigurationSection<TestConfigurationSection>(expectedInputKey);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSectionNameValue, result.SectionName);
            Assert.AreEqual(expectedSectionDescriptionValue, result.SectionDescription);
            Assert.AreEqual(expectedSectionNumberValue, result.SectionNumber);
        }

        [TestMethod]
        public void GetDateValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetDateValue(expectedInputKey));
        }

        [TestMethod]
        public void GetDateValue_ValueCanBeParsed_DateValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = DateTime.Now;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDateValue(expectedInputKey);

            Assert.AreEqual(expectedValue.Date, resultValue.Date);
        }

        [TestMethod]
        public void GetDecimalValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetDecimalValue(expectedInputKey));
        }

        [TestMethod]
        public void GetDecimalValue_ValueCanBeParsed_DateValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = 112.2M;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDecimalValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";
            var defaultValue = 112.2M;

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetDecimalValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ValueCanBeParsed_DecimalValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = 112.2M;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDecimalValueOrDefault(expectedInputKey, 0M);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetDoubleValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetDoubleValue(expectedInputKey));
        }

        [TestMethod]
        public void GetDoubleValue_ValueCanBeParsed_DateValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = 112.2;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDoubleValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";
            var defaultValue = 112.2;

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetDoubleValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ValueCanBeParsed_DoubleValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = 112.2;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDoubleValueOrDefault(expectedInputKey, 0);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetGuidValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetGuidValue(expectedInputKey));
        }

        [TestMethod]
        public void GetGuidValue_ValueCanBeParsed_GuidValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = Guid.NewGuid();
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetGuidValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetGuidValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";
            var defaultValue = Guid.NewGuid();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetGuidValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetGuidValueOrDefault_ValueCanBeParsed_GuidValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = Guid.NewGuid();
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetGuidValueOrDefault(expectedInputKey, Guid.NewGuid());

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetIntValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetIntValue(expectedInputKey));
        }

        [TestMethod]
        public void GetIntValue_ValueCanBeParsed_IntValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = 123;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetIntValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetUriValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";

            var context = new CoreConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetUri(expectedInputKey));
        }

        [TestMethod]
        public void GetUriValue_ValueCanBeParsed_IntValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "https://test.com/";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetUri(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue.ToString());
        }

        [TestMethod]
        public void GetIntValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";
            var defaultValue = 1233;

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetIntValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetIntValueOrDefault_ValueCanBeParsed_IntValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = 1234;
            var expectedValueAsString = expectedValue.ToString();

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetIntValueOrDefault(expectedInputKey, 0);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetStringValue_ValueCanBeParsed_StringValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetStringValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetStringValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = "defaultValue";

            var context = new CoreConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetStringValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetStringValueOrDefault_ValueCanBeParsed_StringValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new CoreConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetStringValueOrDefault(expectedInputKey, "defaultValue");

            Assert.AreEqual(expectedValue, resultValue);
        }

        private class CoreConfigurationProviderTestContext
        {
            internal IConfiguration Configuration;
            internal Dictionary<string, string> InMemoryConfigurationValues { get; set; }

            public CoreConfigurationProviderTestContext()
            {
                InMemoryConfigurationValues = new Dictionary<string, string>();
            }

            public void AddConfigurationValue(string key, string value)
            {
                InMemoryConfigurationValues.Add(key, value);
            }

            internal CoreConfigurationProvider CreateCoreConfigurationProvider()
            {
                Configuration = new ConfigurationBuilder()
                   .AddInMemoryCollection(InMemoryConfigurationValues)
                   .Build();

                return new CoreConfigurationProvider(Configuration);
            }
        }

        private class TestConfigurationSection
        {
            public string SectionName { get; set; }

            public string SectionDescription { get; set; }

            public int SectionNumber { get; set; }
        }
    }
}