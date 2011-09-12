using System;
using System.Collections.Generic;

namespace SchemaProber
{
	/// <summary>
	/// Represents a Primary Key ColumnSchema.
	/// </summary>
	public interface IPrimaryKeyColumnSchema : IColumnSchema
	{
		/// <summary>
		/// Gets the foreign key references.
		/// </summary>
		ForeignKeyColumnSchemaList ForeignKeyReferences { get; }
	}
}
