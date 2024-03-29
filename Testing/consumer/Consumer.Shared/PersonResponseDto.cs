namespace Consumer.Shared
{
    public class PersonResponseDto
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public static class ApiCaller
    {
        public static async Task<HttpResponseMessage> Call(string url, string id)
        {
            using (var client = new HttpClient() { BaseAddress = new Uri(url) })
            {
                try
                {
                    var response = await client.GetAsync($"/api/people?id={id}");
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem connecting to provider API.", ex);
                    throw;
                }
            }
        }
    }
}
