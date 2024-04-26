using Frontend.Models;
using System.Net;

namespace Frontend.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployees();
        public Task<HttpStatusCode> Add(Employee employee);
        public Task<HttpStatusCode> Update(EmployeeUpdate employee);
        public Task<HttpStatusCode> Delete(int ID);
        public Task <Employee> GetById(int ID);
    }
}
