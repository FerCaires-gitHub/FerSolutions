using Domain.Dictionary;
using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class BetService : IBetService
    {

        private double stake = 1.0;
        private Dictionary<string, IEnumerable<BetModel>> BetPairs = new Dictionary<string, IEnumerable<BetModel>>();


        private string CreateHash(double oddHome, double oddAway)
        {
            return $"{Math.Round(oddHome,3)}|{Math.Round(oddAway,3)}";
        }
        public IEnumerable<BetModel> GetBet(double OddHome, double OddAway)
        {
            var hash = CreateHash(OddHome, OddAway);
            if(BetDictionaries.BetPairs.ContainsKey(hash))
                return BetDictionaries.BetPairs[hash];
            else
                return default(IEnumerable<BetModel>);
        }

        public void GetAll()
        {
            GetAllCombinations();
            
        }

        private IEnumerable<BetModel> BetSimulation(double? OddHome, double? OddAway)
        {
            var bets = new List<BetModel>();
            for (double i = 0.0; i <= 1.0; i += 0.01)
            {
                var resultHome = OddHome * i;
                var resultAway = OddAway * (stake - i);
                if (resultHome >= stake && resultAway >= stake)
                    bets.Add(new BetModel()
                    {
                        OddHome = OddHome,
                        OddAway = OddAway,
                        StakeHome = i,
                        StakeAway = (stake - i),
                        ProfitHome = resultHome,
                        ProfitAway = resultAway,
                        Diference = resultHome - resultAway < 0 ? (resultHome - resultAway) * -1 : resultHome - resultAway,
                        Relatioship = (OddHome/OddAway)

                    });

            }
            return bets;
        }
        private void GetAllCombinations()
        {
            for (double i = 1.01; i < 7.0; i+=0.01)
            {
                for (double j = 1.01; j < 7.0; j += 0.01)
                {
                    var hash = $"{Math.Round(i, 3)}|{Math.Round(j, 3)}";
                    var bets = BetSimulation(Math.Round(i, 3), Math.Round(j, 3));
                    if (!BetDictionaries.BetPairs.ContainsKey(hash) && bets?.Count() > 0)
                        BetDictionaries.BetPairs.Add(hash, bets);
                }
            }
            
            
            
        }

        private void CacheOddsDictionary(double? oddHome, double? awayOdd,IEnumerable<BetModel> bets)
        {
            var hash = $"{oddHome}|{awayOdd}";
            if (!BetDictionaries.BetPairs.ContainsKey(hash) && bets.Count() > 0 )
                BetDictionaries.BetPairs.Add(hash, bets);
        }

        private void CacheDictionary(IEnumerable<BetModel> bets)
        {
            foreach (var item in bets)
            {
                var hash = $"{item.OddHome}|{item.OddAway}";
                if (!BetDictionaries.BetRelationship.ContainsKey(hash))
                    BetDictionaries.BetRelationship.Add(hash, item.Relatioship);
            }
            
        }

        public void WriteFile(IDictionary<string, IEnumerable<BetModel>> pairs)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bet.csv");
            Console.WriteLine(path);
            using (StreamWriter sw = new StreamWriter(path))
            {
                string header = string.Format("Hash;{0}", Utils.GetPropertiesNames(new BetModel()));
                sw.WriteLine(header);
                foreach (var item in pairs.Keys)
                {
                    foreach (var listItem in pairs[item])
                    {
                        sw.WriteLine($"{item};{Utils.GetPropertyValues(listItem)}");
                    }
                }
            }
        }

        

        public void Dispose()
        {

        }
    }
}
