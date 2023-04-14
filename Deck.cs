using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Creates a deck of cards
    public class Deck
    {
        public List<string> deck; 
        public string[] ranks; 
        public string[] suits; 

        public Deck()
        {
            this.deck = new List<string>();
            ranks = new string[13];
            for(int i = 0; i <= 8; i++)
            {
                ranks[i] = (i + 2).ToString();
            }
            ranks[9] = "JOKER";
            ranks[10] = "QUEEN";
            ranks[11] = "KING";
            ranks[12] = "ACE";
            suits = new string[4] { "DIAMONDS", "HEARTS", "CLUBS", "SPADES" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    Card card = new Card(rank, suit);
                    this.deck.Add((card.GetCard()));
                }
            }
        }
        public List<string> ShuffleDeck()
        {
            Random r = new Random();
            int randomIndex;
            for (var i = 0; i < this.deck.Count; i++)
            { 
                randomIndex = r.Next(0, this.deck.Count);
                string temp = this.deck[i];
                this.deck[i] = this.deck[randomIndex];
                this.deck[randomIndex] = temp;
            }
            return this.deck;
        }
        public List<string> GetDeck()
        {
            return this.deck;
        }
    }
}
