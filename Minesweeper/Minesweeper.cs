namespace Minesweeper;

class Minesweeper
{

    static void Main()
    {
        Console.Write("Welcome to Mickes version of Minesweeper!\n");

        //Menu loop
        while (true)
        {
            Console.Write("Size of minefield (5-10): ");

            int fieldSize = ChooseFieldSize();
            StartGame(fieldSize);

            Console.WriteLine("Would you like to play again? (y/n)");
            string? userInput = Console.ReadLine();
            if (userInput == "y" || userInput == "Y")
            {
            }
            else
            {
                break;
            }
        }

        Console.WriteLine("Thank you for playing, see you next time!");
        Console.ReadLine();
    }
    static void StartGame(int fieldSize)
    {
        var field = new Minefield(fieldSize);
        bool gameIsActive = true;

        Random rnd = new Random();
        int bombAmount = 0;

        while (bombAmount < fieldSize)
        {
            int x = rnd.Next(0, fieldSize - 1);
            int y = rnd.Next(0, fieldSize - 1);
            if (!field.TileIsBomb(x, y))
            {
                field.SetBomb(x, y);
                bombAmount++;
            }
        }

        // Game loop
        while (gameIsActive)
        {
            field.PrintField();
            Console.Write("Enter coordinates (x y): ");
            var input = Console.ReadLine();
            if (input == null) continue;

            var parts = input.Split(" ");
            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int x) ||
                !int.TryParse(parts[1], out int y) ||
                x > field.fieldSize - 1 ||
                y > field.fieldSize - 1)
            {
                Console.WriteLine("Invalid input. Please enter two numbers separated by a space. Press any key to continue...");
                Console.ReadLine();
                continue;
            }

            if (field.TileIsBomb(x, y))
            {
                field.RevealField();
                Console.WriteLine("Boom! You hit a bomb, and lost...");
                gameIsActive = false;
            }
            else
            {
                field.RevealTile(x, y);

                if (field.AllTilesAreRevealed())
                {
                    field.RevealField();
                    Console.WriteLine("Grattis! You found all safe tiles without blowing up!");
                    gameIsActive = false;
                }
            }
        }
    }
    static int ChooseFieldSize()
    {
        int fieldSize = 5;
        while (true)
        {
            bool fieldSizeChosen = int.TryParse(Console.ReadLine(), out fieldSize);
            if (!fieldSizeChosen || fieldSize < 5 || fieldSize > 10)
                Console.WriteLine("Please enter valid number between 5 - 10.");
            else
                break;
        }
        return fieldSize;
    }
}