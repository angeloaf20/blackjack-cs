using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Defines what a card object is
    public class Card
    {
        public string suit;
        public string rank;
        public Card(string SuitName, string RankName)
        {
            this.suit = SuitName;
            this.rank = RankName;
        }

        public string GetCard()
        {
            return $"{this.suit} of {this.rank}";
        }
    }
}
