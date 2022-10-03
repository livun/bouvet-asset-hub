using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Loan.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext _context;

        public LoanRepository(DataContext context) 
        {
            _context = context;
        }

        public void Add(LoanEntity entity)
        {
            _context.Loans.Add(entity);
        }

        public void AddRange(IEnumerable<LoanEntity> entities)
        {
            _context.Loans.AddRange(entities);
        }

        public IEnumerable<LoanEntity> GetAll()
        {
            return _context.Loans
                .Include(l => l.Asset)
                .Include(l => l.AssignedTo)
                .ToList();
        }

        public LoanEntity GetById(int id)
        {
            return _context.Loans
                .Include(l => l.Asset)
                .Include(l => l.AssignedTo)
                .Where(a => a.Id == id)
                .First();
        }

        public void Remove(LoanEntity entity)
        {
            _context.Loans.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
