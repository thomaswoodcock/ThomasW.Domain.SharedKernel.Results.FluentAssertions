using System;

using FluentAssertions;

using Xunit;
using Xunit.Sdk;

namespace ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests;

public class ResultAssertionsTests
{
    [Fact]
    public void BeSuccessful_SuccessfulResult_Passes()
    {
        // Arrange
        var result = Result.Success();

        // Act Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void BeSuccessful_FailedResult_Fails()
    {
        // Arrange
        var result = Result.Fail(new TestFailureReason());

        // Act
        Action action = () => result.Should().BeSuccessful("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a successful result because it should use the reason, but found a failed result " +
            "with the following failure reason: \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n{\n}.");
    }

    [Fact]
    public void BeSuccessful_SuccessfulTypedResult_Passes()
    {
        // Arrange
        var result = Result.Success("Test Value");

        // Act Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void BeSuccessful_FailedTypedResult_Fails()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act
        Action action = () => result.Should().BeSuccessful(because: "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a successful result because it should use the reason, but found a failed result " +
            "with the following failure reason: \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n{\n}.");
    }

    [Fact]
    public void BeSuccessful_MatchingValue_Passes()
    {
        // Arrange
        object value = new();
        var result = Result.Success(value);

        // Act Assert
        result.Should().BeSuccessful(value);
    }

    [Fact]
    public void BeSuccessful_FailedTypedResultWithNonMatchingValue_Fails()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act
        Action action = () => result.Should().BeSuccessful("Test Value", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a successful result because it should use the reason, but found a failed result " +
            "with the following failure reason: \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n{\n}.");
    }

    [Fact]
    public void BeSuccessful_NonMatchingValue_Fails()
    {
        // Arrange
        var result = Result.Success("Another Value");

        // Act
        Action action = () => result.Should().BeSuccessful("Test Value", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected result value to match \"Test Value\" because it should use the reason, but found" +
            " \"Another Value\".");
    }

    [Fact]
    public void BeSuccessful_MatchingPredicate_Passes()
    {
        // Arrange
        var result = Result.Success("Test Value");

        // Act Assert
        result.Should().BeSuccessful(value => value == "Test Value");
    }

    [Fact]
    public void BeSuccessful_FailedTypedResultWithNonMatchingPredicate_Fails()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act
        Action action = () => result.Should().BeSuccessful(
            value => value == "Test Value", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a successful result because it should use the reason, but found a failed result " +
            "with the following failure reason: \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n{\n}.");
    }

    [Fact]
    public void BeSuccessful_NonMatchingPredicate_Fails()
    {
        // Arrange
        var result = Result.Success("Another Value");

        // Act
        Action action = () => result.Should().BeSuccessful(
            value => value == "Test Value", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected result value to match value => (value == \"Test Value\") because it should use " +
            "the reason, but found \"Another Value\".");
    }

    [Fact]
    public void BeFailed_FailedResult_Passes()
    {
        // Arrange
        var result = Result.Fail(new TestFailureReason());

        // Act Assert
        result.Should().BeFailed();
    }

    [Fact]
    public void BeFailed_SuccessfulResult_Fails()
    {
        // Arrange
        var result = Result.Success();

        // Act
        Action action = () => result.Should().BeFailed("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.");
    }

    [Fact]
    public void BeFailed_MatchingReasonObject_Passes()
    {
        // Arrange
        TestFailureReason reason = new();
        var result = Result.Fail(reason);

        // Act Assert
        result.Should().BeFailed(reason);
    }

    [Fact]
    public void BeFailed_SuccessfulResultWithNonMatchingReasonObject_Fails()
    {
        // Arrange
        TestFailureReason reason = new();
        var result = Result.Success();

        // Act
        Action action = () => result.Should().BeFailed(reason, "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.");
    }

    [Fact]
    public void BeFailed_NonMatchingReasonObject_Fails()
    {
        // Arrange
        var result = Result.Fail(new TestFailureReason());

        // Act
        Action action = () => result.Should().BeFailed(
            new AnotherTestFailureReason(), "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failure reason matching \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            "\n{\n   Message = \"Test Message\"\n} because it should use the reason, but found \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n" +
            "{\n}.");
    }

    [Fact]
    public void BeFailed_MatchingReasonType_Passes()
    {
        // Arrange
        var result = Result.Fail(new TestFailureReason());

        // Act Assert
        result.Should().BeFailed<TestFailureReason>();
    }

    [Fact]
    public void BeFailed_SuccessfulResultWithNonMatchingReasonType_Fails()
    {
        // Arrange
        var result = Result.Success();

        // Act
        Action action = () => result.Should().BeFailed<TestFailureReason>("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.");
    }

    [Fact]
    public void BeFailed_NonMatchingReasonType_Fails()
    {
        // Arrange
        var result = Result.Fail(new TestFailureReason());

        // Act
        Action action = () =>
            result.Should().BeFailed<AnotherTestFailureReason>("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failure reason of type " +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            " because it should use the reason, but found \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n" +
            "{\n}.");
    }

    [Fact]
    public void BeFailed_MatchingPredicate_Passes()
    {
        // Arrange
        var result = Result.Fail(new AnotherTestFailureReason());

        // Act Assert
        result.Should().BeFailed<AnotherTestFailureReason>(reason => reason.Message == "Test Message");
    }

    [Fact]
    public void BeFailed_SuccessfulResultNonMatchingPredicate_Fails()
    {
        // Arrange
        var result = Result.Success();

        // Act
        Action action = () => result.Should().BeFailed<AnotherTestFailureReason>(
            reason => reason.Message == "Test Message", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.\n" +
            "Expected a failure reason of type " +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            " because it should use the reason, but found <null>.\n");
    }

    [Fact]
    public void BeFailed_NonMatchingPredicate_Fails()
    {
        // Arrange
        var result = Result.Fail(new AnotherTestFailureReason());

        // Act
        Action action = () => result.Should().BeFailed<AnotherTestFailureReason>(
            reason => reason.Message == "Another Message", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected failure reason to match reason => (reason.Message == \"Another Message\") because " +
            "it should use the reason, but found \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            "\n{\n   Message = \"Test Message\"\n}.");
    }

    [Fact]
    public void BeFailed_FailedTypedResult_Passes()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act Assert
        result.Should().BeFailed();
    }

    [Fact]
    public void BeFailed_TypedSuccessfulResult_Fails()
    {
        // Arrange
        var result = Result.Success("Test Value");

        // Act
        Action action = () => result.Should().BeFailed("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.");
    }

    [Fact]
    public void BeFailed_TypedResultMatchingReasonObject_Passes()
    {
        // Arrange
        TestFailureReason reason = new();
        var result = Result.Fail<string>(reason);

        // Act Assert
        result.Should().BeFailed(reason);
    }

    [Fact]
    public void BeFailed_TypedSuccessfulResultWithNonMatchingReasonObject_Fails()
    {
        // Arrange
        TestFailureReason reason = new();
        var result = Result.Success("Test Value");

        // Act
        Action action = () => result.Should().BeFailed(reason, "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.\n" +
            "Expected a failure reason matching \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n" +
            "{\n} because it should use the reason, but found <null>.\n");
    }

    [Fact]
    public void BeFailed_TypedResultNonMatchingReasonObject_Fails()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act
        Action action = () => result.Should().BeFailed(
            new AnotherTestFailureReason(), "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failure reason matching \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            "\n{\n   Message = \"Test Message\"\n} because it should use the reason, but found \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n" +
            "{\n}.");
    }

    [Fact]
    public void BeFailed_TypedResultMatchingReasonType_Passes()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act Assert
        result.Should().BeFailed<TestFailureReason>();
    }

    [Fact]
    public void BeFailed_TypedSuccessfulResultWithNonMatchingReasonType_Fails()
    {
        // Arrange
        var result = Result.Success("Test Value");

        // Act
        Action action = () => result.Should().BeFailed<TestFailureReason>("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.\n" +
            "Expected a failure reason of type " +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason" +
            " because it should use the reason, but found <null>.\n");
    }

    [Fact]
    public void BeFailed_TypedResultNonMatchingReasonType_Fails()
    {
        // Arrange
        var result = Result.Fail<string>(new TestFailureReason());

        // Act
        Action action = () =>
            result.Should().BeFailed<AnotherTestFailureReason>("because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failure reason of type " +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            " because it should use the reason, but found \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+TestFailureReason\n" +
            "{\n}.");
    }

    [Fact]
    public void BeFailed_TypedResultMatchingPredicate_Passes()
    {
        // Arrange
        var result = Result.Fail<string>(new AnotherTestFailureReason());

        // Act Assert
        result.Should().BeFailed<AnotherTestFailureReason>(reason => reason.Message == "Test Message");
    }

    [Fact]
    public void BeFailed_TypedSuccessfulResultNonMatchingPredicate_Fails()
    {
        // Arrange
        var result = Result.Success("Test Value");

        // Act
        Action action = () => result.Should().BeFailed<AnotherTestFailureReason>(
            reason => reason.Message == "Test Message", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected a failed result because it should use the reason, but found a successful result.\n" +
            "Expected a failure reason of type " +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            " because it should use the reason, but found <null>.\n" +
            "Expected failure reason to match reason => (reason.Message == \"Test Message\") because it should use the " +
            "reason, but found <null>.\n");
    }

    [Fact]
    public void BeFailed_TypedResultNonMatchingPredicate_Fails()
    {
        // Arrange
        var result = Result.Fail<string>(new AnotherTestFailureReason());

        // Act
        Action action = () => result.Should().BeFailed<AnotherTestFailureReason>(
            reason => reason.Message == "Another Message", "because it should use the {0}", "reason");

        // Assert
        action.Should().Throw<XunitException>().WithMessage(
            "Expected failure reason to match reason => (reason.Message == \"Another Message\") because " +
            "it should use the reason, but found \n\n" +
            "ThomasW.Domain.SharedKernel.Results.FluentAssertions.UnitTests.ResultAssertionsTests+AnotherTestFailureReason" +
            "\n{\n   Message = \"Test Message\"\n}.");
    }

    private class TestFailureReason : FailureReason
    {
    }

    private class AnotherTestFailureReason : FailureReason
    {
        internal AnotherTestFailureReason()
        {
            this.Message = "Test Message";
        }

        public string Message { get; }
    }
}
