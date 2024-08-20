# Result Design Pattern In .NET

## Description

The `Result` design pattern is a technique used to handle the outcomes of operations that can either succeed or fail.
This pattern is widely adopted in functional programming and has gained popularity in C# for its effectiveness in
managing errors and ensuring safe, predictable code flow.

## Basic Usage

The `Result` pattern encapsulates the result of an operation, which can either be successful or contain an error. Below
are the common methods used with the `Result` pattern:

### Creating a Result

- **Failure result**: Use `Result.Failure(error)` to represent a failed operation.
- **Success result**: Use `Result.Success()` to represent a successful operation without a value.
- **Success result with value**: Use `Result.Success(value)` to represent a successful operation that returns a value.

### Example

```csharp
Result.Success();

Result.Success("Operation completed successfully");

new Error {
    Code = "Unespected",
    Name = "Unespected Error",
    Description = "A Unespected Error Has Been Ocurred",
    Extensions = { ["Key"] = "Value" }
};

ErrorFactory.CreateSystemError(
    "Error", 
    "OperationFailed", 
    "An error occurred"
);

ErrorFactory.CreateHttpError(
    StatusCodes.InternalServerError,
    "OperationFailed",
    "An error occurred"
);

Result.Failure(Error);
```

### Matching on Result

```csharp
result.Match(
    onSuccess: () => "Operation succeeded",
    onFailure: error => $"Operation failed: {error.Message}"
);

result.Match(
    onSuccess: value => $"Operation succeeded, result: {value}",
    onFailure: error => $"Operation failed: {error.Message}"
);
```

## Benefits

- **Clear Error Handling**: Distinguishes between successful and failed operations, ensuring that error handling is
  explicit and predictable.
- **Immutable Results**: Once created, a Result object cannot be altered, making it safe to pass around in your
  codebase.
- **Functional Programming Influence**: Encourages a functional approach to handling outcomes, improving code
  readability and maintainability.

## Conclusion

The Result design pattern is a powerful tool for managing outcomes in C# applications, making your code more robust and
easier to maintain. By encapsulating success and failure cases, it enforces a clear structure for handling different
operation outcomes, leading to more reliable and bug-free code.


