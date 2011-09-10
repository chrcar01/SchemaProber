using Magellan.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using SchemaProber;
using System.Collections.ObjectModel;
using SqlTools;
using System.Windows;
using System.IO;
using RazorEngine;

namespace RazorProber.Views.Home
{
	public class GenCrudViewModel : ViewModel
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
		public ICommand GenerateRepositoryCommand
		{
			get
			{
				return new RelayCommand(
					() => 
					{
						var templatePath = Path.Combine(Environment.CurrentDirectory.Replace(@"bin\Debug", ""), "RepositoryTemplate.cshtml");
						var template = File.ReadAllText(templatePath);
						Code = Razor.Parse(template, new { ClassName = SelectedTable.Name.Replace(" ",""), Table = SelectedTable });						
					});
			}
		}
		private string _code;
		public string Code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
				base.NotifyChanged("Code");
			}
		}

	}
}
