using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Robot.Kinematics.UI.ViewModels;
using Robot.Kinematics.UI.Properties;

namespace Robot.Kinematics.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private MainWindowViewModel m_MainWindowViewModel;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var mainWindow = new MainWindow();

			m_MainWindowViewModel = new MainWindowViewModel();
			mainWindow.DataContext = m_MainWindowViewModel;

			mainWindow.Show();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Settings.Default.Save();

			m_MainWindowViewModel.OnExit();
		} 
	}
}
