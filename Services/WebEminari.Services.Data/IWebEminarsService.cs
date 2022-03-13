using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using WebEminari.Data.Models;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Services.Data
{
    public interface IWebEminarsService
    {
        public bool Contains(int id);
        Task CreateAsync(WebEminarViewModel input, string userId, string ImageName);

        Task CreateWithVideoAsync(WebEminarWithVideoViewModel input, string userId);

        T GetById<T>(int id);

        T GetByIdAdmin<T>(int id);

        IEnumerable<T> GetAll<T>(string stateFilter, int page, int itemsPerPage, string searchString, int categoryId);

        IEnumerable<T> GetAllWebinars<T>();

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, WebEminarViewModel input, string ImageName);

        IEnumerable<T> GetLikedByUserId<T>(string userId, string stateFilter, int page, int itemsPerPage, string searchString, int categoryName);

        Task UpdateAdminAsync(int id, WebEminarViewModel input, string ImageName);

        int GetCount();

        Task DeleteAsync(int id);
        Task BookEvent(int eventId, ApplicationUser user);
        Task UpdateWithVideoAsync(int id, WebEminarWithVideoViewModel input);

        Task IzminalToKachen(int id, WebEminarWithVideoViewModel input);
    }
}
