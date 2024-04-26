using Frontend.Models;
using Frontend.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;

namespace Frontend.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient client;

        public EmployeeService()
        {
            client = new HttpClient { BaseAddress = new Uri("https://localhost:7201/") };
        }

        public async Task<HttpStatusCode> Add(Employee employee)
        {
            var contentJson = JsonSerializer.Serialize(employee);
            var requestContent = new StringContent(contentJson, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("api/employee/create",requestContent);

            
            if (res.StatusCode == HttpStatusCode.Created) { return HttpStatusCode.Created; }
            else 
            {
                Console.WriteLine(res.Content.ReadAsStringAsync().Result);
                throw new Exception($"{((int)res.StatusCode)} {res.StatusCode}");
            }
        }
        public async Task<HttpStatusCode> Delete(int ID)
        {
            HttpResponseMessage res = await client.DeleteAsync($"api/employee/delete/{ID}");


            if (res.StatusCode == HttpStatusCode.NoContent) { return HttpStatusCode.NoContent; }
            else
            {
                Console.WriteLine(res.Content.ReadAsStringAsync().Result);
                throw new Exception($"{((int)res.StatusCode)} {res.StatusCode}");
            }
        }

        public async Task<Employee> GetById(int ID)
        {
            HttpResponseMessage res = await client.GetAsync($"api/employee/{ID}");
            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Employee>(content);
            }
            else
            {
                Console.WriteLine(res.Content.ReadAsStringAsync().Result);
                throw new Exception($"{((int)res.StatusCode)} {res.StatusCode}");
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            HttpResponseMessage res = await client.GetAsync("api/employee");
            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Employee>>(content);
            }
            else
            {
                Console.WriteLine(res.Content.ReadAsStringAsync().Result);
                throw new Exception($"{((int)res.StatusCode)} {res.StatusCode}");
            }
        }

        public async Task<HttpStatusCode> Update(EmployeeUpdate employee)
        {
            var contentJson = JsonSerializer.Serialize(employee);
            var requestContent = new StringContent(contentJson, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PutAsync("api/employee/update", requestContent);


            if (res.StatusCode == HttpStatusCode.NoContent) { return HttpStatusCode.NoContent; }
            else
            {
                Console.WriteLine(res.Content.ReadAsStringAsync().Result);
                throw new Exception($"{((int)res.StatusCode)} {res.StatusCode}");
            }
        }
    }
}
