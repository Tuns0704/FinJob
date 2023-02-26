using finjob_backend.Models;
using System.Linq.Expressions;

namespace finjob_backend.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> UpdateAsync(Company entity);
    }
}
