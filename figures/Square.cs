using System.Linq;
using System.Windows.Controls;

namespace Tetris
{
    class Square : Figure
    {
        public Square(Grid grid) : base(grid)
        {
            Fields = (from Field el in grid.Children
                      where (el.X == 3 || el.X == 4) && (el.Y == 0 || el.Y == 1)
                      select el).ToList();
            if (CheckFields())
                FillAll();
        }

        public override void Rotate()
        {
            
        }
    }
}
