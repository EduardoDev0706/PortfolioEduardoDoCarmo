using AgendaEletronica.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AgendaEletronica.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}