using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tetris
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public readonly int GameHeight = 20;
        static public readonly int GameWidth = 10;
        static public bool StopMove { get; set; } = false;
        static public bool GameStop { get; set; } = false;
        Figure CurrentFigure;
        DispatcherTimer Timer;

        public MainWindow()
        {
            InitializeComponent();
            
            //Creation of grid inside code, its way faster than creating 200 elements in xaml
            for(int i = 0; i < GameWidth; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40)});
            }

            for(int i = 0; i < GameHeight; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40)});
            }

            for(int i = 0; i < GameHeight; i++)
            {
                for(int j = 0; j < GameWidth; j++)
                {
                    Field newField = new Field(j , i);
                    MainGrid.Children.Add(newField);
                    Grid.SetColumn(newField, j);
                    Grid.SetRow(newField, i);
                }
            }

            CurrentFigure = GetNewFigure();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 1);
            Timer.Tick += (x, y) =>
            {
                if (GameStop)
                {
                    MessageBox.Show("Game over!");
                    Timer.Stop();
                    return;
                }

                if (!StopMove)
                {
                    CurrentFigure.MoveDown();
                }
                else
                {
                    CheckRows();
                    CurrentFigure = GetNewFigure();
                }

                
            };

            Timer.Start();
        }

        Figure GetNewFigure()
        {
            if (GameStop)
                return null;
            Figure fig = null;
            Random rand = new Random();
            int num = rand.Next(1, 70);

            if (num < 10)
                fig = new Line(MainGrid);
            else if (num < 20)
                fig = new Square(MainGrid);
            else if (num < 30)
                fig = new RightL(MainGrid);
            else if (num < 40)
                fig = new LeftL(MainGrid);
            else if (num < 50)
                fig = new RightStairs(MainGrid);
            else if (num < 60)
                fig = new LeftStairs(MainGrid);
            else if (num <= 70)
                fig = new TLike(MainGrid);

            StopMove = false;
 
            return fig;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (StopMove)
                return;

            switch (e.Key)
            {
                case Key.Left:
                    CurrentFigure.MoveLeft();
                    break;
                case Key.Right:
                    CurrentFigure.MoveRight();
                    break;
                case Key.Down:
                    CurrentFigure.MoveDown();
                    break;
                case Key.Up:
                    CurrentFigure.Rotate();
                    break;
            }
        }

        // Checks if there are filled rows
        void CheckRows()
        {
            for(int i = 1; i < GameHeight; i++)
            {
                var row = from Field el in MainGrid.Children where el.Y == i select el;

                if (IsRowFull(row))
                {
                    ClearRow(row);
                    MoveRows(i);
                }
            }      
        }

        // Clears a row if its filled
        void ClearRow(IEnumerable<Field> row)
        {
            foreach (var el in row)
            {
                var PrevElement = (from Field x in MainGrid.Children
                                   where x.X == el.X && x.Y == el.Y - 1
                                   select x).First();
                el.UnFill();
                if (PrevElement.IsFilled)
                {
                    el.Fill(PrevElement.Color);
                    PrevElement.UnFill();
                }
            }
        }

        // Moves all rowes one down, starts counting upwards from given row number
        void MoveRows(int start)
        {
            for(int i = start; i > 1; i--)
            {
                var row = from Field el in MainGrid.Children where el.Y == i select el;

                foreach (var el in row)
                {
                    var PrevElement = (from Field x in MainGrid.Children
                                       where x.X == el.X && x.Y == el.Y - 1
                                       select x).First();
                    if (PrevElement.IsFilled)
                    {
                        el.Fill(PrevElement.Color);
                        PrevElement.UnFill();
                    }
                }

            }
        }

        // Checks of the row is all filled
        bool IsRowFull(IEnumerable<Field> row)
        {
            foreach (var el in row)
                if (!el.IsFilled)
                    return false;
            return true;
        }
    }
}
