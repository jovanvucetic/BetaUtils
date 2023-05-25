using BetaUtils.Core.Exceptions.Middleware;
using Microsoft.AspNetCore.Http;

namespace BetaUtils.Core.Tests.ExceptionsMiddleware;

[TestClass]
public class TrackedExceptionsTests
{
    [TestInitialize]
    public void Init()
    {
        TrackedExceptions.ResetToDefault();
    }

    [TestMethod]
    public void Add_WillBeSuccessful_WhenExceptionIsNotAlreadyTracked()
    {
        //Act
        bool exceptionAdded = TrackedExceptions.Add<ArgumentException>(StatusCodes.Status400BadRequest);

        //Assert
        Assert.AreEqual(2, TrackedExceptions.GetAllExceptions().Count);
        Assert.IsTrue(exceptionAdded);
    }

    [TestMethod]
    public void Add_WillNotAddException_WhenExceptionIsAlreadyTracked()
    {
        //Arrange
        TrackedExceptions.Add<ArgumentNullException>(StatusCodes.Status400BadRequest);

        //Act
        bool exceptionAdded = TrackedExceptions.Add<ArgumentNullException>(StatusCodes.Status400BadRequest);

        //Assert
        Assert.AreEqual(1, TrackedExceptions.GetAllExceptions().Count);
        Assert.IsFalse(exceptionAdded);
    }

    [TestMethod]
    public void Remove_WillBeSuccessful_IfExceptionIsTracked()
    {
        //Act
        bool exceptionRemoved = TrackedExceptions.Remove<ArgumentNullException>();

        //Assert
        Assert.IsTrue(exceptionRemoved);
        Assert.IsFalse(TrackedExceptions.GetAllExceptions().Any());
    }

    [TestMethod]
    public void Remove_WillReturnFalse_IfExceptionIsNotFound()
    {
        //Act
        TrackedExceptions.Remove<ArgumentNullException>();
        bool exceptionRemoved = TrackedExceptions.Remove<ArgumentNullException>();

        //Assert
        Assert.IsFalse(exceptionRemoved);
        Assert.IsFalse(TrackedExceptions.GetAllExceptions().Any());
    }

    [TestMethod]
    public void ChangeExceptionStatusCode_WillBeSuccessful_WhenExceptionIsTracked()
    {
        //Arrange
        TrackedExceptions.Add<TimeoutException>(StatusCodes.Status400BadRequest);

        //Act
        bool statusChanged = TrackedExceptions.ChangeExceptionStatusCode(new TimeoutException(), StatusCodes.Status100Continue);

        //Assert
        Assert.IsTrue(statusChanged);
    }

    [TestMethod]
    public void ChangeExceptionStatusCode_WillReturnFalse_IfExceptionNotTracked()
    {
        //Act
        bool statusChanged = TrackedExceptions.ChangeExceptionStatusCode(new TimeoutException(), StatusCodes.Status100Continue);

        //Assert
        Assert.IsFalse(statusChanged);
    }

    [TestMethod]
    public void ExceptionExist_WillReturnTrue_IfExceptionIsTracked()
    {
        //Act
        bool statusChanged = TrackedExceptions.ExceptionExist(new ArgumentNullException());

        //Assert
        Assert.IsTrue(statusChanged);
    }

    [TestMethod]
    public void ExceptionExist_WillReturnFalse_IfExceptionIsNotTracked()
    {
        //Arrange
        TrackedExceptions.Remove<ArgumentNullException>();

        //Act
        bool statusChanged = TrackedExceptions.ExceptionExist(new ArgumentNullException());

        //Assert
        Assert.IsFalse(statusChanged);
    }

    [TestMethod]
    public void GetExceptionStatusCode_WillReturnStatusCode_IfExceptionIsTracked()
    {
        //Act
        int statusCode = TrackedExceptions.GetExceptionStatusCode(new ArgumentNullException());

        //Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, statusCode);
    }

    [TestMethod]
    public void GetExceptionStatusCode_WillReturnZero_IfExceptionIsNotTracked()
    {
        //Arrange
        TrackedExceptions.Remove<ArgumentNullException>();

        //Act
        int statusCode = TrackedExceptions.GetExceptionStatusCode(new ArgumentNullException());

        //Assert
        Assert.AreEqual(0, statusCode);
    }
}