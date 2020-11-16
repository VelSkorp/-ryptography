using System.Windows;

namespace DataEncryptionStandard
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