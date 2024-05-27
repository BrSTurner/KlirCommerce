namespace Klir.TechChallenge.Core.Messages
{
    public class CommandResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }

        public static CommandResult<T> Ok(T data) => new CommandResult<T>
        {
            Success = true,
            Data = data
        };

        public static CommandResult<object> Ok() => new CommandResult<object>
        {
            Success = true,
            Data = null
        };

        public static CommandResult<object> Fail() => new CommandResult<object>
        {
            Data = null
        };
    }
}
