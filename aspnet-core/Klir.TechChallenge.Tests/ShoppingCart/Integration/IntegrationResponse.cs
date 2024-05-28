namespace Klir.TechChallenge.Tests.ShoppingCart.Integration
{
    public record IntegrationResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }
}
