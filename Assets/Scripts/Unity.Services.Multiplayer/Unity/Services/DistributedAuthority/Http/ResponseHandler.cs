namespace Unity.Services.DistributedAuthority.Http
{
	internal static class ResponseHandler
	{
		private static global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Http.IDeserializable> DeserializeListOfJsonObjects(global::System.Collections.Generic.List<object> objectList)
		{
			global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Http.IDeserializable> list = new global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Http.IDeserializable>();
			foreach (object @object in objectList)
			{
				list.Add(new global::Unity.Services.DistributedAuthority.Http.JsonObject(@object));
			}
			return list;
		}

		public static T TryDeserializeResponse<T>(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore,
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			};
			try
			{
				string deserializedJson = GetDeserializedJson(response.Data);
				return (deserializedJson == null) ? default(T) : global::Unity.Services.DistributedAuthority.Http.IsolatedJsonConvert.DeserializeObject<T>(deserializedJson, settings);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
		}

		public static object TryDeserializeResponse(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response, global::System.Type type)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore,
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			};
			try
			{
				string deserializedJson = GetDeserializedJson(response.Data);
				return (deserializedJson == null) ? null : global::Unity.Services.DistributedAuthority.Http.IsolatedJsonConvert.DeserializeObject(deserializedJson, type, settings);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
		}

		private static string GetDeserializedJson(byte[] data)
		{
			if (data != null)
			{
				return global::System.Text.Encoding.UTF8.GetString(data);
			}
			return null;
		}

		public static void HandleAsyncResponse(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response, global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap)
		{
			if (statusCodeToTypeMap.ContainsKey(response.StatusCode.ToString()))
			{
				global::System.Type type = statusCodeToTypeMap[response.StatusCode.ToString()];
				if ((type != null && response.IsHttpError) || response.IsNetworkError)
				{
					if (typeof(global::Unity.Services.DistributedAuthority.Models.IOneOf).IsAssignableFrom(type))
					{
						throw CreateOneOfException(response, type);
					}
					throw CreateHttpException(response, type);
				}
				return;
			}
			throw new global::Unity.Services.DistributedAuthority.Http.HttpException(response);
		}

		private static global::Unity.Services.DistributedAuthority.Http.HttpException CreateOneOfException(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response, global::System.Type responseType)
		{
			try
			{
				object obj = TryDeserializeResponse(response, responseType);
				return CreateHttpException(response, ((global::Unity.Services.DistributedAuthority.Models.IOneOf)obj).Type);
			}
			catch (global::System.ArgumentException ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
			catch (global::System.MissingFieldException inner)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, inner, "Discriminator field not found in the parsed json response.");
			}
			catch (global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException ex2)
			{
				if (ex2.InnerException.GetType() == typeof(global::System.MissingFieldException))
				{
					throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex2.InnerException, "Discriminator field not found in the parsed json response.");
				}
				if (ex2.response == null)
				{
					throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex2.Message);
				}
				throw;
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex3, ex3.Message);
			}
		}

		private static global::Unity.Services.DistributedAuthority.Http.HttpException CreateHttpException(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response, global::System.Type responseType)
		{
			global::System.Type type = typeof(global::Unity.Services.DistributedAuthority.Http.HttpException<>).MakeGenericType(responseType);
			try
			{
				if (responseType == typeof(global::System.IO.Stream))
				{
					object obj = ((response.Data == null) ? new global::System.IO.MemoryStream() : new global::System.IO.MemoryStream(response.Data));
					return (global::Unity.Services.DistributedAuthority.Http.HttpException)global::System.Activator.CreateInstance(type, response, obj);
				}
				object obj2 = TryDeserializeResponse(response, responseType);
				return (global::Unity.Services.DistributedAuthority.Http.HttpException)global::System.Activator.CreateInstance(type, response, obj2);
			}
			catch (global::System.ArgumentException ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
			catch (global::System.MissingFieldException inner)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, inner, "Discriminator field not found in the parsed json response.");
			}
			catch (global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException ex2)
			{
				if (ex2.response == null)
				{
					throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex2.Message);
				}
				throw;
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex3, ex3.Message);
			}
		}

		public static T HandleAsyncResponse<T>(global::Unity.Services.DistributedAuthority.Http.HttpClientResponse response, global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap) where T : class
		{
			HandleAsyncResponse(response, statusCodeToTypeMap);
			try
			{
				if (statusCodeToTypeMap[response.StatusCode.ToString()] == typeof(string))
				{
					return ((response.Data == null) ? null : global::System.Text.Encoding.UTF8.GetString(response.Data)) as T;
				}
				if (statusCodeToTypeMap[response.StatusCode.ToString()] == typeof(global::System.IO.Stream))
				{
					return ((response.Data == null) ? new global::System.IO.MemoryStream() : new global::System.IO.MemoryStream(response.Data)) as T;
				}
				return TryDeserializeResponse<T>(response);
			}
			catch (global::System.ArgumentException ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex.Message);
			}
			catch (global::System.MissingFieldException inner)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, inner, "Discriminator field not found in the parsed json response.");
			}
			catch (global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException ex2)
			{
				if (ex2.response == null)
				{
					throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex2.Message);
				}
				throw;
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.DistributedAuthority.Http.ResponseDeserializationException(response, ex3, ex3.Message);
			}
		}
	}
}
