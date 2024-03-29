using Consumer.Shared;
using Newtonsoft.Json;

namespace Consumer.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var url = "https://localhost:44348";
            Console.WriteLine("please send a person id: ");
            string id = Console.ReadLine();
            var result = ApiCaller.Call(url, id).Result.Content.ReadAsStringAsync().Result;
            PersonResponseDto dto = JsonConvert.DeserializeObject<PersonResponseDto>(result);
            Console.WriteLine($"First Name: {dto.Firstname}, Last Name: {dto.Lastname}");
            Console.ReadKey();
        }
    }
}
