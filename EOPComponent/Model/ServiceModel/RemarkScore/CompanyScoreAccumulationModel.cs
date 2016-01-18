using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOPComponent.Model.ScoreAccumulation
{
    public class CompanyScoreAccumulationModel : CompanyScoreOperationModel
    {
        public int LastEggCount { get; set; }
        public int LastFlowerCount { get; set; }
        public int CurrentEggCount { get; set; }
        public int CurrentFlowerCount { get; set; }
        public int CreateScoreCount { get; set; }

        protected static int eggScoreABS { get; set; }
        protected static int flowerScoreABS { get; set; }

        public int GetScoreAccumulation(int eggScore, int flowerScore)
        {
            if (this.OperationScore == 0)
            {
                eggScoreABS = Math.Abs(eggScore);
                flowerScoreABS = Math.Abs(flowerScore);

                int lastEggScore = (-1) * eggScoreABS * this.LastEggCount;
                int lastFlowerScore = flowerScoreABS * this.LastFlowerCount;
                this.LastScore = (-1) * (lastEggScore + lastFlowerScore);

                int currentEggScore = (-1) * eggScoreABS * this.CurrentEggCount;
                int currentFlowerScore = flowerScoreABS * this.CurrentFlowerCount;
                this.CurrentScore = (currentEggScore + currentFlowerScore);

                this.OperationScore = (this.LastScore + this.CurrentScore);
            }
            return this.OperationScore;
        }
    }
}
