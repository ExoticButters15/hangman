using System;

namespace dle_Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            //hangman stages
            static string HangDudes(int death)
            {
                string[] dudes = { @"
             +---+
             |   |
                 |
                 |
                 |
                 |
            =========", @"
             +---+
             |   |
             O   |
                 |
                 |
                 |
            =========", @"
             +---+
             |   |
             O   |
             |   |
                 |
                 |
            =========", @"
             +---+
             |   |
             O   |
            /|   |
                 |
                 |
            =========", @"
             +---+
             |   |
             O   |
            /|\  |
                 |
                 |
            =========", @"
             +---+
             |   |
             O   |
            /|\  |
            /    |
                 |
            =========", @"
             +---+
             |   |
             O   |
            /|\  |
            / \  |
                 |
            =========" };

                return dudes[death];
            }

            //declaration
            string secretWord = " ";
            string guess = " ";
            int x = 0, length = 0, guessBuild = 0, guessAttempts = 0, guessAttemptsIncorrect = 0;
            char[] guesses = new char[10000];
            char guessChar;

            //intro
            Console.WriteLine("Welcome to hangman! Press [ENTER] to continue and enter your secret word. Then you can guess one letter at a time to make out the word. 7 mistakes and you'll kill the man!");
            Console.ReadLine();
            Console.Clear();

            //secret word
            Console.Write("What is the secret word: ");
            secretWord = Console.ReadLine();
            secretWord = secretWord.ToLower();

            //blanks print
            length = secretWord.Length;
            string[] wordPrint = new string[length];
            for (int i = 0; i < length; i++)
                wordPrint[i] = " _ ";

            //wrong guesses
            char[] letterBank = new char[7];

            //repeats prompt until length amount of errors
            while (x != 6)
            {
                Printing(x, length, wordPrint, letterBank, guessAttemptsIncorrect);

                //prompt
                Console.Write("What is your guess: ");
                guess = Console.ReadLine();

                //checks if guess was made already
                while (GuessCheck(guess, guesses, guessAttempts))
                {
                    //prompt again
                    Console.Write("Your guess was invalid, try again: ");
                    guess = Console.ReadLine();
                }
                guessChar = char.Parse(guess);
                guesses[guessAttempts] = guessChar;
                guessAttempts++;

                //checks for guess within the word
                if (secretWord.IndexOf(guessChar) != -1)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (secretWord[j] == guessChar)
                        {
                            wordPrint[j] = " " + guessChar + " ";
                            //letter was found in word
                            guessBuild++;
                        }
                    }
                    //indexes match the length of word
                    if (guessBuild == length)
                    {
                        Printing(x, length, wordPrint, letterBank, guessAttemptsIncorrect);
                        Console.WriteLine("You win!");
                        x = 6;
                    }
                }
                //incorrect guess
                else
                {
                    x++;
                    letterBank[guessAttemptsIncorrect] = guessChar;
                    guessAttemptsIncorrect++;
                    if (x == 6)
                    {
                        Printing(x, length, wordPrint, letterBank, guessAttemptsIncorrect);
                        Console.WriteLine("You lose :( The secret word was " + secretWord);
                    }
                }
            }
            Console.ReadLine();

            //prints hangman and blanks
            static void Printing(int x, int length, string[] wordPrint, char[] letterBank, int guessAttempts)
            {
                //prints hangman stage
                Console.Clear();
                Console.WriteLine(HangDudes(x));
                Console.WriteLine();

                //prints guesses
                for (int i = 0; i < length; i++)
                {
                    Console.Write(wordPrint[i]);
                }
                Console.WriteLine("\n");

                //prints previous guesses
                Console.WriteLine("Previous Guesses");
                for (int i = 0; i < guessAttempts; i++)
                    Console.Write(" " + letterBank[i] + " ");
                Console.WriteLine("\n");
            }

            //checks if guess was made already
            static bool GuessCheck(string guess, char[] guesses, int guessAttempts)
            {
                bool returnValue = false;
                char guessChar = ' ';

                if (guess.Length > 1)
                {
                    returnValue = true;
                }
                else
                    guessChar = char.Parse(guess);

                for (int i = 0; i < guessAttempts; i++)
                {
                    if (guessChar == guesses[i])
                    {
                        returnValue = true;
                    }
                }

                return returnValue;
            }
        }
    }
}
