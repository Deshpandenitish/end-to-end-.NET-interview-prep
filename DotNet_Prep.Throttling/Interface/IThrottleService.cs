namespace DotNet_Prep.Throttling.Interface
{
    public interface IThrottleService
    {
        public bool IsRequestAllowed(string clientKey, int limit);
    }
}
