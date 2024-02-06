using DapperMVC_Demo.Data.Models.Domain;

namespace DapperMvc_Demo.Data.Repository
{
    public interface IPersonReposository
    {
        Task<bool> AddAsync(Person person);
        Task<bool> UpdateAsync(Person person);

        Task<bool> DeleteAsync(int id);

       Task<Person?> GetByIdAsync(int id);

        Task<IEnumerable<Person>> GetAllAsync();     
    }
}