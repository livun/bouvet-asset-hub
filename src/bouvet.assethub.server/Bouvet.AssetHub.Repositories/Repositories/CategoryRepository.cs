using Bouvet.AssetHub.Data;
using Bouvet.AssetHub.Domain.Models;
using Bouvet.AssetHub.Repositories.Interfaces;
using EntityFramework.Exceptions.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bouvet.AssetHub.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public CategoryRepository(ILogger<AssetRepository> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Option<CategoryEntity>> Add(CategoryEntity category)
        {
            await _context.Categories.AddAsync(category);
            try
            {
                await _context.SaveChangesAsync();
                return category;
            }
            catch (UniqueConstraintException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
   
        public async Task<Option<CategoryEntity>> Delete(int id)
        {
            var assets = await _context.Assets
            .Include(a => a.Category)
            .Where(a => a.Category.Id == id)
            .ToListAsync();
            if (assets.Any())
            {
                return null;
            }
            var category = await _context.Categories
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (category is not null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return category;
            }        
            return null;
        }

        public async Task<Option<List<CategoryEntity>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Any() ? categories : null;
        }
        public async Task<Option<CategoryEntity>> Get(int id)
        {
            return await _context.Categories
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Option<CategoryEntity>> Update(CategoryEntity entity)
        {
            var assets = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Category.Id == entity.Id)
                .ToListAsync();
            if (assets.Any())
            {
                return null;
            }
            var category = _context.Categories
                .Where(a => a.Id == entity.Id)
                .FirstOrDefault();
            if (category is not null)
            {
                category.Name = entity.Name;
                await _context.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}
