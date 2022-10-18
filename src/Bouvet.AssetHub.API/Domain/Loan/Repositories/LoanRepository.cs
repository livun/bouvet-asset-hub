using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using Bouvet.AssetHub.API.Domain.Asset.Repositories;
using Bouvet.AssetHub.API.Domain.Employee.Model;
using Bouvet.AssetHub.API.Domain.Loan.Interfaces;
using Bouvet.AssetHub.API.Domain.Loan.Model;
using EntityFramework.Exceptions.Common;
using LanguageExt;
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
        private readonly ILogger _logger;


        public LoanRepository(DataContext context, ILogger<LoanRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<Option<LoanEntity>> Add(LoanEntity loan)
        {
            // add employee to loan
            var employee = await _context.Employees
                .Where(e => e.EmployeeNumber.Value == loan.AssignedTo.Value)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                var newEmployee = new EmployeeEntity { EmployeeNumber = new EmployeeNumber { Value = loan.AssignedTo.Value } };
                _context.Employees.Add(newEmployee);
                loan.Loaner = newEmployee;
            } else
            {
                loan.Loaner = employee;
            }
            
            // add asset to loan
            var asset = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == loan.AssetId)
                .FirstOrDefaultAsync();
            if (asset == null || asset.Status == Status.Unavailable)
            {
                _logger.LogError($"Asset can not be lent out, status is {asset?.Status}");
                return Option<LoanEntity>.None;
            }
            asset.Status = Status.Unavailable;
            loan.Asset = asset;

            await _context.Loans.AddAsync(loan);
            try
            {
                await _context.SaveChangesAsync();
                return loan;

            }
            catch (UniqueConstraintException ex)
            {
                _logger.LogError(ex.Message);
                return Option<LoanEntity>.None;
            }
        }

        public void AddRange(IEnumerable<LoanEntity> entities)
        {
            _context.Loans.AddRange(entities);
        }

        public async Task<Option<List<LoanEntity>>> GetAll()
        {
            var loans = await _context.Loans
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Loaner)
                .ToListAsync();

            if (loans.Count == 0)
                return Option<List<LoanEntity>>.None;
            return loans;
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
