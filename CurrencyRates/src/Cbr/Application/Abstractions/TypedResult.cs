namespace Cbr.Application.Abstractions;


public record Result<T>
{
    private Result( bool isSuccess, T value, Error error )
    {
        if ( ( isSuccess && error != Error.None ) ||
             ( !isSuccess && error == Error.None ) )
        {
            throw new ArgumentException( "Invalid error", nameof(error) );
        }

        HasValue = isSuccess;
        Value = value;
        Error = error;
    }

    public T Value { get; }
    public bool HasValue { get; }
    public Error Error { get; }

    public static Result<T> Failure( Error error ) => new(false, default!, error);

    public static implicit operator Result<T>( T value ) => new(true, value, Error.None);

    public static implicit operator Result<T>( Error error ) => new(false, default!, error);
}