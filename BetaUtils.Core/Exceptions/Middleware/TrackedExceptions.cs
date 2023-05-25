using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BetaUtils.Core.Exceptions.Middleware;

public static class TrackedExceptions
{
    private static Dictionary<Type, int> _exceptions = DefaultExceptions();

    /// <summary>
    /// Add exception with status code to tracked exceptions. Only one exception of same type may exist in dictionary.
    /// </summary>
    /// <returns>True if exception added successfully.</returns>
    public static bool Add<TException>(int statusCode) where TException : Exception
    {
        if (_exceptions.ContainsKey(typeof(TException)))
        {
            return false;
        }

        _exceptions.Add(typeof(TException), statusCode);

        return true;
    }

    /// <summary>
    /// Remove exception from dictionary if tracked.
    /// </summary>
    /// <returns>True if exception is successfully removed.</returns>
    public static bool Remove<TException>() where TException : Exception
    {
        return _exceptions.Remove(typeof(TException));
    }

    /// <summary>
    /// Gets all exceptions that are tracked.
    /// </summary>
    public static Dictionary<Type, int> GetAllExceptions()
    {
        return _exceptions;
    }

    /// <summary>
    /// Update tracked exception with new status code.
    /// </summary>
    /// <param name="exception">Exception on which status code need to be updated.</param>
    /// <param name="statusCode">New status code for exception.</param>
    /// <returns>True if exception status code is updated successfully.</returns>
    public static bool ChangeExceptionStatusCode(Exception exception, int statusCode)
    {
        if (!_exceptions.TryGetValue(exception.GetType(), out int _))
        {
            return false;
        }

        _exceptions[exception.GetType()] = statusCode;

        return true;
    }

    /// <summary>
    /// Check if exception is tracked.
    /// </summary>
    /// <returns>True if exception exist in dictionary.</returns>
    public static bool ExceptionExist(Exception exception)
    {
        return _exceptions.ContainsKey(exception.GetType());
    }

    /// <summary>
    /// Get status code of exception if tracked.
    /// </summary>
    /// <returns>If exception is not tracked, 0 will be returned</returns>
    public static int GetExceptionStatusCode(Exception exception)
    {
        _exceptions.TryGetValue(exception.GetType(), out int value);

        return value;
    }

    /// <summary>
    /// Reverting dictionary of exceptions to default dictionary.
    /// </summary>
    public static void ResetToDefault()
    {
        _exceptions = DefaultExceptions();
    }

    /// <summary>
    /// On exception dictionary initialization we want to have exceptions supported from beginning (by default).
    /// </summary>
    private static Dictionary<Type, int> DefaultExceptions()
    {
        return new Dictionary<Type, int>
        {
            { typeof(ArgumentNullException), StatusCodes.Status400BadRequest },
        };
    }
}