using System;

namespace BetaUtils.Core.Exceptions
{
    public class MissingConfigurationException : Exception
    {
        /// <summary>
        /// Default constructor fro the 
        /// </summary>
        public MissingConfigurationException() : this("Unable to locate a specified configuration section.") { }

        /// <summary>
        /// Constructor for the unknown configuration section with a supplied custom message.
        /// </summary>
        /// <param name="message"></param>
        public MissingConfigurationException(string message) : base(message) { }

        /// <summary>
        /// Constructor for the unknown configuration section with a defined inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public MissingConfigurationException(string message, Exception inner) : base(message, inner) { }
    }
}
