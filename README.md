## ThomasW.Domain.SharedKernel.Results.FluentAssertions

[FluentAssertions](https://www.nuget.org/packages/FluentAssertions) extensions for
[ThomasW.Domain.SharedKernel.Results](https://github.com/thomaswoodcock/ThomasW.Domain.SharedKernel.Results).

## Installation

This package can be installed via [NuGet](https://www.nuget.org/packages/ThomasW.Domain.SharedKernel.Results.FluentAssertions).

## Usage

The package currently provides two assertion methods, `BeSuccessful` and
`BeFailed`, for both typed and non-typed results.

Simple checks on the success of a result can be made by using the methods with
no arguments:

```c#
public async Task GetUser_ValidId_ReturnsUser()
{
    // Arrange Act
    Result<User> result = await this._sut.GetUser(this._userId);

    // Assert
    result.Should().BeSuccessful();
}

public async Task GetUser_InvalidId_ReturnsUser()
{
    // Arrange Act
    Result<User> result = await this._sut.GetUser(Guid.NewGuid());

    // Assert
    result.Should().BeFailed();
}
```

If you want to assert on the value of a successful result, you can pass a
predicate into the method. The assertion will only pass if the result is
successful _and_ the predicate evaluates to `true`:

```c#
public async Task GetUser_ValidId_ReturnsUser()
{
    // Arrange Act
    Result<User> result = await this._sut.GetUser(this._userId);

    // Assert
    result.Should().BeSuccessful(user => user.Id == this._userId);
}
```

You can also assert on the failure reason by passing a type argument into the
`IsFailed` method:

```c#
public async Task GetUser_InvalidId_ReturnsUser()
{
    // Arrange Act
    Result<User> result = await this._sut.GetUser(Guid.NewGuid());

    // Assert
    result.Should().BeFailed<EntityNotFoundFailure>();
}
```

This assertion will only pass if the result is failed _and_ the failure reason
matches the passed-in type.

Furthermore, you can pass a predicate into the `IsFailed` method, as well as
the type parameter, to assert against the `FailureReason` object itself:

```c#
public async Task GetUser_InvalidId_ReturnsUser()
{
    // Arrange Act
    Result<User> result = await this._sut.GetUser(Guid.NewGuid());

    // Assert
    result.Should().BeFailed<EntityNotFoundFailure>(
        reason => reason.Message == "User not found");
}
```

The above assertion will only pass if the result is failed, the failure reason
matches the passed-in type _and_ the predicate evaluates to `true`.
