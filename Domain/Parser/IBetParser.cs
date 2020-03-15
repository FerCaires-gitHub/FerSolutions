using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Parser
{
    public interface IBetParser
    {
        IDictionary<string,IEnumerable<OddModel>> GetMatchOdds();
        IEnumerable<OddModel> GetDoubleChance();

        IEnumerable<EventModel> GetEvents();

        IEnumerable<OddModel> GetOdds();
            
        
    }
}
