using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    /// <summary>
    /// Logika interakcji dla klasy Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        // if the field is filled with a figure
        private bool _isFilled = false;
        public bool IsFilled {
            get { return _isFilled; }
        }

        public Color Color { get { return ((SolidColorBrush)ContentLabel.Background).Color; } }

        // Column and Row in grid
        public readonly int X, Y;

        public Field(int x, int y)
        {
            InitializeComponent();
            X = x;
            Y = y;
        }

        // Fills with a color and marks it as part of a figure
        public void Fill(Color color)
        {
            _isFilled = true;
            ContentLabel.Background = new SolidColorBrush(color);
        }

        // Reverts Fill
        public void UnFill()
        {
            _isFilled = false;
            ContentLabel.Background = new SolidColorBrush(Colors.Gray);
        }
    }
}
