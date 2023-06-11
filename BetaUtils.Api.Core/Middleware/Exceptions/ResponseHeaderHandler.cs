using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace BetaUtils.Api.Core.Middleware.Exceptions;

public static class ResponseHeaderHandler
{
    /// <summary>
    /// Removing caching headers and setting status code of response.
    /// </summary>
    internal static void ClearResponse(HttpContext context, int statusCode)
    {
        HeaderDictionary headers = new();

        foreach (KeyValuePair<string, StringValues> header in context.Response.Headers)
        {
            if (header.Key != HeaderNames.CacheControl && header.Key != HeaderNames.Pragma && header.Key != HeaderNames.Expires)
            {
                headers.Add(header);
            }
        }

        context.Response.Clear();
        context.Response.StatusCode = statusCode;

        headers.Append(HeaderNames.CacheControl, "no-cache, no-store, must-revalidate");
        headers.Append(HeaderNames.Pragma, "no-cache");
        headers.Append(HeaderNames.Expires, "0");

        foreach (KeyValuePair<string, StringValues> header in headers)
        {
            context.Response.Headers.Add(header);
        }
    }
}