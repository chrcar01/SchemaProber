using System;
using System.Collections.Generic;
using System.Linq;

namespace NVelocityProber.Models
{
	public class PropertyColumn
	{
		public string ColumnName { get; set; }
		public bool IsString { get; set; }
        public string PropertyName { get; set; }
		public string SqlDbType { get; set; }
		public int Length { get; set; }
		public bool RequiresLengthDefinition { get; set; }
		public string ArgName { get; set; }
		public bool IsNullable { get; set; }
		public string CSharpAlias { get; set; }
		public bool IsAutoIncrement { get; set; }
	}
}
