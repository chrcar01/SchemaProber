using NUnit.Framework;
using SqlTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace SchemaProber.Tests
{
	[TestFixture]
	public class SqlServerProviderTests
	{
		private static readonly string _connectionString = "server=sx-alessandra;database=RipTrack2;user id=sa;password=d0v3R;";
		private IDbHelper _helper;
		private IDbProvider _provider;
		[TestFixtureSetUp]
		public void InitializeAllTests()
		{
			_helper = new SqlDbHelper(_connectionString);
			_provider = new SqlServerProvider(_helper);
		}
		[Test]
		public void CanGetTables()
		{
			var table = _provider.GetTableSchema("repairheader");
			foreach (PrimaryKeyColumnSchema pk in table.PrimaryKeys)
			{
				Console.WriteLine("{0}, {1}", pk.Name, pk.IsIdentity);
			}
		}
	}
}
