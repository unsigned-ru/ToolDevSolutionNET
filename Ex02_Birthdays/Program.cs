

using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Welcome to the console Birthday app. ");
        Console.WriteLine("Current Date: " + DateTime.Now.ToString("d", CultureInfo.CurrentCulture));

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(">> Please enter a day of birth: ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This was not a valid date: ".PadLeft(5, ' '));
            }

            DateTime parsedDate;

            if
            (
                DateTime.TryParse(input, CultureInfo.CurrentCulture,
                    DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite, out parsedDate) ||
                DateTime.TryParse(input, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite, out parsedDate)
            )
            {
                //check if date is in the future
                if (DateTime.Now < parsedDate)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This was not a valid date: ".PadLeft(5, ' '));
                }
                else
                {
                    DateTime today = DateTime.Now;
                    //valid case
                    DateTime nextBday = new DateTime(today.Year, parsedDate.Month, parsedDate.Day);
                    if (nextBday < today)
                        nextBday = nextBday.AddYears(1);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"This person is born on {parsedDate:dddd M, yyyy}.".PadLeft(5, ' '));
                    Console.WriteLine($"-> {(nextBday - today).Days} days to go until its next birthday!".PadLeft(5, ' '));

                }
            }
            else
            {
                //no valid date found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This was not a valid date: ".PadLeft(5, ' '));
            }

        }
    }
}
