namespace Minesweeper;

public class Minefield
{
    private readonly int[,] field;
    private readonly bool[,] visible;
    public int fieldSize;
    private Dictionary<int, ConsoleColor> colorMapping = new Dictionary<int, ConsoleColor>
        {
            {-1, ConsoleColor.DarkGray},  // Empty tile
            {9, ConsoleColor.DarkRed},    // Bomb tile

            {1, ConsoleColor.White},
            {2, ConsoleColor.Yellow},
            {3, ConsoleColor.DarkYellow},
            {4, ConsoleColor.Red},
            {5, ConsoleColor.Red},
            {6, ConsoleColor.Red},
            {7, ConsoleColor.Red},
            {8, ConsoleColor.Red}
        };

    public Minefield(int chosenFieldSize)
    {
        field = new int[chosenFieldSize, chosenFieldSize];
        visible = new bool[chosenFieldSize, chosenFieldSize];
        fieldSize = chosenFieldSize;
    }

    public void SetBomb(int x, int y)
    {
        field[x, y] = 9;
        IncrementAdjacentTiles(x, y);
    }

    private void IncrementAdjacentTiles(int x, int y)
    {
        for (int colX = -1; colX <= 1; colX++) //Iterate over 3 columns, from -1 to 1
        {
            for (int rowY = -1; rowY <= 1; rowY++) //Iterate over 3 rows, from -1 to 1
            {
                int tarX = x + colX;
                int tarY = y + rowY;
                if (TileIsOutOfBounds(tarX, tarY))
                    continue;
                else if (TileIsBomb(tarX, tarY))
                    continue;
                else
                    field[tarX, tarY]++; //Increase targeted tile's value
            }
        }
    }

    private void PrintTopLayer()
    {
        Console.Write("  ");
        for (int i = 0; i < fieldSize; i++)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine();
        Console.Write("  ");
        for (int i = 0; i < fieldSize; i++)
        {
            Console.Write($"__");
        }
        Console.WriteLine();
    }

    public void PrintField()
    {
        Console.Clear();
        PrintTopLayer();

        for (int y = fieldSize - 1; y >= 0; y--)
        {
            Console.Write($"{y}|");
            for (int x = 0; x < fieldSize; x++)
            {
                if (visible[x, y])
                {
                    if (TileIsBomb(x, y))
                        ColorPrint(9);
                    else if (TileIsEmpty(x, y))
                        Console.Write("  ");
                    else
                        ColorPrint(field[x, y]);
                }
                else
                    ColorPrint(-1);
            }
            Console.WriteLine();
        }
    }

    public void RevealTile(int x, int y)
    {
        if (TileIsOutOfBounds(x, y) || visible[x, y]) return; // Ignore if out of bounds or already visible

        visible[x, y] = true; // Set targeted tile to visible

        if (TileIsEmpty(x, y)) // If empty, check adjacent tiles
        {
            for (int tarX = -1; tarX <= 1; tarX++)
            {
                for (int tarY = -1; tarY <= 1; tarY++)
                {
                    if (tarX != 0 || tarY != 0) // Reveal if tile is not 0 0 (self)
                        RevealTile(x + tarX, y + tarY);
                }
            }
        }
    }

    public bool AllTilesAreRevealed()
    {
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                if (!TileIsBomb(x, y) && !visible[x, y])
                    return false;
            }
        }
        return true;
    }

    public void RevealField()
    {
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                visible[x, y] = true;
            }
        }
        PrintField();
    }

    public bool TileIsBomb(int x, int y)
    {
        if (field[x, y] == 9)
            return true;
        else
            return false;
    }

    public bool TileIsOutOfBounds(int x, int y)
    {
        if (x < 0 || x >= fieldSize)
            return true;
        else if (y < 0 || y >= fieldSize)
            return true;
        return false;
    }

    public bool TileIsEmpty(int x, int y)
    {
        if (field[x, y] == 0)
            return true;
        else
            return false;
    }

    private void ColorPrint(int value)
    {
        Console.ForegroundColor = colorMapping[value];

        if (value == -1)
            Console.Write(" ?");
        else if (value == 9)
            Console.Write(" X");
        else
            Console.Write($" {value}");

        Console.ForegroundColor = ConsoleColor.White;
    }
}