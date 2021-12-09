using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task SetVoteAsync(int webEminarId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.WebEminarId == webEminarId && x.UserId == userId);
            if (vote == null)
            {
                 vote = new Vote()
                {
                    UserId = userId,
                    WebEminarId = webEminarId,
                    Value = value,
                };
                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }

        public double GetAverageVotes(int webEminarId)
        {
            var averageVotes = this.votesRepository
                .All()
                .Where(v => v.WebEminarId == webEminarId)
                .Average(v => v.Value);

            return averageVotes;
        }
    }
}
