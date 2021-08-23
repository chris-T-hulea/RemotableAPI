using System.Windows;

namespace Remotable
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(MainWindowViewModel viewModel)
		{
			this.DataContext = viewModel;
			this.InitializeComponent();
			this.Show();
		}
	}
}