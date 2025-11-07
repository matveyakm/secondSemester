using System.Windows;

namespace WpfCalculatorTest
{
    /// <summary>
    /// Logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TokenazerTest tokenazerTest = new TokenazerTest();
            tokenazerTest.Add_PositiveNumbers_ReturnsExpectedResult();
            
        }
    }
}
