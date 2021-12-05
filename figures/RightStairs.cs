using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Tetris
{
    class RightStairs : Figure
    {
        public RightStairs(Grid grid) : base(grid)
        {
            Fields = (from Field el in grid.Children
                      where (el.Y == 0 && (el.X == 4 || el.X == 5)) || (el.Y == 1 && (el.X == 3 || el.X == 4))
                      select el).ToList();
            if (CheckFields())
                FillAll();
        }

        public override void Rotate()
        {
            List<Field> newFields = new List<Field>();

            if (Rotation == 0 || Rotation == 2)
            {
                int YAxis = Fields[0].Y;
                int XAxis = Fields[0].X;
                for (int i = 1; i < 4; i++)
                {
                    if (Fields[i].Y > YAxis)
                        YAxis = Fields[i].Y;
                    if (Fields[i].X < XAxis)
                        XAxis = Fields[i].X;
                }

                for (int i = 0; i < 2; i++)
                {
                    newFields.Add(getField(XAxis, YAxis + i));
                    newFields.Add(getField(XAxis + 1, YAxis + 1 + i));
                }
            }
            else if (Rotation == 1 || Rotation == 3)
            {
                int YAxis = Fields[0].Y;
                int XAxis = Fields[0].X;
                for (int i = 1; i < 4; i++)
                {
                    if (Fields[i].Y > YAxis)
                        YAxis = Fields[i].Y;
                    if (Fields[i].X < XAxis)
                        XAxis = Fields[i].X;
                }

                for (int i = 0; i < 2; i++)
                {
                    newFields.Add(getField(XAxis + i, YAxis));
                    newFields.Add(getField(XAxis + 1 + i, YAxis - 1));
                }
            }

            ConfirmRotation(newFields);
        }
    }
}
