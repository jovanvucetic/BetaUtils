using BetaUtils.Core.Exceptions;
using BetaUtils.DotNetFramework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace BetaUtils.DotNetFramework.Tests.Configuration
{
    [TestClass]
    public class WindowsConfigurationProviderTests
    {
        [TestMethod]
        public void Constructor_AllParametersProvided_InstanceCreatedSuccessfully()
        {
            var target = new WindowsConfigurationProviderTestContext().CreateCoreConfigurationProvider();
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void GetBooleanValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";
            var expectedValue = "configurationValue1";

            var context = new WindowsConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValue);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetBooleanValue(expectedInputKey));
        }

        [TestMethod]
        public void GetBooleanValue_ValueCanBeParsed_BooleanValueReturned()
        {
            var expectedInputKey = "SomeTrueBooleanValue";
            var expectedValue = true;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetBooleanValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = true;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetBooleanValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ValueCanBeParsed_BooleanValueReturned()
        {
            var expectedInputKey = "SomeTrueBooleanValue";
            var expectedValue = true;
            var expectedValueAsString = expectedValue.ToString();

            var context = new WindowsConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetBooleanValueOrDefault(expectedInputKey, false);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetConfigurationSection_SectionNotFound_MissingConfigurationExceptionThrown()
        {
            var expectedInputKey = "configurationKey1";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetConfigurationSection<TestConfigurationSection>(expectedInputKey));
        }

        [TestMethod]
        public void GetConfigurationSection_SectionFound_SectionObjectReturnedSuccessfully()
        {
            var expectedInputKey = "TestConfigurationSection";
            var expectedSectionNameValue = "Section name";
            var expectedSectionDescriptionValue = "Section description";
            var expectedSectionNumberValue = 124;

            var context = new WindowsConfigurationProviderTestContext();

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

            var context = new WindowsConfigurationProviderTestContext();
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

            var context = new WindowsConfigurationProviderTestContext();
            context.AddConfigurationValue(expectedInputKey, expectedValueAsString);

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDateValue(expectedInputKey);

            Assert.AreEqual(expectedValue.Date, resultValue.Date);
        }

        [TestMethod]
        public void GetDecimalValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetDecimalValue(expectedInputKey));
        }

        [TestMethod]
        public void GetDecimalValue_ValueCanBeParsed_DateValueReturned()
        {
            var expectedInputKey = "ValidTestDecimal";
            var expectedValue = -79228162514264337593543950335.00M;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDecimalValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = 112.2M;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetDecimalValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ValueCanBeParsed_DecimalValueReturned()
        {
            var expectedInputKey = "ValidTestDecimal";
            var expectedValue = -79228162514264337593543950335.00M;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDecimalValueOrDefault(expectedInputKey, 0M);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetDoubleValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetDoubleValue(expectedInputKey));
        }

        [TestMethod]
        public void GetDoubleValue_ValueCanBeParsed_DateValueReturned()
        {
            var expectedInputKey = "TestNumericString";
            var expectedValue = 999.999;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDoubleValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = 112.2;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetDoubleValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ValueCanBeParsed_DoubleValueReturned()
        {
            var expectedInputKey = "TestNumericString";
            var expectedValue = 999.999;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetDoubleValueOrDefault(expectedInputKey, 0);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetGuidValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetGuidValue(expectedInputKey));
        }

        [TestMethod]
        public void GetGuidValue_ValueCanBeParsed_GuidValueReturned()
        {
            var expectedInputKey = "ValidTestGuid";
            var expectedValue = Guid.Parse("b47a8890-19cc-4a22-824a-3f1fa451fb10");

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetGuidValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetGuidValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = Guid.NewGuid();

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetGuidValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetGuidValueOrDefault_ValueCanBeParsed_GuidValueReturned()
        {
            var expectedInputKey = "ValidTestGuid";
            var expectedValue = Guid.Parse("b47a8890-19cc-4a22-824a-3f1fa451fb10");

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetGuidValueOrDefault(expectedInputKey, Guid.NewGuid());

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetIntValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetIntValue(expectedInputKey));
        }

        [TestMethod]
        public void GetIntValue_ValueCanBeParsed_IntValueReturned()
        {
            var expectedInputKey = "ValidTestInt";
            var expectedValue = 7;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetIntValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetIntValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = 1233;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetIntValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetIntValueOrDefault_ValueCanBeParsed_IntValueReturned()
        {
            var expectedInputKey = "ValidTestInt";
            var expectedValue = 7;

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetIntValueOrDefault(expectedInputKey, 0);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetStringValue_ValueCanBeParsed_StringValueReturned()
        {
            var expectedInputKey = "SomeStringValue";
            var expectedValue = "My string value";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetStringValue(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetStringValueOrDefault_ValueCannotBeParsed_DefaultValueReturned()
        {
            var expectedInputKey = "configurationKey1";
            var defaultValue = "defaultValue";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var result = configurationRepository.GetStringValueOrDefault(expectedInputKey, defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod]
        public void GetStringValueOrDefault_ValueCanBeParsed_StringValueReturned()
        {
            var expectedInputKey = "SomeStringValue";
            var expectedValue = "My string value";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetStringValueOrDefault(expectedInputKey, "defaultValue");

            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void GetUriValue_ValueCannotBeParsed_MissingConfigurationException()
        {
            var expectedInputKey = "configurationKey1";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            Assert.ThrowsException<MissingConfigurationException>(() => configurationRepository.GetUri(expectedInputKey));
        }

        [TestMethod]
        public void GetUriValue_ValueCanBeParsed_IntValueReturned()
        {
            var expectedInputKey = "SomeUriValue";
            var expectedValue = "https://test.com/";

            var context = new WindowsConfigurationProviderTestContext();

            var configurationRepository = context.CreateCoreConfigurationProvider();

            var resultValue = configurationRepository.GetUri(expectedInputKey);

            Assert.AreEqual(expectedValue, resultValue.ToString());
        }

        private class WindowsConfigurationProviderTestContext
        {
            public WindowsConfigurationProviderTestContext()
            {
                ConfigurationManager.RefreshSection("appSettings");
            }

            public void AddConfigurationValue(string key, string value)
            {
                ConfigurationManager.AppSettings[key] = value;
            }

            internal WindowsConfigurationProvider CreateCoreConfigurationProvider()
            {
                return new WindowsConfigurationProvider();
            }
        }
    }

    public class TestConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("sectionName", IsRequired = true, IsKey = true)]
        public string SectionName
        {
            get => (string)this["sectionName"];
            set => this["sectionName"] = value;
        }


        [ConfigurationProperty("sectionDescription", IsRequired = true, IsKey = true)]
        public string SectionDescription
        {
            get => (string)this["sectionDescription"];
            set => this["sectionDescription"] = value;
        }


        [ConfigurationProperty("sectionNumber", IsRequired = true, IsKey = true)]
        public int SectionNumber
        {
            get => (int)this["sectionNumber"];
            set => this["sectionNumber"] = value;
        }
    }
}