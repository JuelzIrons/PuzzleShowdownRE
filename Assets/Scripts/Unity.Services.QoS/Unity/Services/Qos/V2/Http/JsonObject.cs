namespace Unity.Services.Qos.V2.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Qos.V2.Http.JsonObjectConverter))]
	internal class JsonObject : global::Unity.Services.Qos.V2.Http.IDeserializable
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
				return global::Unity.Services.Qos.V2.Http.IsolatedJsonConvert.SerializeObject(obj);
			}
			catch (global::System.Exception)
			{
				throw new global::System.InvalidOperationException("Failed to convert JsonObject to string.");
			}
		}

		public T GetAs<T>(global::Unity.Services.Qos.V2.Http.DeserializationSettings deserializationSettings = null)
		{
			deserializationSettings = deserializationSettings ?? new global::Unity.Services.Qos.V2.Http.DeserializationSettings();
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = ((deserializationSettings.MissingMemberHandling == global::Unity.Services.Qos.V2.Http.MissingMemberHandling.Error) ? global::Newtonsoft.Json.MissingMemberHandling.Error : global::Newtonsoft.Json.MissingMemberHandling.Ignore)
			};
			try
			{
				return global::Unity.Services.Qos.V2.Http.IsolatedJsonConvert.DeserializeObject<T>(global::Unity.Services.Qos.V2.Http.IsolatedJsonConvert.SerializeObject(obj), settings);
			}
			catch (global::Newtonsoft.Json.JsonSerializationException ex)
			{
				throw new global::Unity.Services.Qos.V2.Http.DeserializationException(ex.Message);
			}
			catch (global::System.Exception)
			{
				throw new global::Unity.Services.Qos.V2.Http.DeserializationException("Unable to deserialize object.");
			}
		}

		public T GetAs<T>()
		{
			return GetAs<T>(null);
		}

		internal static global::Unity.Services.Qos.V2.Http.IDeserializable GetNewJsonObjectResponse(object o)
		{
			return new global::Unity.Services.Qos.V2.Http.JsonObject(o);
		}

		internal static global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Http.IDeserializable> GetNewJsonObjectResponse(global::System.Collections.Generic.List<object> o)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(o?, (global::System.Func<object, global::Unity.Services.Qos.V2.Http.IDeserializable>)((object v) => new global::Unity.Services.Qos.V2.Http.JsonObject(v))));
		}

		internal static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Http.IDeserializable>> GetNewJsonObjectResponse(global::System.Collections.Generic.List<global::System.Collections.Generic.List<object>> o)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(o?, (global::System.Collections.Generic.List<object> l) => global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select((global::System.Collections.Generic.IEnumerable<object>)l, (global::System.Func<object, global::Unity.Services.Qos.V2.Http.IDeserializable>)((object v) => (v != null) ? new global::Unity.Services.Qos.V2.Http.JsonObject(v) : null)))));
		}

		internal static global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Qos.V2.Http.IDeserializable> GetNewJsonObjectResponse(global::System.Collections.Generic.Dictionary<string, object> o)
		{
			return global::System.Linq.Enumerable.ToDictionary(o?, (global::System.Func<global::System.Collections.Generic.KeyValuePair<string, object>, string>)((global::System.Collections.Generic.KeyValuePair<string, object> kv) => kv.Key), (global::System.Func<global::System.Collections.Generic.KeyValuePair<string, object>, global::Unity.Services.Qos.V2.Http.IDeserializable>)((global::System.Collections.Generic.KeyValuePair<string, object> kv) => new global::Unity.Services.Qos.V2.Http.JsonObject(kv.Value)));
		}

		internal static global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Http.IDeserializable>> GetNewJsonObjectResponse(global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<object>> o)
		{
			return global::System.Linq.Enumerable.ToDictionary(o?, (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<object>> kv) => kv.Key, (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<object>> kv) => GetNewJsonObjectResponse(kv.Value));
		}
	}
}
