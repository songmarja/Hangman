using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace Console_Hangman
{
    public  static class Services
    {
       
        public static void GetHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***********************************************************************************************************************");
            Console.WriteLine("\n                                                    Welcome to play Hangman!\n");
            Console.WriteLine("***********************************************************************************************************************");
            Console.ResetColor();

            string s = "Hangman game is about guessing a secret word. You have 10 guesses to find out!\n" +
                       "You can choose to make a guess on the whole word anytime you want,\n" +
                       "but if you have found some characters, it would be more easy.\n" +
                       "Good Luck!!";
            Console.WriteLine(s);
        }
        /// <summary>
        /// Gets an answer from user if hen wants to play. otherwise app turns down
        /// </summary>
        /// <returns></returns>
        public static bool ReadyToPlay()
        {
            Console.WriteLine();
            Console.Write("Are you ready to play? Y (Yes) /N (No): ");
            string? yesOrNo = null;
            bool notValid = true; 
            // Check if valid input. Output => 'y' or 'n'
            while(notValid)
            {
                yesOrNo = Console.ReadLine().ToLower();
                if (String.IsNullOrEmpty(yesOrNo) || (yesOrNo != "n" && yesOrNo != "y") )
                {
                    Console.Write("Please enter: y (Yes) / n (No): \n ");
                   
                }
                else
                {
                    notValid = false;
                }
            }
            // Wants to quit
            if(yesOrNo == "n")
            {
                Environment.Exit(0);
            }
            // wants to play
            return true;
        }


        /// <summary>
        ///  Creates a new random word from the string array, by selecting an index of the string array
        /// save the word in a char array, and returns it.
        /// </summary>
        /// <returns>string word</returns>
        public static char[] GetSecretRandomWordFromFile()
        {
            Random rnd = new Random();

            // string[] randomWords = { "solsting", "myggmedel", "sommarlov", "pollenallergi",
            //    "smultron","åskväder","semester", "brandrisk","vattentemperatur", "kanadensare", "badring", "glass", "bajamaja"};

            string[] randomWords = GetSecretWordsFromFile();
            int index = rnd.Next(0, randomWords.Length);
            char[] secretChars = randomWords[index].ToCharArray();

            return secretChars;
        }

        public static string[] GetSecretWordsFromFile()
        {
            string[] secretWords = new string[17];
            string? secretWordsString = null;
            string secretWordsFile = "../../../secretWords.txt";

            try
            {
                secretWordsString = File.ReadAllText(secretWordsFile);
               
                secretWords = secretWordsString.Split(',')
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error, when reading a file",e);
            }
            return secretWords;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string ReadUserInput(int guessesLeft)
        {
            //Console.Clear();
            Console.Write($"\n\nYou have now: {guessesLeft} guesses left.\nWrite a letter you think is a part of the word, or guess the word: ");
            string? input = Console.ReadLine();
            while (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Enter a word or a character you think is correct: ");
                input = Console.ReadLine();
            }
            return input;
        }



        public static bool CheckIfIsEqual(char[] secretChars, string inputLetters)
        {
            bool isEqual = secretChars.SequenceEqual(inputLetters.ToCharArray());
            return isEqual;
        }

        public static bool CorrectWordGuess(char[] secretChars, char[] guessedChars)
        {
            bool isEqual = secretChars.SequenceEqual(guessedChars);

            return isEqual;
        }


        public static char[] ShowSecretWordWithDashes(char[] letters)
        {
            char[] guessedChars = new char[letters.Length];
            int indx = 0;

            //TODO*********** Comment out show secretChars, when playing
            foreach (char letter in letters)
            {
                Console.Write($"{letter} -  ");
            }
            Console.WriteLine();

            foreach (char letter in guessedChars)
            {
                Console.Write("_  ");
                guessedChars[indx] = '_';
                indx++;
            }
            return guessedChars;

        }
        
        public static string ThrowIfNullOrEmpty(this string value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
            if (value == "")
            {
                throw new ArgumentException("Argument must not be the empty string.",
                                            name);
            }
            return value;
        }

        public static StringBuilder AddGuessedLetter(string inputLetter)
        {
            StringBuilder guessedLetters = new StringBuilder();
            guessedLetters.Append(inputLetter);
            return guessedLetters;
        }

        public static bool FirstGuess(StringBuilder sb, string input)
        {
;            bool firstGuess = true;
            for(int i = 0; i < sb.Length; i++)
            {
                if (sb.ToString().Contains(input))
                {
                    firstGuess = false;
                }
            }
            sb.Append($"{input}  ");
            return firstGuess;
        }

        public static char[] UpdateGuessedChars(char[] guessedChars, char[] secretChars,char inputLetter)
        {
            for (int i = 0; i < secretChars.Length; i++)
            {
                // Found the letter in the secret word
                if (secretChars[i] == inputLetter)
                {
                    guessedChars[i] = inputLetter;
                }
               
            }
            return guessedChars;
        }
    }
}
