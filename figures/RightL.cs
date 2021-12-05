using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Tetris
{
    class RightL : Figure
    {
        public RightL(Grid grid) : base(grid)
        {
            Fields = (from Field el in grid.Children
                      where ((el.X == 4) && (el.Y == 0 || el.Y == 1 || el.Y == 2))
                      || (el.X == 5 && el.Y == 2)
                      select el).ToList();
            if (CheckFields())
                FillAll();
        }

        public override void Rotate()
        {
            List<Field> newFields = new List<Field>();

            if(Rotation == 0)
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

                for(int i = 0; i < 3; i++)
                {
                    newFields.Add(getField(XAxis + i, YAxis));
                }
                newFields.Add(getField(XAxis + 2, YAxis - 1)); 
            }
            else if(Rotation == 1)
            {
                int YAxis = Fields[0].Y;
                int XAxis = Fields[0].X;
                for (int i = 1; i < 4; i++)
                {
                    if (Fields[i].Y < YAxis)
                        YAxis = Fields[i].Y;
                    if (Fields[i].X > XAxis)
                        XAxis = Fields[i].X;
                }

                for (int i = 0; i < 3; i++)
                {
                    newFields.Add(getField(XAxis, YAxis - i));
                }
                newFields.Add(getField(XAxis - 1, YAxis - 2));
            }
            else if (Rotation == 2)
            {
                int YAxis = Fields[0].Y;
                int XAxis = Fields[0].X;
                for (int i = 1; i < 4; i++)
                {
                    if (Fields[i].Y > YAxis)
                        YAxis = Fields[i].Y;
                    if (Fields[i].X > XAxis)
                        XAxis = Fields[i].X;
                }

                for (int i = 0; i < 3; i++)
                {
                    newFields.Add(getField(XAxis - i, YAxis));
                }
                newFields.Add(getField(XAxis - 2, YAxis + 1));
            }
            else if (Rotation == 3)
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

                for (int i = 0; i < 3; i++)
                {
                    newFields.Add(getField(XAxis, YAxis + i));
                }
                newFields.Add(getField(XAxis + 1, YAxis + 2));
            }


            ConfirmRotation(newFields);
        }
    }
}