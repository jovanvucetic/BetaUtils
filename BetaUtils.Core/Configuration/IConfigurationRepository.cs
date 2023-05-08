using System;
using BetaUtils.Core.Exceptions;

namespace BetaUtils.Core.Configuration
{
    public interface IConfigurationRepository
    {
        /// <summary>
        /// This method will return text value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Text value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist</exception>
        string GetStringValue(string key);

        /// <summary>
        /// This method will return text value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <param items="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Text value of the configuration item</returns>
        string GetStringValueOrDefault(string key, string defaultValue = "");

        /// <summary>
        /// This method will return an integer value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Int value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not an integer</exception>
        int GetIntValue(string key);

        /// <summary>
        /// This method will return an integer value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <param items="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Int value of the configuration item</returns>
        int GetIntValueOrDefault(string key, int defaultValue = default(int));

        /// <summary>
        /// This method will return a Guid value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Guid value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a Guid</exception>
        Guid GetGuidValue(string key);

        /// <summary>
        /// This method will return an Guid value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <param items="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Guid value of the configuration item</returns>
        Guid GetGuidValueOrDefault(string key, Guid defaultValue = default(Guid));

        /// <summary>
        /// This method will return a decimal value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Decimal value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a decimal</exception>
        decimal GetDecimalValue(string key);

        /// <summary>
        /// This method will return a decimal value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <param items="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Decimal value of the configuration item</returns>
        decimal GetDecimalValueOrDefault(string key, decimal defaultValue = default(decimal));

        /// <summary>
        /// This method will return a double value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Double value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a double</exception>
        double GetDoubleValue(string key);

        /// <summary>
        /// This method will return a double value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <param items="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Double value of the configuration item</returns>
        double GetDoubleValueOrDefault(string key, double defaultValue = default(double));

        /// <summary>
        /// This method will return a boolean value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Boolean value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a boolean</exception>
        bool GetBooleanValue(string key);

        /// <summary>
        /// This method will return a boolean value of a configuration item for provided key. 
        /// If an item for the provided key does not exist, this method will return a provided default value
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <param items="defaultValue">Value that will be returned if configuration item with provided key does not exist</param>
        /// <returns>Boolean value of the configuration item</returns>
        bool GetBooleanValueOrDefault(string key, bool defaultValue = false);

        /// <summary>
        /// This method will return a new instance of a provided type, taken from the configuration item for provided key
        /// </summary>
        /// <param items="sectionName">Key of the configuration item</param>
        /// <returns>Instance of the provided type T, with values of the configuration item</returns>
        /// <exception cref="ArgumentNullException">If a provided section name is null or empty</exception>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist</exception>
        T GetConfigurationSection<T>(string sectionName) where T : class, new();

        /// <summary>
        /// This method will return a DateTime value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>DateTime value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a DateTime</exception>
        DateTime GetDateValue(string key);

        /// <summary>
        /// This method will return an Uri value of a configuration item for provided key
        /// </summary>
        /// <param items="key">Key of the configuration item</param>
        /// <returns>Uri value of the configuration item</returns>
        /// <exception cref="MissingConfigurationException">If item for provided key does not exist or it is not a Uri</exception>
        Uri GetUri(string key);
    }
}
