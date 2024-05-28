namespace Klir.TechChallenge.Domain.Models
{
    public class Result<T> : DomainResult
    {
        public T Value { get; }

        protected Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static new Result<T> Fail(T value, string error)
        {
            return new Result<T>(value, false, error);
        }

        public static new Result<T> Fail(string error)
        {
            return new Result<T>(default(T), false, error);
        }
    }
}
