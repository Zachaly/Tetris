using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Tetris
{
    class Line : Figure
    {
        public Line(Grid grid) : base(grid)
        {
            Fields = (from Field el in grid.Children
                      where el.X == 4 && (el.Y == 0 || el.Y == 1 || el.Y == 2 || el.Y == 3)
                      select el).ToList();
            if (CheckFields())
                FillAll();
        }

        public override void Rotate()
        {
            List<Field> newFields = new List<Field>();
            
            if (Rotation == 0 || Rotation == 2) // All fields are relocated to the last row
            {
                int Highest = Fields[0].Y;
                for(int i = 1; i < 4; i++)
                {
                    if (Fields[i].Y > Highest)
                        Highest = Fields[i].Y;
                }

                for (int i = 0; i <= 3; i++)
                {
                    newFields.Add(getField(Fields[0].X + i, Highest));
                }
                    
            }
            else if (Rotation == 1 || Rotation == 3) // All field are relocated to the first column
            {
                int Lowest = Fields[0].X;

                for (int i = 1; i < 4; i++)
                {
                    if (Fields[i].X < Lowest)
                        Lowest = Fields[i].X;
                }

                for (int i = 0; i < 4; i++)
                {
                    newFields.Add(getField(Lowest, Fields[0].Y - i));
                }
            }

            ConfirmRotation(newFields);
        }

    }
}
