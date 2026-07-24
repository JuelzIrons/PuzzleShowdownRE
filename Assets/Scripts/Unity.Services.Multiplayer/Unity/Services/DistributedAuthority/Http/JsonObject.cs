namespace Unity.Services.DistributedAuthority.Http
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.DistributedAuthority.Http.JsonObjectConverter))]
	internal class JsonObject : global::Unity.Services.DistributedAuthority.Http.IDeserializable
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
				return global::Unity.Services.DistributedAuthority.Http.IsolatedJsonConvert.SerializeObject(obj);
			}
			catch (global::System.Exception)
			{
				throw new global::System.InvalidOperationException("Failed to convert JsonObject to string.");
			}
		}

		public T GetAs<T>(global::Unity.Services.DistributedAuthority.Http.DeserializationSettings deserializationSettings = null)
		{
			deserializationSettings = deserializationSettings ?? new global::Unity.Services.DistributedAuthority.Http.DeserializationSettings();
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = ((deserializationSettings.MissingMemberHandling == global::Unity.Services.DistributedAuthority.Http.MissingMemberHandling.Error) ? global::Newtonsoft.Json.MissingMemberHandling.Error : global::Newtonsoft.Json.MissingMemberHandling.Ignore)
			};
			try
			{
				return global::Unity.Services.DistributedAuthority.Http.IsolatedJsonConvert.DeserializeObject<T>(global::Unity.Services.DistributedAuthority.Http.IsolatedJsonConvert.SerializeObject(obj), settings);
			}
			catch (global::Newtonsoft.Json.JsonSerializationException ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.DeserializationException(ex.Message);
			}
			catch (global::System.Exception)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.DeserializationException("Unable to deserialize object.");
			}
		}

		public T GetAs<T>()
		{
			return GetAs<T>(null);
		}

		internal static global::Unity.Services.DistributedAuthority.Http.IDeserializable GetNewJsonObjectResponse(object o)
		{
			return new global::Unity.Services.DistributedAuthority.Http.JsonObject(o);
		}

		internal static global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Http.IDeserializable> GetNewJsonObjectResponse(global::System.Collections.Generic.List<object> o)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(o?, (global::System.Func<object, global::Unity.Services.DistributedAuthority.Http.IDeserializable>)((object v) => new global::Unity.Services.DistributedAuthority.Http.JsonObject(v))));
		}

		internal static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Http.IDeserializable>> GetNewJsonObjectResponse(global::System.Collections.Generic.List<global::System.Collections.Generic.List<object>> o)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(o?, (global::System.Collections.Generic.List<object> l) => global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select((global::System.Collections.Generic.IEnumerable<object>)l, (global::System.Func<object, global::Unity.Services.DistributedAuthority.Http.IDeserializable>)((object v) => (v != null) ? new global::Unity.Services.DistributedAuthority.Http.JsonObject(v) : null)))));
		}

		internal static global::System.Collections.Generic.Dictionary<string, global::Unity.Services.DistributedAuthority.Http.IDeserializable> GetNewJsonObjectResponse(global::System.Collections.Generic.Dictionary<string, object> o)
		{
			return global::System.Linq.Enumerable.ToDictionary(o?, (global::System.Func<global::System.Collections.Generic.KeyValuePair<string, object>, string>)((global::System.Collections.Generic.KeyValuePair<string, object> kv) => kv.Key), (global::System.Func<global::System.Collections.Generic.KeyValuePair<string, object>, global::Unity.Services.DistributedAuthority.Http.IDeserializable>)((global::System.Collections.Generic.KeyValuePair<string, object> kv) => new global::Unity.Services.DistributedAuthority.Http.JsonObject(kv.Value)));
		}

		internal static global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Http.IDeserializable>> GetNewJsonObjectResponse(global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<object>> o)
		{
			return global::System.Linq.Enumerable.ToDictionary(o?, (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<object>> kv) => kv.Key, (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<object>> kv) => GetNewJsonObjectResponse(kv.Value));
		}
	}
}
