using System;
using System.Text;

namespace Mastermind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SecretCode sc = new SecretCode();
            Console.WriteLine("Ready to play Mastermind? You have 12 guesses to win");
            if (getUserInput(sc)) {
                Console.WriteLine("\nYou solved it!\n");
            } else {
                Console.WriteLine("\nYou lose :(\n");
            }
           Console.ReadKey();
        }

        /*
            Function that gets 12 valid user guesses and tracks progress
        */
        static bool getUserInput(SecretCode sc) {

            int numGuesses = 1;
            while (numGuesses <= 12) {
                Console.WriteLine("Enter Guess #{0}", numGuesses);
                string guess = Console.ReadLine();
                // minor input validations
                if (guess != null && guess.Length == 4) {
                    numGuesses++;
                    string res = sc.guessResult(guess);
                    if (checkForWin(res)) { return true; }
                    Console.WriteLine("\n{0}\n", res);
                } else {
                    Console.WriteLine("Invalid Guess... try again");
                }
            }
            return false;
        }

        /* Checks guess results to see if user won */
        static bool checkForWin(string guessResult) {
            return guessResult == "++++";
        }
    }

    /* Class to represent the secret code */
    public class SecretCode {

        private int[] code;

        /* Constructor randomly generates a secret code */
        public SecretCode() {
            Random rand = new Random();
            int[] temp = {rand.Next(1,7), rand.Next(1,7), rand.Next(1,7), rand.Next(1,7)};
            code = temp;
        }

        /* returns secret code as a string representation (used for testing) */
        public string toString() {
            return "" + code[0] + code[1] + code[2] + code[3];
        }

        /* Checks input guess against secret code and generates result string */
        public string guessResult(string guess) {
            StringBuilder builder = new StringBuilder();
            int[] guessArray = {(int)char.GetNumericValue(guess[0]), (int)char.GetNumericValue(guess[1]), (int)char.GetNumericValue(guess[2]), (int)char.GetNumericValue(guess[3])};
            int[] tempCode = (int[])code.Clone();

            for (int i = 0; i < code.Length; i++) {
                if (tempCode[i] == guessArray[i]) {
                    builder.Insert(0, "+");
                    // eliminating this guess and code digit from being checked again
                    guessArray[i] = 0;
                    tempCode[i] = -1; 
                } 
            }
            for (int i  = 0; i < code.Length; i++) {  
                if (tempCode.Contains(guessArray[i])) {
                    
                    int index = Array.IndexOf(tempCode, guessArray[i]);
                    tempCode[index] = -1; // Eliminating this digit from being matched with again
                    builder.Append("-");
                }
            }
            return builder.ToString();
        }
    }
}