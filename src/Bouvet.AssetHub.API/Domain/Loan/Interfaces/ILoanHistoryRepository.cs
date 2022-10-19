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
    public interface ILoanHistoryRepository
    {
        Task<Option<List<LoanHistoryEntity>>> GetAll();
        Task<Option<LoanHistoryEntity>> Add(LoanHistoryEntity entity);

    }
}
