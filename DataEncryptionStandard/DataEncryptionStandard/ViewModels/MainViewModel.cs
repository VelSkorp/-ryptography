using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace DataEncryptionStandard
{
	public class MainViewModel : BaseViewModel
	{
		#region Public Properties

		public List<FileSystemObjectInfo> ItemsTextInput { get; set; }
		public List<FileSystemObjectInfo> ItemsTextOutput { get; set; }
		public List<FileSystemObjectInfo> ItemsKeyInput { get; set; }
		public FileSystemObjectInfo SelectedTextInputItem { get; set; }
		public FileSystemObjectInfo SelectedKeyInputItem { get; set; }
		public FileSystemObjectInfo SelectedTextOutputItem { get; set; }

		#endregion

		#region Commands

		public ICommand EncryptCommand { get; set; }

		public ICommand DecryptCommand { get; set; }

		#endregion

		#region Constructor

		public MainViewModel()
		{
			ItemsTextInput = new List<FileSystemObjectInfo>();
			ItemsTextOutput = new List<FileSystemObjectInfo>();
			ItemsKeyInput = new List<FileSystemObjectInfo>();

			var drives = DriveInfo.GetDrives();
			DriveInfo.GetDrives().ToList().ForEach(drive =>
			{
				ItemsTextInput.Add(new FileSystemObjectInfo(drive));
				ItemsTextOutput.Add(new FileSystemObjectInfo(drive));
				ItemsKeyInput.Add(new FileSystemObjectInfo(drive));
			});

			EncryptCommand = new RelayCommand(Encrypt);
			DecryptCommand = new RelayCommand(Decrypt);
		}

		#endregion

		#region Command Methods

		public void Encrypt()
		{



		}

		public void Decrypt()
		{
		}

		#endregion
	}
}