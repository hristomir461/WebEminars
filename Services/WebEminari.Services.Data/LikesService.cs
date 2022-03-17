using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public class LikesService : ILikesService
    {
        private readonly IDeletableEntityRepository<Like> likesRepository;

        public LikesService(IDeletableEntityRepository<Like> likesRepository)
            => this.likesRepository = likesRepository;
        public int GetCount()
        => this.likesRepository
            .All()
            .Count();
        public int GetCountByLessonId(int webEminarId)
            => this.likesRepository
                .All()
                .Count(x => x.WebEminarId == webEminarId);

        public async Task ToggleAsync(int webEminarId, string userId)
        {
            var like = this.likesRepository
                .All()
                .FirstOrDefault(x => x.WebEminarId == webEminarId && x.UserId == userId);

            if (like == null)
            {
                await this.AddAsync(webEminarId, userId);
            }
            else
            {
                await this.RemoveAsync(like);
            }
        }

        public async Task AddAsync(int webEminarId, string userId)
        {
            var like = new Like
            {
                WebEminarId = webEminarId,
                UserId = userId,
            };

            await this.likesRepository.AddAsync(like);
            await this.likesRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(Like like)
        {
            this.likesRepository.HardDelete(like);
            await this.likesRepository.SaveChangesAsync();
        }
    }
}
