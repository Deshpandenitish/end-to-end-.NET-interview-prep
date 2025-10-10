namespace DotNet_Core_API_Gateway.Helpers
{
    public static class HealthChecks
    {
        public static void WaitForDownStreamApiService(string url, int maxTries = 10, int delay = 3)
        {
            using var client = new HttpClient();
            for (int i = 0; i <= maxTries; i++)
            {
                try
                {
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"API is ready !!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //Log the exception here
                }
                Thread.Sleep(delay * 1000);
            }
            Console.WriteLine($"{url} API can't be hit after {maxTries} Tries!");
        }
    }
}
