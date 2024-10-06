namespace MinesweeperTest;
using Minesweeper;

[TestClass]
public class Tests
{
    [TestMethod]
    [DataRow(1, 1, true)]
    [DataRow(1, 0, false)]
    public void SetBomb_TileIsBomb_BombIsTile11(int x, int y, bool expected)
    {
        Minefield field = new Minefield(5);
        field.SetBomb(1, 1);
        bool result = field.TileIsBomb(x, y);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(1, 1, false)]
    [DataRow(0, 1, false)]
    [DataRow(3, 3, true)]
    public void TileIsEmpty_CheckingTile11(int x, int y, bool expected)
    {
        Minefield field = new Minefield(5);
        field.SetBomb(x, y);

        bool result = field.TileIsEmpty(1, 1);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(1, 1, false)]
    [DataRow(-1, 1, true)]
    [DataRow(1, 11, true)]
    [DataRow(4, 4, false)]
    public void TileIsOutOfBounds_FromDataRow(int x, int y, bool expected)
    {
        Minefield field = new Minefield(5);

        bool result = field.TileIsOutOfBounds(x, y);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void AllTilesAreRevealed_ReturnFalse()
    {
        Minefield field = new Minefield(5);

        bool result = field.AllTilesAreRevealed();

        Assert.IsFalse(result);
    }
}
