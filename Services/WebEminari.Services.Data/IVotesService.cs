using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebEminari.Services.Data
{
    public interface IVotesService
    {
        Task SetVoteAsync(int webEminarId, string userId, byte value);

        double GetAverageVotes(int webEminarId);
    }
}
