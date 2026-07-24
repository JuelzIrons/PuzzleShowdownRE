namespace Newtonsoft.Json.Linq.JsonPath
{
	internal abstract class PathFilter
	{
		public abstract global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings);

		protected static global::Newtonsoft.Json.Linq.JToken? GetTokenIndex(global::Newtonsoft.Json.Linq.JToken t, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings, int index)
		{
			if (t is global::Newtonsoft.Json.Linq.JArray jArray)
			{
				if (jArray.Count <= index)
				{
					if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Index {0} outside the bounds of JArray.", global::System.Globalization.CultureInfo.InvariantCulture, index));
					}
					return null;
				}
				return jArray[index];
			}
			if (t is global::Newtonsoft.Json.Linq.JConstructor jConstructor)
			{
				if (jConstructor.Count <= index)
				{
					if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Index {0} outside the bounds of JConstructor.", global::System.Globalization.CultureInfo.InvariantCulture, index));
					}
					return null;
				}
				return jConstructor[index];
			}
			if (settings != null && settings.ErrorWhenNoMatch)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Index {0} not valid on {1}.", global::System.Globalization.CultureInfo.InvariantCulture, index, t.GetType().Name));
			}
			return null;
		}

		protected static global::Newtonsoft.Json.Linq.JToken? GetNextScanValue(global::Newtonsoft.Json.Linq.JToken originalParent, global::Newtonsoft.Json.Linq.JToken? container, global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (container != null && container.HasValues)
			{
				value = container.First;
			}
			else
			{
				while (value != null && value != originalParent && value == value.Parent.Last)
				{
					value = value.Parent;
				}
				if (value == null || value == originalParent)
				{
					return null;
				}
				value = value.Next;
			}
			return value;
		}
	}
}
