using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NVelocityProber
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ClassGeneratorButton_Click(object sender, RoutedEventArgs e)
		{
			var win = new ClassGenerator();
			win.Owner = this;
			win.Show();
		}

		private void RepoGeneratorButton_Click(object sender, RoutedEventArgs e)
		{
			var win = new RepoGenerator();
			win.Owner = this;
			win.Show();
		}
	}
}
