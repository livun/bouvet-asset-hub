using Bouvet.AssetHub.Domain.Models;
using LanguageExt;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        
        Task<Option<LoanEntity>> Get(Expression<Func<LoanEntity, bool>> predicate);
        Task<Option<List<LoanEntity>>> GetAll();
        Task<Option<List<LoanEntity>>> GetByEmployeeNumber(int id);
        Task<Option<LoanEntity>> Add(LoanEntity entity);
        Task<Option<LoanEntity>> Update(LoanEntity entity);
        Task<Option<LoanEntity>> Delete(int id);
    }
}
