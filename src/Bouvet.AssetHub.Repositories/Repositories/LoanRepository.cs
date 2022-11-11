using Bouvet.AssetHub.Data;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using EntityFramework.Exceptions.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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
                loan.Borrower = newEmployee;
            } else
            {
                loan.Borrower = employee;
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

        public async Task<Option<LoanEntity>> Update(LoanEntity loan)
        {
            var entity = await _context.Loans
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Borrower)
                .Where(a => a.Id == loan.Id)
                .FirstOrDefaultAsync();
                            
            if (entity is not null)
            {
                entity.Interval.Stop = loan.Interval.Stop;
                await _context.SaveChangesAsync();
                return entity;
            }
            return Option<LoanEntity>.None;

        }

        public async Task<Option<List<LoanEntity>>> GetAll()
        {
            var loans = await _context.Loans
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Borrower)
                .ToListAsync();
            return loans.Any() ? loans : null;
        }
        public async Task<Option<List<LoanEntity>>> GetByEmployeeNumber(int employeeNumber)        {
            var loans = await _context.Loans
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Borrower)
                .Where(a => a.AssignedTo.Value == employeeNumber)
                .ToListAsync();
            return loans.Any() ? loans : null;
        }
        public async Task<Option<LoanEntity>> Get(Expression<Func<LoanEntity, bool>> predicate)
        {
            return await _context.Loans
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Borrower)
                .AsQueryable()
                .Where(predicate)
                .FirstOrDefaultAsync();

        }

        public async Task<Option<LoanEntity>> Delete(int id)
        {
            var entity = await _context.Loans
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Borrower)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();
            if (entity is not null)
            {
                _context.Loans.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return Option<LoanEntity>.None;


        }
    }
}
