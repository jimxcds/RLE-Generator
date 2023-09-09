using System;

namespace Run_Length_encoding
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            TitleCard();
            programStart:
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Would you like to encode or decode? \npress: (1 encode or 2 decode)");
            Console.Write("Key: ");
            var keyPress = Console.ReadKey();
            Console.WriteLine();
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:
                    EncodeUserInterface();
                    break;
                case ConsoleKey.D2:
                    DecodeUserInterface();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid key code\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto programStart;
            }
            
            Console.WriteLine("\nWould you like to encode/decode again or exit the program? \npress: (1 continue or 2 exit)");
            retryKeyPress:
            Console.Write("Key: ");
            keyPress = Console.ReadKey();
            Console.WriteLine();
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine();
                    goto programStart;
                case ConsoleKey.D2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter a valid key code\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto retryKeyPress;
            }

            void EncodeUserInterface()
            {
                Console.WriteLine("\nEnter the string you want to encode");
                string toBeEncoded = Console.ReadLine();
                string encodedString = Encode(toBeEncoded);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Result: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(encodedString + "\nPress any key to continue");
                Console.ReadKey();
            }

            void DecodeUserInterface()
            {
                Console.WriteLine("\nEnter the string you want to decode");
                string toBeDecoded = Console.ReadLine();
                string decodedString = Decode(toBeDecoded);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Result: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(decodedString + "\nPress any key to continue");
                Console.ReadKey();
            }
        }

        static void TitleCard()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("__________.____     ___________");
            Console.WriteLine("\\______   \\    |    \\_   _____/");
            Console.WriteLine(" |       _/    |     |    __)_ ");
            Console.WriteLine(" |    |   \\    |___  |        \\");
            Console.WriteLine(" |____|_  /_______ \\/_______  /");
            Console.WriteLine("        \\/        \\/        \\/ ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ___  ____  __ _  ____  ____   __  ____  __  ____ ");
            Console.WriteLine(" / __)(  __)(  ( \\(  __)(  _ \\ / _\\(_  _)/  \\(  _ \\");
            Console.WriteLine("( (_ \\ ) _) /    / ) _)  )   //    \\ )( (  O ))   /");
            Console.WriteLine(" \\___/(____)\\_)__)(____)(__\\_)\\_/\\_/(__) \\__/(__\\_)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Created by: James Daniel");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string Encode(string stringToBeEncoded)
        {
            char[] stringChars = stringToBeEncoded.ToCharArray();
            string encodedString = "";
            bool isInRepeatedChar = false;
            CurrantCharData currantCharData = new CurrantCharData( null, 0);
            for (int currantChar = 0; currantChar < stringChars.Length; currantChar++)
            {
                if (stringChars.Length == 1)
                {
                    currantCharData.Letter = stringChars[currantChar].ToString();
                    currantCharData.NumberOfTimesMentioned++;
                    encodedString += currantCharData.Letter + (currantCharData.NumberOfTimesMentioned);
                    break;
                }
                if (currantChar + 1 == stringChars.Length)
                {
                    if (currantCharData.Letter == stringChars[currantChar].ToString())
                    {
                        currantCharData.NumberOfTimesMentioned++;
                        encodedString += currantCharData.Letter + (currantCharData.NumberOfTimesMentioned);
                        currantCharData.Letter = stringChars[currantChar].ToString();
                    }
                    else
                    {
                        encodedString += currantCharData.Letter + (currantCharData.NumberOfTimesMentioned);
                        currantCharData.Letter = stringChars[currantChar].ToString();
                        currantCharData.NumberOfTimesMentioned = 0;
                        currantCharData.NumberOfTimesMentioned++;
                        encodedString += currantCharData.Letter + (currantCharData.NumberOfTimesMentioned);
                        currantCharData.Letter = stringChars[currantChar].ToString();
                    }
                    
                    break;
                }
                
                if (currantChar == 0)
                {
                    currantCharData.Letter = stringChars[currantChar].ToString();
                    currantCharData.NumberOfTimesMentioned++;
                    continue;
                }

                if (currantCharData.Letter == stringChars[currantChar].ToString())
                {
                    if (isInRepeatedChar == false)
                    {
                        isInRepeatedChar = true;
                    }

                    currantCharData.NumberOfTimesMentioned++;
                }
                else
                {
                    encodedString += currantCharData.Letter + (currantCharData.NumberOfTimesMentioned);
                    isInRepeatedChar = false;
                    currantCharData.Letter = stringChars[currantChar].ToString();
                    currantCharData.NumberOfTimesMentioned = 0;
                    currantCharData.NumberOfTimesMentioned++;
                }

            }

            Console.WriteLine("\nNumber of characters before encoding: " + stringToBeEncoded.Length +
                              "\nNumber of characters after encoding: " + encodedString.Length);
            return encodedString;
        }

        struct CurrantCharData
        {
            public string Letter;
            public int NumberOfTimesMentioned;

            public CurrantCharData(string letter, int numberOfTimesMentioned)
            {
                Letter = letter;
                NumberOfTimesMentioned = numberOfTimesMentioned;
            }
        }

        static string Decode(string stringToBeDecoded)
        {
            char[] stringChars = stringToBeDecoded.ToCharArray();
            char? currantLetter = null;
            string currantNumber = "";
            string decodedString = "";
            for (int currantChar = 0; currantChar < stringChars.Length; currantChar++)
            {
                if (currantLetter == null && Char.IsDigit(stringChars[currantChar]))
                {
                    Console.WriteLine("Incorrect format!");
                    return null;
                }

                if (currantChar + 1 == stringChars.Length)
                {
                    currantNumber += stringChars[currantChar];
                    Int32.TryParse(currantNumber, out var numberOfLetters);
                    for (int currantLetterToCreate = 0; currantLetterToCreate < numberOfLetters; currantLetterToCreate++)
                    {
                        decodedString += currantLetter;
                    }
                    break;
                }
                
                if (!Char.IsDigit(stringChars[currantChar]))
                {
                    if (currantLetter == null)
                    {
                        currantLetter = stringChars[currantChar];
                    }
                    else
                    {
                        Int32.TryParse(currantNumber, out var numberOfLetters);
                        for (int currantLetterToCreate = 0; currantLetterToCreate < numberOfLetters; currantLetterToCreate++)
                        {
                            decodedString += currantLetter;
                        }

                        currantLetter = stringChars[currantChar];
                        currantNumber = "";
                    }
                    continue;
                }

                if (Char.IsDigit(stringChars[currantChar]))
                {
                    currantNumber += stringChars[currantChar];
                }
                
                
            }
            Console.WriteLine("\nNumber of characters before decoding: " + stringToBeDecoded.Length +
                              "\nNumber of characters after decoding: " + decodedString.Length);
            return decodedString;
        }
    }
}