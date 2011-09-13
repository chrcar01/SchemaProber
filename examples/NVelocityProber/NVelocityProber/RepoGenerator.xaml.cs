using CodeGenTools;
using NVelocityProber.Models;
using SchemaProber;
using SqlTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NVelocityProber
{
	public partial class RepoGenerator : Window
	{
		public RepoGenerator()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private IEnumerable<TableSchema> _tables;
		private string _connectionString;
		private void TableComboBox_DropDownOpened(object sender, EventArgs e)
		{
			if (_tables != null 
				&& _tables.Count() > 0 
				&& !String.IsNullOrEmpty(_connectionString) 
				&& !String.IsNullOrEmpty(ConnectionStringTextBox.Text) 
				&& _connectionString.Equals(ConnectionStringTextBox.Text))
			{
				TableComboBox.ItemsSource = _tables;
				return;
			}

			var helper = new SqlDbHelper(ConnectionStringTextBox.Text);
			var provider = new SqlServerProvider(helper);
			var tables = provider.GetTableSchemas();
			_tables = tables.OrderBy(x => x.Name);
			TableComboBox.ItemsSource = _tables;
		}

		private void TableComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var selectedTable = TableComboBox.SelectedItem as TableSchema;
			if (selectedTable == null)
				return;


			var pkArgs = new List<PrimaryKeyMethodArg>();
			if (selectedTable.PrimaryKeys.Count > 0)
			{
				foreach (var pk in selectedTable.PrimaryKeys)
				{
					pkArgs.Add(new PrimaryKeyMethodArg { Name = Inflector.Camelize(pk.Name), CSharpAlias = TypeTranslator.ToCSharpAlias(pk.DataType) });
				}
			}

			var propertyColumns = new List<PropertyColumn>();			
			foreach (var col in selectedTable.Columns)
			{
				propertyColumns.Add(CreatePropertyColumn(col));
			}

			var nonKeyPropertyColumns = new List<PropertyColumn>();
			foreach (var col in selectedTable.NonPrimaryKeyColumns)
			{
				nonKeyPropertyColumns.Add(CreatePropertyColumn(col));
			}
			
			var primaryKeyColumns = new List<PropertyColumn>();
			foreach (var col in selectedTable.PrimaryKeys)
			{
				primaryKeyColumns.Add(CreatePropertyColumn(col));
			}

			var primaryKeyWhereClause = String.Empty;
			foreach (var pk in selectedTable.PrimaryKeys)
			{
				if (!String.IsNullOrEmpty(primaryKeyWhereClause)) primaryKeyWhereClause += " AND ";
				primaryKeyWhereClause += String.Format("[{0}] = @{1}", pk.Name, pk.Name.Replace(" ", ""));
			}
			
			var entityName = Inflector.Pascalize(selectedTable.Name.Replace(" ", ""));
			var templateDirectory = Environment.CurrentDirectory.Replace(@"\bin\Debug", "");
			var templateName = "RepoTemplate.vm";
			CodeTextBox.Text = new VelociWrapper(templateDirectory, templateName)
				.AddProperty("entityName", entityName)
				.AddProperty("pkArgs", pkArgs)
				.AddProperty("propertyColumns", propertyColumns)
				.AddProperty("nonKeyPropertyColumns", nonKeyPropertyColumns)
				.AddProperty("primaryKeyWhereClause", primaryKeyWhereClause)
				.AddProperty("primaryKeyColumns", primaryKeyColumns)
				.AddProperty("table", selectedTable)
				.Render();
		}
		private static PropertyColumn CreatePropertyColumn(IColumnSchema column)
		{
			var col = column as ColumnSchema;
			var pc = new PropertyColumn();
			pc.ColumnName = col.Name;
			pc.Length = col.Length;
			pc.PropertyName = col.Name; // Inflector.Pascalize(col.Name.Replace(" ", ""));
			pc.ArgName = Inflector.Camelize(col.Name.Replace(" ", ""));
			pc.SqlDbType = TypeTranslator.ToSqlDbType(col.SqlTypeName).ToString();
			pc.IsNullable = col.Nullable;
			pc.IsString = col.DataType == typeof(string);
			pc.CSharpAlias = TypeTranslator.ToCSharpAlias(col.SqlTypeName);
			pc.IsAutoIncrement = col.IsIdentity;
			pc.RequiresLengthDefinition = col.Length > 0
				&& !(new[] { "image", "text", "ntext" }.Contains(col.SqlTypeName, StringComparer.OrdinalIgnoreCase));
			return pc;
		}
	}
}
