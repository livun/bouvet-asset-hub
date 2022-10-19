using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Loan.Model;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.API.Helpers
{

    public static class LoanPredicates
    {
        public static Expression<Func<LoanEntity, bool>> ById(int id)
        {
            return (a => a.Id == id);
        }
        //public static Expression<Func<LoanEntity, bool>> BySerialNumber(int serialNumber)
        //{
        //    return (a => a.SerialNumber.Value == serialNumber);
        //}
    }

}
