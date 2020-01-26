
using System.Collections;
using System.IO;

namespace Minesweeper
{
    public class MinesweeperRunner
    {
        public static void Main(string[] args)
        {
            Minesweeper sweeper = new Minesweeper(args[0], args[1]);
        }
    }
}