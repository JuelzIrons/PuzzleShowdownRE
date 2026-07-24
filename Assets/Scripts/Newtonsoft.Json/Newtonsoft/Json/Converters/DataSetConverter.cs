namespace Newtonsoft.Json.Converters
{
	public class DataSetConverter : global::Newtonsoft.Json.JsonConverter
	{
		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::System.Data.DataSet obj = (global::System.Data.DataSet)value;
			global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver = serializer.ContractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver;
			global::Newtonsoft.Json.Converters.DataTableConverter dataTableConverter = new global::Newtonsoft.Json.Converters.DataTableConverter();
			writer.WriteStartObject();
			foreach (global::System.Data.DataTable table in obj.Tables)
			{
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(table.TableName) : table.TableName);
				dataTableConverter.WriteJson(writer, table, serializer);
			}
			writer.WriteEndObject();
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Data.DataSet dataSet = ((objectType == typeof(global::System.Data.DataSet)) ? new global::System.Data.DataSet() : ((global::System.Data.DataSet)global::System.Activator.CreateInstance(objectType)));
			global::Newtonsoft.Json.Converters.DataTableConverter dataTableConverter = new global::Newtonsoft.Json.Converters.DataTableConverter();
			reader.ReadAndAssert();
			while (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				global::System.Data.DataTable dataTable = dataSet.Tables[(string)reader.Value];
				bool num = dataTable != null;
				dataTable = (global::System.Data.DataTable)dataTableConverter.ReadJson(reader, typeof(global::System.Data.DataTable), dataTable, serializer);
				if (!num)
				{
					dataSet.Tables.Add(dataTable);
				}
				reader.ReadAndAssert();
			}
			return dataSet;
		}

		public override bool CanConvert(global::System.Type valueType)
		{
			return typeof(global::System.Data.DataSet).IsAssignableFrom(valueType);
		}
	}
}
