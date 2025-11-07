
namespace WpfCalculator
{
        using System.Windows;
        using System.Windows.Controls;

        /// <summary>
        /// Logic for MainWindow.xaml.
        /// </summary>
        public partial class MainWindow : Window
        {
            private Tokenazer tokenazer;

            /// <summary>
            /// Initializes a new instance of the <see cref="MainWindow"/> class.
            /// </summary>
            public MainWindow()

            {
                this.InitializeComponent();
                DataContext = new MainWindowViewModel();
                this.tokenazer = new Tokenazer();
            }

            private void ButtonClick(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string content = button.Content.ToString();
                char symbol = char.Parse(content);
                if (symbol == '=')
                {
                    this.tokenazer.Complete();
                }
                else
                {
                    this.tokenazer.AddSymbol(symbol);
                }

                this.RefreshView();
            }

            private void ButtonClick_C(object sender, RoutedEventArgs e)
            {
                this.tokenazer = new Tokenazer();
                var viewMidel = (MainWindowViewModel)this.DataContext;
                viewMidel.ClearAll();
            }

            private void RefreshView()
            {
                var viewMidel = (MainWindowViewModel)this.DataContext;
                string res = this.tokenazer.GetResult();
                viewMidel.ClearAll();
                viewMidel.UpdateFormulaText(res);
            }
        }
}