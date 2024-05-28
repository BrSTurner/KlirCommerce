namespace Klir.TechChallenge.Domain.Models
{
    public class DomainResult
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        protected DomainResult(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
            {
                throw new InvalidOperationException();
            }
            else if (!isSuccess && error == string.Empty)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static DomainResult Ok()
        {
            return new DomainResult(true, string.Empty);
        }

        public static DomainResult Fail(string error)
        {
            return new DomainResult(false, error);
        }
    }
}
