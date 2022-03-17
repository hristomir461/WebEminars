using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        public bool Contains(int id) => this.webEminarRepository.All().Any(x => x.Id == id);
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

        public async Task CreateWithVideoAsync(WebEminarWithVideoViewModel input, string userId)
        {
            var webEminar = new WebEminar()
            {
                Title = input.Title,
                Video = input.Video,
                CategoryId = input.CategoryId,
                Description = input.Description,
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
        public T GetByIdAdmin<T>(int id)
        {
            var webEminar = this.webEminarRepository
                .AllWithDeleted()
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
        public async Task HardDeleteAsync(int id)
        {
            var webEminar = this.webEminarRepository
                .All()
                .FirstOrDefault(r => r.Id == id);

            this.webEminarRepository
                .HardDelete(webEminar);

            await this.webEminarRepository
                .SaveChangesAsync();
        }

        public IEnumerable<T> GetAllWebinars<T>()
        {
            var webinars = this.webEminarRepository
                .AllAsNoTracking()
                .To<T>()
                .ToArray();
            return webinars;
        }

        public IEnumerable<T> GetAll<T>(string stateFilter, int page, int itemsPerPage, string searchString, int categoryName)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(s => s.Title!.Contains(searchString)).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToArray();
                return webEminarsSearch;
            }
            if (categoryName != null && categoryName != 0)
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(s => s.CategoryId == categoryName).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToArray();
                return webEminarsSearch;
            }
            if (stateFilter == "Izminal")
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(s => s.ImageName != null && s.DateTime <= DateTime.UtcNow).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToArray();
                return webEminarsSearch;
            }
            if (stateFilter == "Kachen")
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(s => s.ImageName == null).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
                return webEminarsSearch;
            }
            if (stateFilter == "Available")
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(s => s.ImageName != null && s.DateTime >= DateTime.UtcNow).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToArray();
                return webEminarsSearch;
            }
            var webEminars = this.webEminarRepository
               .AllAsNoTracking()
               .OrderByDescending(r => r.Id)
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .To<T>()
               .ToArray();

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
                webEminar.Video = input.Video;


            if (!string.IsNullOrEmpty(ImageName))
            {
                webEminar.ImageName = ImageName;
            }

            await this.webEminarRepository.SaveChangesAsync();
        }
        public async Task UpdateAdminAsync(int id, WebEminarViewModel input, string ImageName)
        {
            var webEminar = this.webEminarRepository
                .AllWithDeleted()
                .FirstOrDefault(r => r.Id == id);

            webEminar.Title = input.Title;
            webEminar.CategoryId = input.CategoryId;
            webEminar.Description = input.Description;
            webEminar.MaxPeople = input.MaxPeople;
            webEminar.DateTime = input.DateTime;
            webEminar.MeetLink = input.MeetLink;
            webEminar.Video = input.Video;
            webEminar.IsDeleted = input.IsDeleted;


            if (!string.IsNullOrEmpty(ImageName))
            {
                webEminar.ImageName = ImageName;
            }

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

        public async Task BookEvent(int eventId, ApplicationUser user)
        {
            WebEminar eventFd = this.webEminarRepository.All().FirstOrDefault(x => x.Id == eventId);

            if (eventFd == null)
            {
                return;
                throw new ApplicationException("EventNotFound");
            }
            if (eventFd.MaxPeople < 0)
            {
                return;
                throw new ApplicationException("Not enough room");
            }
            eventFd.MaxPeople--;
            eventFd.UserBookings.Add(new UserBooking(user.Id, eventId, user.FirstName, user.LastName, user.Email));
            await this.webEminarRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetLikedByUserId<T>(string userId, string stateFilter, int page, int itemsPerPage, string searchString, int categoryName)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(x => x.Likes.Any(like => like.UserId == userId)).Where(s => s.Title!.Contains(searchString)).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
                return webEminarsSearch;
            }
            if (categoryName != null && categoryName != 0)
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(x => x.Likes.Any(like => like.UserId == userId)).Where(s => s.CategoryId == categoryName).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
                return webEminarsSearch;
            }
            if (stateFilter == "Izminal")
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(x => x.Likes.Any(like => like.UserId == userId)).Where(s => s.ImageName != null && s.DateTime <= DateTime.UtcNow).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
                return webEminarsSearch;
            }
            if (stateFilter == "Kachen")
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(x => x.Likes.Any(like => like.UserId == userId)).Where(s => s.ImageName == null).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
                return webEminarsSearch;
            }
            if (stateFilter == "Available")
            {
                var webEminarsSearch = this.webEminarRepository.AllAsNoTracking().Where(x => x.Likes.Any(like => like.UserId == userId)).Where(s => s.ImageName != null && s.DateTime >= DateTime.UtcNow).OrderByDescending(r => r.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
                return webEminarsSearch;
            }

            var allWithRelated = this.webEminarRepository
               .AllAsNoTracking()
               .Where(x => x.Likes.Any(like => like.UserId == userId))
               .OrderByDescending(r => r.Id)
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .To<T>()
               .ToList();

            return allWithRelated;
        }

        public async Task UpdateWithVideoAsync(int id, WebEminarWithVideoViewModel input)
        {
            var webEminar = this.webEminarRepository
             .All()
             .FirstOrDefault(r => r.Id == id);

            webEminar.Title = input.Title;
            webEminar.CategoryId = input.CategoryId;
            webEminar.Description = input.Description;
            webEminar.Video = input.Video;


            await this.webEminarRepository.SaveChangesAsync();
        }
        public async Task IzminalToKachen(int id, WebEminarWithVideoViewModel input)
        {
            var webEminar = this.webEminarRepository
             .All()
             .FirstOrDefault(r => r.Id == id);

            webEminar.Title = input.Title;
            webEminar.CategoryId = input.CategoryId;
            webEminar.Description = input.Description;
            webEminar.Video = input.Video;
            webEminar.Image = null;
            webEminar.ImageName = null;
            webEminar.MeetLink = null;

            await this.webEminarRepository.SaveChangesAsync();
        }
    }
}
