using System;
using System.Collections.Generic;

namespace SchemaProber
{
	/// <summary>
	/// List of Primary Key ColumnSchemas.
	/// </summary>
	public class PrimaryKeyColumnSchemaList : List<IPrimaryKeyColumnSchema>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PrimaryKeyColumnSchemaList"/> class.
		/// </summary>
		public PrimaryKeyColumnSchemaList()
		{
			
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="PrimaryKeyColumnSchemaList"/> class.
		/// </summary>
		/// <param name="capacity">The capacity.</param>
		public PrimaryKeyColumnSchemaList(int capacity)
			: base(capacity)
		{
			
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="PrimaryKeyColumnSchemaList"/> class.
		/// </summary>
		/// <param name="collection">The collection.</param>
		public PrimaryKeyColumnSchemaList(IEnumerable<IPrimaryKeyColumnSchema> collection)
			: base(collection)
		{
			
		}

	}
}
