using System;
using System.Collections.Generic;

namespace SchemaProber
{
	/// <summary>
	/// List of ViewSchemas
	/// </summary>
	public class ViewSchemaList : List<ViewSchema>
	{
		/// <summary>
		/// Gets the <see cref="ViewSchema"/> with the specified view name.
		/// </summary>
		/// <value>
		/// The <see cref="ViewSchema"/>.
		/// </value>
		/// <param name="viewName">Name of the view.</param>
		/// <returns></returns>
		public ViewSchema this[string viewName]
		{
			get
			{
				return Find(v => v.Name.Equals(viewName, StringComparison.OrdinalIgnoreCase));
			}
		}
	}
	/// <summary>
	/// List of TableSchemas.
	/// </summary>
	public class TableSchemaList : List<TableSchema>
	{
		/// <summary>
		/// Gets a TableSchema from the list of tables in this instance by name.
		/// </summary>
		/// <param name="tableName">Name of the table to find.</param>
		/// <returns></returns>
		public TableSchema this[string tableName]
		{
			get
			{
				return Find(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
			}
		}
	}
}
