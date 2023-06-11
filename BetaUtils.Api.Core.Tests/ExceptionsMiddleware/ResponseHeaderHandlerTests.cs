using System.Reflection;
using BetaUtils.Api.Core.Middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BetaUtils.Api.Core.Tests.ExceptionsMiddleware;

[TestClass]
public class ResponseHeaderHandlerTests
{
    private readonly HttpContext _context = new DefaultHttpContext();

    [TestMethod]
    public void ClearResponse_WillBeSuccessful()
    {
        //Arrange
        ContextSetup(null);
        MethodInfo? clearResponseMethod = InvokeMethod();

        object[] methodArguments = { _context, StatusCodes.Status400BadRequest };

        //Act
        clearResponseMethod?.Invoke(null, methodArguments);

        //Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, _context.Response.StatusCode);

        _context.Response.Headers.TryGetValue(HeaderNames.CacheControl, out StringValues cacheControl);
        Assert.AreEqual("no-cache, no-store, must-revalidate", cacheControl.ToString());

        _context.Response.Headers.TryGetValue(HeaderNames.Pragma, out StringValues pragma);
        Assert.AreEqual(CacheControlHeaderValue.NoCacheString, pragma.ToString());

        _context.Response.Headers.TryGetValue(HeaderNames.Expires, out StringValues cacheDuration);
        Assert.AreEqual("0", cacheDuration.ToString());
    }

    [TestMethod]
    public void ClearResponse_IfCacheHeaderIsNotFound_NewHeaderForCacheControlWillBeAdded()
    {
        //Arrange
        ContextSetup(HeaderNames.Allow);
        MethodInfo? clearResponseMethod = InvokeMethod();

        object[] methodArguments = { _context, StatusCodes.Status400BadRequest };

        //Act
        clearResponseMethod?.Invoke(null, methodArguments);

        //Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, _context.Response.StatusCode);

        _context.Response.Headers.TryGetValue(HeaderNames.CacheControl, out StringValues cacheControl);
        Assert.AreEqual("no-cache, no-store, must-revalidate", cacheControl.ToString());

        _context.Response.Headers.TryGetValue(HeaderNames.Pragma, out StringValues pragma);
        Assert.AreEqual(CacheControlHeaderValue.NoCacheString, pragma.ToString());

        _context.Response.Headers.TryGetValue(HeaderNames.Expires, out StringValues cacheDuration);
        Assert.AreEqual("0", cacheDuration.ToString());

        Assert.IsTrue(_context.Response.Headers.ContainsKey(HeaderNames.Allow));
    }

    private static MethodInfo? InvokeMethod()
    {
        return typeof(ResponseHeaderHandler).GetMethod(
            "ClearResponse",
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
    }

    private void ContextSetup(string? headerKey)
    {
        HeaderDictionary headers = new();

        if (headerKey is not null)
        {
            _context.Response.Headers.Add(new KeyValuePair<string, StringValues>(headerKey, new StringValues()));
        }

        _context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        headers.Append(HeaderNames.CacheControl, CacheControlHeaderValue.OnlyIfCachedString);
        headers.Append(HeaderNames.Pragma, CacheControlHeaderValue.OnlyIfCachedString);
        headers.Append(HeaderNames.Expires, "10000000");

        foreach (KeyValuePair<string, StringValues> header in headers)
        {
            _context.Response.Headers.Add(header);
        }
    }
}