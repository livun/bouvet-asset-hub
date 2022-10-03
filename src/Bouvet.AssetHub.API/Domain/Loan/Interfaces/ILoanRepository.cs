using Bouvet.AssetHub.API.Domain.Loan.Model;
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
        LoanEntity GetById(int id);
        IEnumerable<LoanEntity> GetAll();
        void Add(LoanEntity entity);
        //void AddRange(IEnumerable<AssetEntity> entities);
        void Remove(LoanEntity entity);
        void Save();
        //void RemoveRange(IEnumerable<AssetEntity> entities);

    }
}
