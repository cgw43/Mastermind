using System;
using System.Text;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SecretCode sc = new SecretCode();
            Console.WriteLine(sc.toString());
            Console.WriteLine("Ready to play Mastermind? You have 12 guesses to win");
            if (getUserInput(sc)) {
                Console.WriteLine("\nYou solved it!\n");
            } else {
                Console.WriteLine("\nYou lose :(\n");
            }
            Environment.Exit(0);
        }

        static bool getUserInput(SecretCode sc) {

            int numGuesses = 1;
            do {
                Console.WriteLine("Enter Guess #{0}", numGuesses);
                string guess = Console.ReadLine();
                if (guess != null && guess.Length == 4) {
                    numGuesses++;
                    string res = sc.guessResult(guess);
                    if (checkForWin(res)) { return true; }
                    Console.WriteLine("\n{0}\n", res);
                } else {
                    Console.WriteLine("Invalid Guess... try again");
                }
            } while (numGuesses <= 12);
            return false;
        }

        static bool checkForWin(string guessResult) {
            return guessResult == "++++";
        }
    }
    public class SecretCode {

        private int[] code;

        public SecretCode() {
            Random rand = new Random();
            int[] temp = {rand.Next(1,7), rand.Next(1,7), rand.Next(1,7), rand.Next(1,7)};
            code = temp;
        }

        public string toString() {
            return "" + code[0] + code[1] + code[2] + code[3];
        }

        public string guessResult(string guess) {
            StringBuilder builder = new StringBuilder();
            int[] checkedDigits = new int[6]; 
            int[] guessArray = {(int)char.GetNumericValue(guess[0]), (int)char.GetNumericValue(guess[1]), (int)char.GetNumericValue(guess[2]), (int)char.GetNumericValue(guess[3])};
            
            for (int i = 0; i < code.Length; i++) {
                if (code[i] == guessArray[i]) {
                    builder.Insert(0, "+");
                } else if (code.Contains(guessArray[i]) && !(checkedDigits[guessArray[i]-1] == guessArray[i])) {
                    
                    checkedDigits[guessArray[i]-1] = guessArray[i];
                    builder.Append("-");
                    
                }
            }
            return builder.ToString();
        }
    }
}