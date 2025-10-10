namespace DotNet_Core_API_Gateway.GatewayInterfaces
{
    public interface IGatewayService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string endPoint, string cacheKey, TimeSpan? cacheDuration = null);
    }
}
