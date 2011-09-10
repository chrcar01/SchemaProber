using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SchemaProber;
using SqlTools;

namespace NVelocityProber
{
	public partial class ClassGenerator : Window
	{
		public ClassGenerator()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void TableComboBox_DropDownOpened(object sender, EventArgs e)
		{
			var helper = new SqlDbHelper(ConnectionStringTextBox.Text);
			var provider = new SqlServerProvider(helper);
			var tables = provider.GetTableSchemas();
			TableComboBox.ItemsSource = tables.OrderBy(x => x.Name);
		}

		private void TableComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var selectedTable = TableComboBox.SelectedItem as TableSchema;
			if (selectedTable == null)
				return;

			var templateDirectory = Environment.CurrentDirectory.Replace(@"\bin\Debug", "");
			var templateName = "ClassTemplate.vm";
			CodeTextBox.Text = new VelociWrapper(templateDirectory, templateName)
				.AddProperty("className", Inflector.Pascalize(selectedTable.Name.Replace(" ","")))
				.AddProperty("columns", selectedTable.Columns)
				.Render();
		}
	}
}
