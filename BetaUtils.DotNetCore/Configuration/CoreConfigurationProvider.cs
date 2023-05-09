using BetaUtils.Core.Configuration;
using BetaUtils.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Globalization;

namespace BetaUtils.DotNetCore.Configuration
{
    public class CoreConfigurationProvider : IConfigurationRepository
    {
        private readonly IConfiguration _configuration;

        public CoreConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// This method will return a boolean value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Boolean value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a boolean</exception>
        /// <exception cref="ArgumentException">If provided key is null</exception>
        public bool GetBooleanValue(string key)
        {
            var stringValue = GetStringValue(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            if (bool.TryParse(stringValue, out var result))
            {
                return result;
            }

            throw new MissingConfigurationException($"Configuration key '{key}' contains a value that is not boolean. The value supplied is '{stringValue}'.");
        }

        /// <summary>
        /// This method will return a boolean value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <param name="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Boolean value of the configuration item</returns>
        /// <exception cref="ArgumentException">If provided key is null</exception>
        public bool GetBooleanValueOrDefault(string key, bool defaultValue = false)
            => GetValueOrDefault(key, defaultValue);

        /// <summary>
        /// This method will return a new instance of a provided type, taken from the configuration item for provided key
        /// </summary>
        /// <param name="sectionName">Key of the configuration item</param>
        /// <returns>Instance of the provided type T, with values of the configuration item</returns>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist</exception>
        public T GetConfigurationSection<T>(string sectionName) where T : class, new()
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                throw new ArgumentException("Provided section name cannot be null or empty string");
            }

            var instance = new T();
            var section = _configuration.GetSection(sectionName);

            if (!section.Exists())
            {
                throw new MissingConfigurationException($"The configuration section '{sectionName}' cannot be found or is invalid.");
            }

            section.Bind(instance);

            return instance;
        }

        /// <summary>
        /// This method will return a DateTime value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>DateTime value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a DateTime</exception>
        /// <exception cref="ArgumentException">If provided key is null</exception>
        public DateTime GetDateValue(string key)
        {
            var stringValue = GetStringValue(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            if (DateTime.TryParse(stringValue, out var result))
            {
                return result;
            }

            throw new MissingConfigurationException($"Configuration key '{key}' contains a value that is not a date (culture en-GB). The invalid value supplied is '{stringValue}'.");
        }

        /// <summary>
        /// This method will return a decimal value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Decimal value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a decimal</exception>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public decimal GetDecimalValue(string key)
        {
            var stringValue = GetNormalizedNumericString(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new MissingConfigurationException($"Configuration key '{key}' contains a value that is not a decimal. The value supplied is '{stringValue}'.");
        }

        /// <summary>
        /// This method will return a decimal value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <param name="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Decimal value of the configuration item</returns>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public decimal GetDecimalValueOrDefault(string key, decimal defaultValue = 0)
            => GetValueOrDefault(key, defaultValue);

        /// <summary>
        /// This method will return a double value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Double value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a double</exception>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public double GetDoubleValue(string key)
        {
            var stringValue = GetNormalizedNumericString(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new MissingConfigurationException($"Configuration key '{key}' contains a value that is not a double. The value supplied is '{stringValue}'.");

        }

        /// <summary>
        /// This method will return a double value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <param name="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Double value of the configuration item</returns>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public double GetDoubleValueOrDefault(string key, double defaultValue = 0)
            => GetValueOrDefault(key, defaultValue);

        /// <summary>
        /// This method will return a Guid value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Guid value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a Guid</exception>
        public Guid GetGuidValue(string key)
        {
            var stringValue = GetStringValue(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            if (Guid.TryParse(stringValue, out var result))
            {
                return result;
            }

            throw new MissingConfigurationException($"Configuration key '{key}' contains a value that is not a unique identifier. The value supplied is '{stringValue}'.");
        }

        /// <summary>
        /// This method will return an Guid value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <param name="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Guid value of the configuration item</returns>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public Guid GetGuidValueOrDefault(string key, Guid defaultValue = default)
            => GetValueOrDefault(key, defaultValue);

        /// <summary>
        /// This method will return an integer value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Int value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not an integer</exception>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public int GetIntValue(string key)
        {
            var stringValue = GetStringValue(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            if (int.TryParse(stringValue, out var result))
            {
                return result;
            }

            throw new MissingConfigurationException($"Configuration key '{key}' contains a value that is not an integer. The value supplied is '{stringValue}'.");
        }

        /// <summary>
        /// This method will return an integer value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <param name="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Int value of the configuration item</returns>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public int GetIntValueOrDefault(string key, int defaultValue = 0)
            => GetValueOrDefault(key, defaultValue);

        /// <summary>
        /// This method will return text value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Text value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist</exception>
        /// <exception cref="ArgumentException">If provided key is null</exception>
        public string GetStringValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Provided configuration key cannot be null or empty string");
            }

            return _configuration[key];
        }

        /// <summary>
        /// This method will return text value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <param name="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Text value of the configuration item</returns>
        /// <exception cref="ArgumentException">If provided key is null</exception>
        public string GetStringValueOrDefault(string key, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Provided configuration key cannot be null or empty string");
            }

            var value = _configuration[key];
            return value ?? defaultValue;
        }

        /// <summary>
        /// This method will return an Uri value of a configuration item for provided key
        /// </summary>
        /// <param name="key">Key of the configuration item</param>
        /// <returns>Uri value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a Uri</exception>
        /// <exception cref="ArgumentException">If a provided section name is null or empty</exception>
        public Uri GetUri(string key)
        {
            var stringValue = GetStringValue(key);

            VerifyIfConfigurationValueExists(key, stringValue);

            return new Uri(stringValue);
        }

        private T GetValueOrDefault<T>(string key, T defaultValue = default)
        {
            try
            {
                string stringValue = GetStringValue(key);
                VerifyIfConfigurationValueExists(key, stringValue);

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                return (T)converter.ConvertFromString(stringValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        private string GetNormalizedNumericString(string key)
        {
            return GetStringValue(key)?.Replace(',', '.');
        }

        private static void VerifyIfConfigurationValueExists(string key, string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new MissingConfigurationException($"Unable to locate configuration key '{key}' or the value found is null or empty.");
            }
        }
    }
}
