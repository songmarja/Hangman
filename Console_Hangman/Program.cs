using System.Text;
using static Console_Hangman.Services;

namespace Console_Hangman
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Printing header and some text about the game to the player
            GetHeader();

            bool userWantsToPlay = true;
            while (userWantsToPlay)
            {
                // Stores all wrong guessed letters
                StringBuilder wrongLetters = new StringBuilder();

                //Check if user wants to play
                userWantsToPlay = ReadyToPlay();

                // Gets the random, secret word from string array and
                //  "Convert" the string into a char array
                //string[] secretWords = GetSecretWordsFromFile();
                char[] secretChars = GetSecretRandomWordFromFile();

                //Show the secret word by lower dashes for user
                char[] guessedChars = ShowSecretWordWithDashes(secretChars);

                // Start the game -loop, with first guess, and until user wants to quit - OR - finds the word - OR - user has made 10 guesses
                bool correctGuess = false;
                bool notTooManyGuesses = true;
                
                int guessCount = 0;
                while (!correctGuess && notTooManyGuesses)
                {
                    string inputLetters = ReadUserInput(10 - guessCount);

                    if (inputLetters.Length >= 2)
                    {
                        guessCount++;
                        // Compare input with secret word(in char[])
                        bool equalWords = CheckIfIsEqual(secretChars, inputLetters);
                        if (equalWords == true)
                        {
                            Console.WriteLine($"**** CONGRATULATION! You found out the word! **** ");
                            //CreateUserMessage();
                            correctGuess = true;
                        }
                        // It was not the secret word
                        else
                        {
                            if (guessCount >= 10)
                            {
                                Console.WriteLine("**** You missed it! You have used your 10 guesses. ****");
                                notTooManyGuesses = false;
                                //CreateUserMessage();
                                //TODO***** Validation, if want to play another game..
                            }
                            Console.WriteLine("**** Sorry, not the word I think about, but try again! ****");
                        }
                    }
                    else // = one character guess
                    {
                        bool letterNotFound = true;
                        for (int i = 0; i < secretChars.Length; i++)
                        {
                            // Found the letter in the secret word
                            if (secretChars[i] == inputLetters[0])
                            {
                                letterNotFound = false;
                            }
                        }

                        // when not found letter in secret word
                        if (letterNotFound == true)
                        {
                            //Is it first guess on that letter?
                            bool firstGuess = FirstGuess(wrongLetters, inputLetters);
                            if (firstGuess == true)
                            {
                                guessCount++;
                            }
                            Console.WriteLine("\n**** Noop. Not a letter in the secret word. Try again. ****");
                        }
                        // letter found
                        else
                        {
                            guessedChars = UpdateGuessedChars(guessedChars, secretChars, inputLetters[0]);
                            
                            correctGuess = CorrectWordGuess(secretChars, guessedChars);
                            if(correctGuess == true)
                            {
                                Console.WriteLine($"**** CONGRATULATION! You found out the word! ****");
                                correctGuess = true;
                                //Console.WriteLine("Do you want to play another game? y (yes) /n (no): ");
                            }
                            guessCount++;
                        }

                        if (guessCount >= 10)
                        {
                            Console.WriteLine("\n**** SORRY! You missed it! You have used your 10 guesses. ****");
                            notTooManyGuesses = false;
                            //TODO***** Validation, if want to play another game..
                        }

                        foreach (char letter in guessedChars)
                        {
                            Console.Write($"{letter} ");
                        }
                        Console.Write($"\n" +
                            $"You have tried this characters: {wrongLetters.ToString()}");
                    }
                }
            }
        }
    }
}