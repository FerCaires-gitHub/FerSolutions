using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public interface IBetService : IDisposable
    {
        IEnumerable<BetModel> GetBet(double OddHome, double OddAway);

        void GetAll();

        void WriteFile(IDictionary<string, IEnumerable<BetModel>> pairs);

    }
}
