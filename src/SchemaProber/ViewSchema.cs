using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaProber
{
	/// <summary>
	/// Represents a database view definition.
	/// </summary>
	/// <seealso cref="SchemaProber.TableSchema" />
	public class ViewSchema : TableSchema
	{
		/// <summary>
		/// Initializes a new instance of the TableSchema class.
		/// </summary>
		public ViewSchema()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TableSchema class.
		/// </summary>
		/// <param name="name">Name of the table.</param>
		/// <param name="dbprovider">Instance of the dbprovider used to obtain information about the table.</param>
		public ViewSchema(IDbProvider dbprovider, string name)
			: base(dbprovider, name)
		{
		}
	}
}
