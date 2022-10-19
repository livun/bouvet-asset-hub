using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using EntityFramework.Exceptions.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.API.Domain.Asset.Repositories
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
                return Option<CategoryEntity>.None;
            }
        }

   
        public async Task<Option<CategoryEntity>> Delete(int id)
        {
            var category = await _context.Categories
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (category is not null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return category;
            }
            return Option<CategoryEntity>.None;


        }

        public async Task<Option<List<CategoryEntity>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Any() ? categories : null;
        }

        public async Task<Option<CategoryEntity>> Update(CategoryEntity entity)
        {
            var category = _context.Categories
                .Where(a => a.Id == entity.Id)
                .FirstOrDefault();
            if (category is not null)
            {
                category.Name = entity.Name;
                await _context.SaveChangesAsync();
                return category;
            }
            return Option<CategoryEntity>.None;
        }
    }
}
