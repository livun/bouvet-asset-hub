using Bouvet.AssetHub.Domain.Models;
using LanguageExt;

namespace Bouvet.AssetHub.Repositories.Interfaces
{
    public interface ILoanHistoryRepository
    {
        Task<Option<List<LoanHistoryEntity>>> GetAll();
        Task<Option<LoanHistoryEntity>> Add(LoanHistoryEntity entity);
    }
}