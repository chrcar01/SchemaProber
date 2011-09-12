using NUnit.Framework;
using SqlTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaProber.Tests
{
	[TestFixture]
	public class SqlServerProviderTests
	{
		private static readonly string _connectionString = "server=.;database=northwind;integrated security=true;";
		private IDbHelper _helper;
		private IDbProvider _provider;
		[TestFixtureSetUp]
		public void InitializeAllTests()
		{
			_helper = new SqlDbHelper(_connectionString);
			_provider = new SqlServerProvider(_helper);
		}

		[Test]
		public void VerifyNonPrimaryKeyColumnsDoesNotContainPrimaryKeys()
		{
			var table = _provider.GetTableSchema("Order Details");
			Assert.AreEqual(2, table.PrimaryKeys.Count);
			Assert.AreEqual(3, table.NonPrimaryKeyColumns.Count);
			Assert.AreEqual(5, table.Columns.Count);

		}
		[Test]
		public void VerifyMultiplePrimaryKeys()
		{
			var primaryKeys = _provider.GetPrimaryKeys("Order Details");
			Assert.AreEqual(2, primaryKeys.Count);			
		}
		[Test]
		public void VerifyForeignKeys()
		{
			var foreignKeys = _provider.GetForeignKeys("Products");
			Assert.AreEqual(2, foreignKeys.Count);
		}
		[Test]
		public void CanGetTableSchemas()
		{
			var tables = _provider.GetTableSchemas();
			Assert.AreEqual(13, tables.Count());
		}
	}
}
