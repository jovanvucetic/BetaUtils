using BetaUtils.Core.Exceptions.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;

namespace BetaUtils.Core.Tests.ExceptionsMiddleware;

[TestClass]
public class ExceptionMiddlewareTests
{
    private readonly Mock<IActionResultExecutor<ObjectResult>> _actionResultExecutorMock = new();
    private readonly Mock<RequestDelegate> _requestDelegateMock = new();
    private ExceptionMiddleware _exceptionMiddleware = null!;

    [TestMethod]
    public async Task Invoke_WillBeSuccessful_WhenNoExceptionHappened()
    {
        //Arrange
        _exceptionMiddleware = new ExceptionMiddleware(_requestDelegateMock.Object, _actionResultExecutorMock.Object);

        //Act Assert
        await _exceptionMiddleware.Invoke(new DefaultHttpContext()).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task Invoke_WhenExceptionIsThrown_WillHandleIfExceptionIsTracked()
    {
        // Arrange
        DefaultHttpContext context = new();

        //Included tracked exception.
        ArgumentNullException exception = new($"Test Exception");
        _requestDelegateMock.Setup(n => n.Invoke(context)).Throws(exception);

        ModelStateDictionary modelState = new();
        modelState.AddModelError("TestException", exception.Message);

        _actionResultExecutorMock.Setup(e => e.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>())).Returns(Task.CompletedTask);

        ExceptionMiddleware exceptionMiddleware = new(_requestDelegateMock.Object, _actionResultExecutorMock.Object);

        // Act
        await exceptionMiddleware.Invoke(context).ConfigureAwait(false);

        // Assert
        _actionResultExecutorMock.Verify(e => e.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()), Times.Once);
    }

    [TestMethod]
    public async Task Invoke_WhenExceptionIsThrown_WillNotExecute()
    {
        // Arrange
        DefaultHttpContext context = new();

        //Not tracked exception.
        HttpRequestException exception = new("Test Exception");
        _requestDelegateMock.Setup(n => n.Invoke(context)).Throws(exception);

        ModelStateDictionary modelState = new();
        modelState.AddModelError("TestException", exception.Message);

        _actionResultExecutorMock.Setup(e => e.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>())).Returns(Task.CompletedTask);

        ExceptionMiddleware exceptionMiddleware = new(_requestDelegateMock.Object, _actionResultExecutorMock.Object);

        // Act

        // Assert
        await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await exceptionMiddleware.Invoke(context).ConfigureAwait(false))
            .ConfigureAwait(false);

        _actionResultExecutorMock.Verify(e => e.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()), Times.Never);
    }
}