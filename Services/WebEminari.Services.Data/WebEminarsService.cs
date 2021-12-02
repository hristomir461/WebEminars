using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;
using WebEminari.Services.Mapping;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Services.Data
{
    public class WebEminarsService : IWebEminarsService
    {
        private readonly IDeletableEntityRepository<WebEminar> webEminarRepository;

        public WebEminarsService(IDeletableEntityRepository<WebEminar> webEminarRepository)
        {
            this.webEminarRepository = webEminarRepository;
        }

        public async Task CreateAsync(WebEminarViewModel input, string userId, string ImageName)
        {
            var webEminar = new WebEminar()
            {
                Title = input.Title,
                ImageName = ImageName,
                MaxPeople = input.MaxPeople,
                CategoryId = input.CategoryId,
                Description = input.Description,
                MeetLink = input.MeetLink,
                DateTime = input.DateTime,
                AddedByUserId = userId,
            };

            await this.webEminarRepository.AddAsync(webEminar);
            await this.webEminarRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var webEminar = this.webEminarRepository
                .AllAsNoTracking()
                .Where(r => r.Id == id)
                .To<T>()
                .FirstOrDefault();

            return webEminar;
        }

        public async Task DeleteAsync(int id)
        {
            var webEminar = this.webEminarRepository
                .All()
                .FirstOrDefault(r => r.Id == id);

            this.webEminarRepository
                .Delete(webEminar);

            await this.webEminarRepository
                .SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var webEminars = this.webEminarRepository
               .AllAsNoTracking()
               .OrderByDescending(r => r.Id)
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .To<T>()
               .ToList();

            return webEminars;
        }

        public int GetCount()
        {
            return this.webEminarRepository
                .All()
                .Count();
        }

        public async Task UpdateAsync(int id, WebEminarViewModel input, string ImageName)
        {
            var webEminar = this.webEminarRepository
                .All()
                .FirstOrDefault(r => r.Id == id);

            webEminar.Title = input.Title;
            webEminar.CategoryId = input.CategoryId;
            webEminar.Description = input.Description;
            webEminar.MaxPeople = input.MaxPeople;
            webEminar.DateTime = input.DateTime;
            webEminar.MeetLink = input.MeetLink;
            webEminar.ImageName = ImageName;

            await this.webEminarRepository.SaveChangesAsync();
        }


        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.webEminarRepository
                .All()
                .OrderBy(r => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }

     
    }
}
