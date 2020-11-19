using System.Windows;

namespace GOST_28147_89
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new WindowViewModel();
		}
	}
}