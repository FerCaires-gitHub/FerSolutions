using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dictionary
{
    public static class BetDictionaries
    {
        public static Dictionary<double, List<double>> OddPairs = new Dictionary<double, List<double>>();
        public static Dictionary<string, IEnumerable<BetModel>> BetPairs = new Dictionary<string, IEnumerable<BetModel>>();
        public static Dictionary<string, double?> BetRelationship = new Dictionary<string, double?>();
        public static Dictionary<string, string> EventPairs = new Dictionary<string, string>()
        {
            {"1","2X"},
            {"X","12"},
            {"2","1X"}
        };


    }
}
