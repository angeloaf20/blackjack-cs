using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Game
    {
        //Adds cards to hand, removes them from the game deck
        public static List<string> AddToHand(List<string> GameDeck, List<string> Hand)
        {
            Hand.Add(GameDeck[0]);
            GameDeck.Remove(GameDeck[0]);
            return Hand;
        }
        public static int GetSum(List<string> Hand)
        {
            int sum = 0;
            foreach (string card in Hand)
            {
                string[] parts = card.Split(' ');
                string rank = parts[0];
                int num = 0;
                if (int.TryParse(rank, out num))
                {
                    sum += int.Parse(rank);
                }
                else
                {
                    if(rank == "ACE")
                    {
                        if(sum < 11)
                        {
                            sum += 11;
                        }
                        else
                        {
                            sum += 1;
                        }
                    }
                    else
                    {
                        sum += 10;
                    }
                }
            }
            
            return sum;
        }
        public static void Menu(List<string> PlayerHand, List<string> DealerHand, int PlayerSum)
        {
            Console.Write("Your cards are: ");
            foreach(string card in PlayerHand)
            {
                Console.Write(card + " ");
            }
            Console.Write($"SUM: {PlayerSum} \n");
            Console.WriteLine($"The dealer's first card: {DealerHand[0]}");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1) Hit");
            Console.WriteLine("2) Stand");
        }
        public static int HitPlayerHand(List<string> GameDeck, List<string> PlayerHand, List<string> DealerHand)
        {
            int PlayerSum;
            do
            {
                PlayerSum = GetSum(PlayerHand);
                if (PlayerSum > 21)
                {
                    break;
                }
                Menu(PlayerHand, DealerHand, PlayerSum);
                int choice = Convert.ToInt32(Console.ReadLine());
                while (choice != 1 && choice != 2)
                {
                    Console.Write("Invalid selection. Please choose 1 or 2. ");
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                if (choice == 1)
                {
                    PlayerHand = AddToHand(GameDeck, PlayerHand);
                }
                else
                {
                    break;
                }
            } while (PlayerSum < 21);
            if (PlayerSum == 21)
            {
                Console.WriteLine("You got to 21! Nice!");
            }
            else if (PlayerSum > 21)
            {
                Console.WriteLine($"You went over 21! Your sum is {PlayerSum}");
            }
            return PlayerSum;
        }
        public static int HitDealerHand(List<string> GameDeck, List<string> DealerHand, int PlayerSum)
        {
            int DealerSum = GetSum(DealerHand);
            int DStoWin = 21 - DealerSum;
            int PStoWin = 21 - PlayerSum;
            while (DealerSum < 21)
            {
                if(PlayerSum > 21 || (DStoWin < PStoWin))
                {
                    break;
                }
                DealerHand = AddToHand(GameDeck, DealerHand);
                DealerSum = GetSum(DealerHand);
                DStoWin = 21 - DealerSum;
            }
            return DealerSum;
        }
        public static string CompareSums(int PlayerSum, int DealerSum)
        {
            int DStoWin = 21 - DealerSum;
            int PStoWin = 21 - PlayerSum;
            if ((DealerSum == PlayerSum) || (PlayerSum > 21 && DealerSum > 21))
            {
                return "Tie!";
            }
            else if(PlayerSum == 21 && DealerSum != 21)
            {
                return "Dealer wins!";
            }
            else if (PlayerSum != 21 && DealerSum == 21)
            {
                return "Dealer wins!";
            }
            else if ((21 > PlayerSum && 21 > DealerSum) && (DStoWin > PStoWin))
            {
                return "Player wins!";
            }
            else if ((21 > PlayerSum && 21 > DealerSum) && (DStoWin < PStoWin))
            {
                return "Dealer wins!";
            }
            else if (PlayerSum > 21 && DealerSum < 21)
            {
                return "Dealer wins!";
            }
            else if (PlayerSum < 21 && DealerSum > 21)
            {
                return "Player wins!";
            }
            return "Tie!";
        }
        public static void EndMessages(int balance)
        {
            if (balance > 500)
            {
                Console.WriteLine($"Thanks for playing! You came out with ${balance}. Nice job!");
            }
            else if (balance == 500)
            {
                Console.WriteLine("You either didn't bet anything or broke even. Either way, play again!");
            }
            else if (balance > 0 && balance < 500)
            {
                Console.WriteLine($"Sucks! You came in with $500, and now you have ${balance}. You lost ${500 - balance}. Sorry!");
            }
            else
            {
                Console.WriteLine("You lost all your money. Try to take it easy with your money.");
            }
        }
        public static void GameLogic()
        {
            string PlayAgain = "y";
            int balance = 500;
            while (PlayAgain.ToLower() == "y")
            {
                Console.WriteLine($"Your balance is: ${balance}.");
                Console.Write("How much do you want to bet? $");
                int bet = int.Parse(Console.ReadLine());
                while (bet > balance)
                {
                    Console.Write("You cannot bet more than you have! Make a lower bet. $");
                    bet = int.Parse(Console.ReadLine());
                }

                Deck deck = new Deck();
                List<string> GameDeck = deck.ShuffleDeck();
                List<string> PlayerHand = new List<string>();
                List<string> DealerHand = new List<string>();

                AddToHand(GameDeck, DealerHand);
                AddToHand(GameDeck, DealerHand);
                AddToHand(GameDeck, PlayerHand);
                AddToHand(GameDeck, PlayerHand);

                int PlayerSum = HitPlayerHand(GameDeck, PlayerHand, DealerHand);
                int DealerSum = HitDealerHand(GameDeck, DealerHand, PlayerSum);
                foreach (string card in DealerHand)
                {
                    Console.Write(card + " ");
                }
                Console.Write($"SUM: {DealerSum} \n");

                Console.WriteLine(CompareSums(PlayerSum, DealerSum));
                if (CompareSums(PlayerSum, DealerSum) == "Player wins!")
                {
                    balance += bet;
                    Console.WriteLine($"Your winnings are ${balance}.");
                }
                else if (CompareSums(PlayerSum, DealerSum) == "Dealer wins!")
                {
                    balance -= bet;
                    Console.WriteLine($"You lost ${bet} dollars. Sucks!");
                }
                if (balance == 0)
                {
                    break;
                }
                Console.Write("Do you want to play again? Y/N. ");
                PlayAgain = Console.ReadLine();
                while (PlayAgain.ToLower() != "y" && PlayAgain.ToLower() != "n")
                {
                    Console.Write("Invalid selection. Choose either y or n. ");
                    PlayAgain = Console.ReadLine();
                }
            }
            EndMessages(balance);
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("[BLACKJACK]");
            GameLogic();
        }
    }
}
