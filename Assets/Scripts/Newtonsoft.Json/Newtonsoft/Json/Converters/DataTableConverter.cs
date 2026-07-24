namespace Newtonsoft.Json.Converters
{
	public class DataTableConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::System.Data.DataTable obj = (global::System.Data.DataTable)value;
			global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver = serializer.ContractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver;
			writer.WriteStartArray();
			foreach (global::System.Data.DataRow row in obj.Rows)
			{
				writer.WriteStartObject();
				foreach (global::System.Data.DataColumn column in row.Table.Columns)
				{
					object obj2 = row[column];
					if (serializer.NullValueHandling != global::Newtonsoft.Json.NullValueHandling.Ignore || (obj2 != null && obj2 != global::System.DBNull.Value))
					{
						writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(column.ColumnName) : column.ColumnName);
						serializer.Serialize(writer, obj2);
					}
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Data.DataTable dataTable = existingValue as global::System.Data.DataTable;
			if (dataTable == null)
			{
				dataTable = ((objectType == typeof(global::System.Data.DataTable)) ? new global::System.Data.DataTable() : ((global::System.Data.DataTable)global::System.Activator.CreateInstance(objectType)));
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				dataTable.TableName = (string)reader.Value;
				reader.ReadAndAssert();
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
				{
					return dataTable;
				}
			}
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartArray)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected JSON token when reading DataTable. Expected StartArray, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			reader.ReadAndAssert();
			while (reader.TokenType != global::Newtonsoft.Json.JsonToken.EndArray)
			{
				CreateRow(reader, dataTable, serializer);
				reader.ReadAndAssert();
			}
			return dataTable;
		}

		private static void CreateRow(global::Newtonsoft.Json.JsonReader reader, global::System.Data.DataTable dt, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			global::System.Data.DataRow dataRow = dt.NewRow();
			reader.ReadAndAssert();
			while (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				string text = (string)reader.Value;
				reader.ReadAndAssert();
				global::System.Data.DataColumn dataColumn = dt.Columns[text];
				if (dataColumn == null)
				{
					global::System.Type columnDataType = GetColumnDataType(reader);
					dataColumn = new global::System.Data.DataColumn(text, columnDataType);
					dt.Columns.Add(dataColumn);
				}
				if (dataColumn.DataType == typeof(global::System.Data.DataTable))
				{
					if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartArray)
					{
						reader.ReadAndAssert();
					}
					global::System.Data.DataTable dataTable = new global::System.Data.DataTable();
					while (reader.TokenType != global::Newtonsoft.Json.JsonToken.EndArray)
					{
						CreateRow(reader, dataTable, serializer);
						reader.ReadAndAssert();
					}
					dataRow[text] = dataTable;
				}
				else if (dataColumn.DataType.IsArray && dataColumn.DataType != typeof(byte[]))
				{
					if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartArray)
					{
						reader.ReadAndAssert();
					}
					global::System.Collections.Generic.List<object> list = new global::System.Collections.Generic.List<object>();
					while (reader.TokenType != global::Newtonsoft.Json.JsonToken.EndArray)
					{
						list.Add(reader.Value);
						reader.ReadAndAssert();
					}
					global::System.Array array = global::System.Array.CreateInstance(dataColumn.DataType.GetElementType(), list.Count);
					((global::System.Collections.ICollection)list).CopyTo(array, 0);
					dataRow[text] = array;
				}
				else
				{
					object value = ((reader.Value != null) ? (serializer.Deserialize(reader, dataColumn.DataType) ?? global::System.DBNull.Value) : global::System.DBNull.Value);
					dataRow[text] = value;
				}
				reader.ReadAndAssert();
			}
			dataRow.EndEdit();
			dt.Rows.Add(dataRow);
		}

		private static global::System.Type GetColumnDataType(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.JsonToken tokenType = reader.TokenType;
			switch (tokenType)
			{
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
				return reader.ValueType;
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.Undefined:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return typeof(string);
			case global::Newtonsoft.Json.JsonToken.StartArray:
				reader.ReadAndAssert();
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartObject)
				{
					return typeof(global::System.Data.DataTable);
				}
				return GetColumnDataType(reader).MakeArrayType();
			default:
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected JSON token when reading DataTable: {0}", global::System.Globalization.CultureInfo.InvariantCulture, tokenType));
			}
		}

		public override bool CanConvert(global::System.Type valueType)
		{
			return typeof(global::System.Data.DataTable).IsAssignableFrom(valueType);
		}
	}
}
