using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Loan.Model;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Loan.Interfaces
{
    public interface ILoanRepository
    {
        
        Task<Option<LoanEntity>> Get(Expression<Func<LoanEntity, bool>> predicate);
        Task<Option<List<LoanEntity>>> GetAll();
        Task<Option<LoanEntity>> Add(LoanEntity entity);
        Task<Option<LoanEntity>> Update(LoanEntity entity);
        Task<Option<LoanEntity>> Delete(int id);
    }
}
