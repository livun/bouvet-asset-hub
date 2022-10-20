using Bouvet.AssetHub.API.Domain.Loan.Models;
using LanguageExt;

namespace Bouvet.AssetHub.API.Domain.Loan.Interfaces
{
    public interface ILoanHistoryRepository
    {
        Task<Option<List<LoanHistoryEntity>>> GetAll();
        Task<Option<LoanHistoryEntity>> Add(LoanHistoryEntity entity);

    }
}
