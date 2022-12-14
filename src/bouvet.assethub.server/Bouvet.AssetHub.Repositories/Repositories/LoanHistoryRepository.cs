using Bouvet.AssetHub.Data;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using EntityFramework.Exceptions.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bouvet.AssetHub.Repositories
{
    public class LoanHistoryRepository : ILoanHistoryRepository

    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public LoanHistoryRepository(DataContext context, ILogger<LoanHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Option<LoanHistoryEntity>> Add(LoanHistoryEntity loan)
        {
            // add employee to loanhistory
            var employee = await _context.Employees
                .Where(e => e.Id == loan.Borrower.Id)
                .FirstOrDefaultAsync();
            if (employee is not null ) 
                loan.Borrower = employee;
            
            // add asset to loanhistory
            var asset = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == loan.Asset.Id)
                .FirstOrDefaultAsync();
           if (asset is not null )
                loan.Asset = asset;

            loan.Id = 0;

            await _context.LoanHistory.AddAsync(loan);
            try
            {
                await _context.SaveChangesAsync();
                return loan;
            }
            catch (UniqueConstraintException ex)
            {
                _logger.LogError(ex.Message);
                return Option<LoanHistoryEntity>.None;
            }
        }
        public async Task<Option<List<LoanHistoryEntity>>> GetAll()
        {
            var loans = await _context.LoanHistory
                .Include(l => l.Asset).ThenInclude(a => a.Category)
                .Include(l => l.Borrower)
                .ToListAsync();
            return loans.Any() ? loans : null;
        }
    }
}