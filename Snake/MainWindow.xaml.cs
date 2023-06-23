using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int rows = 15, columns = 15;
        private readonly Image[,] gridImages;

        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.SnakeBody },
            { GridValue.Food, Images.Food },
        };

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetUpGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private Image[,] SetUpGrid()
        {
            Image[,] gridImages = new Image[rows, columns];
            GameGrid.Rows = rows;
            GameGrid.Columns = columns;

            for(int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,
                    };

                    gridImages[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return gridImages;
        }
    }
}
