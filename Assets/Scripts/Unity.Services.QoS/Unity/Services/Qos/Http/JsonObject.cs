namespace Unity.Services.Qos.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Qos.Http.JsonObjectConverter))]
	internal class JsonObject : global::Unity.Services.Qos.Http.IDeserializable
	{
		[global::UnityEngine.Scripting.Preserve]
		internal object obj;

		[global::UnityEngine.Scripting.Preserve]
		internal JsonObject(object obj)
		{
			this.obj = obj;
		}

		public string GetAsString()
		{
			try
			{
				if (obj == null)
				{
					return "";
				}
				if (obj.GetType() == typeof(string))
				{
					return obj.ToString();
				}
				return global::Newtonsoft.Json.JsonConvert.SerializeObject(obj);
			}
			catch (global::System.Exception)
			{
				throw new global::System.InvalidOperationException("Failed to convert JsonObject to string.");
			}
		}

		public T GetAs<T>(global::Unity.Services.Qos.Http.DeserializationSettings deserializationSettings = null)
		{
			deserializationSettings = deserializationSettings ?? new global::Unity.Services.Qos.Http.DeserializationSettings();
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = ((deserializationSettings.MissingMemberHandling == global::Unity.Services.Qos.Http.MissingMemberHandling.Error) ? global::Newtonsoft.Json.MissingMemberHandling.Error : global::Newtonsoft.Json.MissingMemberHandling.Ignore)
			};
			try
			{
				T val = global::Newtonsoft.Json.JsonConvert.DeserializeObject<T>(global::Newtonsoft.Json.JsonConvert.SerializeObject(obj), settings);
				global::System.Collections.Generic.List<string> list = ValidateObject(val);
				if (list.Count > 0)
				{
					throw new global::Unity.Services.Qos.Http.DeserializationException(string.Join("\n", list));
				}
				return val;
			}
			catch (global::Unity.Services.Qos.Http.DeserializationException)
			{
				throw;
			}
			catch (global::Newtonsoft.Json.JsonSerializationException ex2)
			{
				throw new global::Unity.Services.Qos.Http.DeserializationException(ex2.Message);
			}
			catch (global::System.Exception)
			{
				throw new global::Unity.Services.Qos.Http.DeserializationException("Unable to deserialize object.");
			}
		}

		public T GetAs<T>()
		{
			return GetAs<T>(null);
		}

		internal static global::Unity.Services.Qos.Http.IDeserializable GetNewJsonObjectResponse(object o)
		{
			return new global::Unity.Services.Qos.Http.JsonObject(o);
		}

		internal static global::System.Collections.Generic.List<global::Unity.Services.Qos.Http.IDeserializable> GetNewJsonObjectResponse(global::System.Collections.Generic.List<object> o)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(o?, (global::System.Func<object, global::Unity.Services.Qos.Http.IDeserializable>)((object v) => new global::Unity.Services.Qos.Http.JsonObject(v))));
		}

		internal static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Unity.Services.Qos.Http.IDeserializable>> GetNewJsonObjectResponse(global::System.Collections.Generic.List<global::System.Collections.Generic.List<object>> o)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(o?, (global::System.Collections.Generic.List<object> l) => global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select((global::System.Collections.Generic.IEnumerable<object>)l, (global::System.Func<object, global::Unity.Services.Qos.Http.IDeserializable>)((object v) => (v != null) ? new global::Unity.Services.Qos.Http.JsonObject(v) : null)))));
		}

		internal static global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Qos.Http.IDeserializable> GetNewJsonObjectResponse(global::System.Collections.Generic.Dictionary<string, object> o)
		{
			return global::System.Linq.Enumerable.ToDictionary(o?, (global::System.Func<global::System.Collections.Generic.KeyValuePair<string, object>, string>)((global::System.Collections.Generic.KeyValuePair<string, object> kv) => kv.Key), (global::System.Func<global::System.Collections.Generic.KeyValuePair<string, object>, global::Unity.Services.Qos.Http.IDeserializable>)((global::System.Collections.Generic.KeyValuePair<string, object> kv) => new global::Unity.Services.Qos.Http.JsonObject(kv.Value)));
		}

		internal static global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.Services.Qos.Http.IDeserializable>> GetNewJsonObjectResponse(global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<object>> o)
		{
			return global::System.Linq.Enumerable.ToDictionary(o?, (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<object>> kv) => kv.Key, (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<object>> kv) => GetNewJsonObjectResponse(kv.Value));
		}

		private global::System.Collections.Generic.List<string> ValidateObject<T>(T objectToCheck, global::System.Collections.Generic.List<string> errors = null)
		{
			if (errors == null)
			{
				errors = new global::System.Collections.Generic.List<string>();
			}
			if (objectToCheck != null)
			{
				if (typeof(global::System.Collections.IEnumerable).IsAssignableFrom(typeof(T)))
				{
					foreach (object item in (global::System.Collections.IEnumerable)(object)objectToCheck)
					{
						ValidateFieldInfos(item, errors);
						ValidatePropertyInfos(item, errors);
					}
				}
				else
				{
					ValidateFieldInfos(objectToCheck, errors);
					ValidatePropertyInfos(objectToCheck, errors);
				}
			}
			return errors;
		}

		private void ValidatePropertyInfos<T>(T objectToCheck, global::System.Collections.Generic.List<string> errors)
		{
			global::System.Reflection.PropertyInfo[] properties = objectToCheck.GetType().GetProperties();
			foreach (global::System.Reflection.PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.GetIndexParameters().Length != 0)
				{
					for (int j = 0; j < propertyInfo.GetIndexParameters().Length; j++)
					{
						object value = propertyInfo.GetValue(objectToCheck, new object[1] { j });
						string name = propertyInfo.Name;
						string name2 = objectToCheck.GetType().Name;
						ValidateValue(value, name2, "Property", name, errors);
					}
				}
				else
				{
					object value2 = propertyInfo.GetValue(objectToCheck);
					string name3 = propertyInfo.Name;
					string name4 = objectToCheck.GetType().Name;
					ValidateValue(value2, name4, "Property", name3, errors);
				}
			}
		}

		private void ValidateFieldInfos<T>(T objectToCheck, global::System.Collections.Generic.List<string> errors)
		{
			global::System.Reflection.FieldInfo[] fields = objectToCheck.GetType().GetFields();
			foreach (global::System.Reflection.FieldInfo obj in fields)
			{
				object value = obj.GetValue(objectToCheck);
				string name = obj.Name;
				string name2 = objectToCheck.GetType().Name;
				ValidateValue(value, name2, "Field", name, errors);
			}
		}

		private void ValidateValue(object value, string objectName, string memberType, string memberName, global::System.Collections.Generic.List<string> errors)
		{
			if (!(value is global::System.ValueType) && !(value is string))
			{
				if (value is global::Newtonsoft.Json.Linq.JObject)
				{
					errors.Add(memberType + ": \"" + memberName + "\" on Type: \"" + objectName + "\" must not be of type `object` or `dynamic`");
				}
				else
				{
					ValidateObject(value, errors);
				}
			}
		}
	}
}
