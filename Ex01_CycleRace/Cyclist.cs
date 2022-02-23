using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex01_CycleRace
{
    public class Cyclist
    {
        private static readonly int MIN_SPEED = 1;
        private static readonly int MAX_SPEED = 5;

        public static readonly int MAX_CYCLIST = Enum.GetValues(typeof(ConsoleColor)).Length - 2;


        public Cyclist(string name, ConsoleColor? color = null)
        {
            _rnd = new Random();
            Name = name.ToUpper();

            //generate random color if null
            if (color == null)
                RandomizeColor();
            else
                //set color
                Color = color.GetValueOrDefault();
        }

        public string Name { get; set; }

        public ConsoleColor Color { get; set; }

        private uint _wheelState;
        public uint WheelState {
            get => _wheelState;

            set
            {
                if (value >= _wheelchars.Length)
                    value = 0;

                _wheelState = value;
            }
        }


        private int XPos { get; set; }

        private readonly Random _rnd;

        private readonly char[] _wheelchars = new[] { '/', '-', '\\', '|' };



        public void Display()
        {
            Console.ForegroundColor = Color;


            //create whitespace string
            var wsString = new string(' ', XPos);

            //print the bike with the correct wheels
            Console.WriteLine(wsString + "   __°");
            Console.WriteLine(wsString + " _\\<,_");
            Console.WriteLine(wsString + $"({_wheelchars[WheelState]})/ ({_wheelchars[WheelState]})");

            DisplayStreet();
        }

        private void DisplayStreet()
        {
            var street = Name.PadRight(Console.BufferWidth - 1, '=') + "|";
            Console.WriteLine(street);
        }

        public bool Update()
        {
            WheelState++;
            XPos += Math.Clamp(_rnd.Next(MIN_SPEED, MAX_SPEED), 0, Console.BufferWidth - 9);

            return XPos >= Console.BufferWidth - 9;
        }

        public void RandomizeColor()
        {
            ConsoleColor color;

            //generate random color that's not white or black
            do { color = Program.GenerateRandomColor(_rnd); }
            while (color == ConsoleColor.Black || color == ConsoleColor.White);

            //assign it
            Color = color;
        }
    }
}
