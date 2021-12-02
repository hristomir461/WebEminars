using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categoriesRepository
                .All()
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                })
                .ToList()
                .OrderBy(a => a.Name)
                .Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name));
        }
    }
}
