using Magellan.Framework;
using RazorEngine;
using SchemaProber;
using SqlTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace RazorProber.Views.Home
{
	public class ColumnsViewModel : ViewModel
	{
		public virtual void Initialize()
		{
			ConnectionString = "server=.;database=NORTHWIND;integrated security=SSPI;";                           	
		}
		private string _connectionString;
		public string ConnectionString
		{
			get
			{
				return _connectionString;
			}
			set
			{
				_connectionString = value;
				base.NotifyChanged("ConnectionString");
			}
		}
		public ICommand PopulateTablesCommand
		{
			get
			{
				return new RelayCommand(
					() => 
					{
						var helper = new SqlDbHelper(ConnectionString);
						var provider = new SchemaProber.SqlServerProvider(helper);
						Tables = provider.GetTableSchemas().OrderBy(x => x.Name);
					});
			}
		}
		private IEnumerable<TableSchema> _tables;
		public IEnumerable<TableSchema> Tables
		{
			get
			{
				if (_tables == null)
				{
					_tables = new List<TableSchema>();
				}
				return _tables;
			}
			set
			{
				_tables = value;
				base.NotifyChanged("Tables");
			}
		}
		private TableSchema _selectedTable;
		public TableSchema SelectedTable
		{
			get
			{
				return _selectedTable;
			}
			set
			{
				_selectedTable = value;
				base.NotifyChanged("SelectedTable");
			}
		}
	}
}
