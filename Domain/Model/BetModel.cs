using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class BetModel
    {
        public double? OddHome { get; set; }
        public double? OddAway { get; set; }
        public double? StakeHome { get; set; }
        public double? StakeAway { get; set; }
        public double? ProfitHome { get; set; }
        public double? ProfitAway { get; set; }
        public double? Diference { get; set; }
        public double? Relatioship { get; set; }

        public override string ToString()
        {
            return $"Odds = > Home:{this.OddHome} | Away:{this.OddAway} \nStake => Home:{StakeHome} | Away:{StakeAway} \nRelationship: {this.Relatioship*100}%";
        }
    }
}
