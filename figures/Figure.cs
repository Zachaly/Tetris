using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Controls;

namespace Tetris
{
    

    abstract class Figure
    {
        int _rotation = 0;
        protected Color Color;
        public List<Field> ReadFields { get { return Fields; } }
        protected List<Field> Fields; // Fields marked as part of this figure
        protected Grid Grid; // Grid with all fields
        // Contains current rotation of figure, default is zero, max value is 3
        protected int Rotation
        {
            get { return _rotation; }
            set
            {
                if (value > 3 || value < 0)
                    _rotation = 0;
                else
                    _rotation = value;
            }
        }

        public Figure(Grid grid)
        {
            Grid = grid;
            SetColor();
        }

        public abstract void Rotate();// Each figure type will rotate in other way

        // Picks a random color for the figure
        void SetColor()
        {
            Color[] PossibleColors = { Colors.Blue, Colors.Purple, Colors.Red, Colors.Yellow, Colors.Orange, Colors.Green };
            Random rand = new Random();
            int color = rand.Next(0, PossibleColors.Length);
            Color = PossibleColors[color];
        }

        // Moves figure right, returns if its not possible
        public void MoveRight()
        {
            List<Field> FieldsRight = new List<Field>();

            foreach (var f in Fields)
            {
                FieldsRight.Add(getField(f.X + 1, f.Y));
            }

            if (!CheckNewFields(FieldsRight))
                return;

            UnFillAll();
            Fields = FieldsRight;
            FillAll();
        }

        // Moves figure left, returns if not possible
        public void MoveLeft()
        {
            List<Field> FieldsLeft = new List<Field>();

            foreach (var f in Fields)
            {
                FieldsLeft.Add(getField(f.X - 1, f.Y));
            }

            if (!CheckNewFields(FieldsLeft))
                return;

            UnFillAll();
            Fields = FieldsLeft;
            FillAll();
        }

        // Moves figure one field down
        public void MoveDown()
        {
            List<Field> FieldsDown = new List<Field>();

            foreach (var f in Fields)
            {
                FieldsDown.Add(getField(f.X, f.Y + 1));
            }

            if (FieldsDown.Contains(null)) {
                MainWindow.StopMove = true;
                return;
            }


            foreach(var field in FieldsDown)
            {
                if (field.IsFilled && !Fields.Contains(field))
                {
                    MainWindow.StopMove = true;
                    return;
                }
            }

            UnFillAll();
            Fields = FieldsDown;
            FillAll();
        }


        // Gets field with certain x and y values
        protected Field getField(int x, int y)
        {
            try
            {
                return (from Field el in Grid.Children
                        where el.Y == y && el.X == x
                        select el).First();
            }
            catch(InvalidOperationException e)
            {
                return null;
            }

        }

        // Fills all fields
        protected void FillAll()
        {
            foreach (var field in Fields)
                field.Fill(Color);
        }

        // Unfills all fields
        protected void UnFillAll()
        {
            foreach (var field in Fields)
                field.UnFill();
        }

        // Checks if rotating/moving into new fields is possible
        protected bool CheckNewFields(List<Field> newfields)
        {
            foreach (var field in newfields)
                if (field is null || (field.IsFilled && !Fields.Contains(field)))
                    return false;

            return true;
        }

        // To be used when the new figure is generated, stop the game if new element is not possible to create
        protected bool CheckFields()
        {
            foreach (var field in Fields)
                if (field.IsFilled)
                {
                    MainWindow.StopMove = true;
                    MainWindow.GameStop = true;
                    return false;
                }
            return true;
        }

        // Checks if figure can rotate to the position, if can it rotates
        protected void ConfirmRotation(List<Field> newFields)
        {
            if (CheckNewFields(newFields))
            {
                UnFillAll();
                Fields = newFields;
                FillAll();
                Rotation++;
            }
        }
    }
}