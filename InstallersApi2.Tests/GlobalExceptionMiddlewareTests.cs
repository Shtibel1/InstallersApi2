using BLL.Exceptions;
using InstallersApi.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace InstallersApi2.Tests;

public class GlobalExceptionMiddlewareTests
{
    private static async Task<(HttpContext ctx, ErrorDetails? body)> Run(RequestDelegate next)
    {
        var ctx = new DefaultHttpContext();
        ctx.Response.Body = new MemoryStream();
        var mw = new GlobalExceptionMiddleware(NullLogger<GlobalExceptionMiddleware>.Instance, next);
        await mw.InvokeAsync(ctx);
        ctx.Response.Body.Position = 0;
        var text = await new StreamReader(ctx.Response.Body).ReadToEndAsync();
        return (ctx, text.Length == 0 ? null : JsonConvert.DeserializeObject<ErrorDetails>(text));
    }

    [Fact]
    public async Task Passes_through_when_no_exception()
    {
        var (ctx, body) = await Run(c => { c.Response.StatusCode = 204; return Task.CompletedTask; });
        Assert.Equal(204, ctx.Response.StatusCode);
        Assert.Null(body);
    }

    [Fact]
    public async Task Maps_EntityNotFoundException_to_404_json()
    {
        var (ctx, body) = await Run(_ => throw new EntityNotFoundException("Assignment"));
        Assert.Equal(404, ctx.Response.StatusCode);
        Assert.Equal("application/json", ctx.Response.ContentType);
        Assert.Equal(404, body!.StatusCode);
        Assert.Contains("Entity Assignment not found", body.Message);
    }

    [Fact]
    public async Task Maps_unexpected_exception_to_500_json()
    {
        var (ctx, body) = await Run(_ => throw new InvalidOperationException("boom"));
        Assert.Equal(500, ctx.Response.StatusCode);
        Assert.Equal(500, body!.StatusCode);
        Assert.Contains("boom", body.Message);
    }

    [Theory]
    [InlineData(400, "Bad Request")]
    [InlineData(401, "Unauthorized")]
    [InlineData(403, "Forbidden")]
    [InlineData(404, "Not Found")]
    [InlineData(500, "Internal Server Error")]
    [InlineData(418, "Unknown status code")]
    public void StatusCodeMessageResolver_returns_expected_message(int code, string expected)
        => Assert.Equal(expected, StatusCodeMessageResolver.GetMessage(code));
}
