
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

namespace Ex01_CycleRace
{
    internal class Program
    {

        static T RequestInput<T>(string request, bool writeLine = false)
        {
            while (true)
            {
                if (writeLine) 
                    Console.WriteLine(request);
                else 
                    Console.Write(request);

                var input = Console.ReadLine();
                if (input == null) 
                    continue;

                //check if we can convert input to specified type
                var rv = Convert.ChangeType(input, typeof(T));
                if (rv == null) 
                    continue;

                return (T) rv;
            }
        }

        static T[] RequestInputArray<T>(string request, bool writeLine = false, char delim = ',', bool skipInvallid = true)
        {
            while (true)
            {
                start:
                if (writeLine)
                    Console.WriteLine(request);
                else
                    Console.Write(request);

                var input = Console.ReadLine();
                if (input == null) goto start;


                var inputArr = input.Split(delim);

                List<T> output = new List<T>();

                foreach (var s in inputArr)
                {
                    //check if we can convert input to specified type
                    var convertedObj = Convert.ChangeType(s, typeof(T));

                    if (convertedObj == null)
                        if (skipInvallid)
                            continue;
                        else 
                            goto start;

                    output.Add((T)convertedObj);
                }

                return output.ToArray();
            }
        }

        static void Main(string[] args)
        {
            /* Initialization */
            Console.BufferWidth = Console.WindowWidth;

            /* Console loop */
            while (true)
            {
                /* input */
                var names = RequestInputArray<string>("Enter name of the cyclist (seperated by ','): ");

                if (names.Length > Cyclist.MAX_CYCLIST)
                {
                    Console.WriteLine($"Too many cyclists, you can only add up to: {Cyclist.MAX_CYCLIST}");
                    continue;
                }

                Console.WriteLine($"Ready, {string.Join(", ", names.Take(names.Length-1))}{(names.Length > 1 ? $" and {names.Last()}" : "")}? Let the race begin!!");

                //create cyclists
                Cyclist[] cyclists = names.Select(s => new Cyclist(s.Trim())).ToArray();

                //store cursor position
                var (left, top) = Console.GetCursorPosition();

                bool won = false;
                Cyclist? winner = null;
                while (!won)
                {
                    //set cursor at start to overwrite
                    Console.SetCursorPosition(left, top);

                    foreach (var cyclist in cyclists)
                    {
                        if (!won && cyclist.Update())
                        {
                            won = true;
                            winner = cyclist;
                        }

                        cyclist.Display();
                    }

                    Thread.Sleep(100);
                }

                Debug.Assert(winner != null, nameof(winner) + " != null");
                AnnounceWinner(winner);
            }
        }

        private static void AnnounceWinner(Cyclist winner)
        {
            Console.WriteLine();
            Console.ForegroundColor = winner.Color;
            const char boxChar = '*';
            
            var winnerString = $"{boxChar} AND THE WINNER IS.... {winner.Name}!!! {boxChar}";
            var boxBotTop = new string(boxChar, winnerString.Length);

            Console.WriteLine(boxBotTop);
            Console.WriteLine(winnerString);
            Console.WriteLine(boxBotTop);
        }

        public static ConsoleColor GenerateRandomColor(Random? rand)
        {
            rand ??= new Random();

            var consoleColorValues = Enum.GetValues(typeof(ConsoleColor));
            var randNr = rand.Next(consoleColorValues.Length);
            return (ConsoleColor)(consoleColorValues.GetValue(randNr) ?? throw new Exception("Random color returned null value"));
        }
    }
}