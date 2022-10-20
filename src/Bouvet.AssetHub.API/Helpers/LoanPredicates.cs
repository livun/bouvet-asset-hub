using Bouvet.AssetHub.API.Domain.Loan.Models;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.API.Helpers
{

    public static class LoanPredicates
    {
        public static Expression<Func<LoanEntity, bool>> ById(int id)
        {
            return (a => a.Id == id);
        }
        public static Expression<Func<LoanEntity, bool>> ByAssetId(int id)
        {
            return (a => a.AssetId == id);
        }
     
    }

}
