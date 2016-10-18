using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaProber
{
	/// <summary>
	/// List of ColumnSchemas.
	/// </summary>
	public class ColumnSchemaList : List<IColumnSchema>
	{
		/// <summary>
		/// Gets a ColumnSchema by name from the list of ColumnSchemas.
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public IColumnSchema this[string columnName]
		{
			get
			{
				return this.FirstOrDefault(x => x.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
			}
		}
		/// <summary>
		/// Initializes a new instance of the ColumnSchemaList
		/// </summary>
		public ColumnSchemaList()
		{
		}
		/// <summary>
		/// Initializes a new instance of the ColumnSchemaList
		/// </summary>
		/// <param name="collection"></param>
		public ColumnSchemaList(IEnumerable<IColumnSchema> collection)
			: base(collection)
		{
			
		}
	}
}
