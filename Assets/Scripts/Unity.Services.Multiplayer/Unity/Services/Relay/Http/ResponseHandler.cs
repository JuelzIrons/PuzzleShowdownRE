namespace Unity.Services.Relay.Http
{
	internal static class ResponseHandler
	{
		private static global::System.Collections.Generic.List<global::Unity.Services.Relay.Http.IDeserializable> DeserializeListOfJsonObjects(global::System.Collections.Generic.List<object> objectList)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Relay.Http.IDeserializable> list = new global::System.Collections.Generic.List<global::Unity.Services.Relay.Http.IDeserializable>();
			foreach (object @object in objectList)
			{
				list.Add(new global::Unity.Services.Relay.Http.JsonObject(@object));
			}
			return list;
		}

		public static T TryDeserializeResponse<T>(global::Unity.Services.Relay.Http.HttpClientResponse response)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore,
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			};
			try
			{
				return global::Newtonsoft.Json.JsonConvert.DeserializeObject<T>(GetDeserializedJson(response.Data), settings);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
		}

		public static object TryDeserializeResponse(global::Unity.Services.Relay.Http.HttpClientResponse response, global::System.Type type)
		{
			global::Newtonsoft.Json.JsonSerializerSettings settings = new global::Newtonsoft.Json.JsonSerializerSettings
			{
				MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore,
				ReferenceLoopHandling = global::Newtonsoft.Json.ReferenceLoopHandling.Ignore
			};
			try
			{
				return global::Newtonsoft.Json.JsonConvert.DeserializeObject(GetDeserializedJson(response.Data), type, settings);
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
		}

		private static string GetDeserializedJson(byte[] data)
		{
			return global::System.Text.Encoding.UTF8.GetString(data);
		}

		public static void HandleAsyncResponse(global::Unity.Services.Relay.Http.HttpClientResponse response, global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap)
		{
			if (statusCodeToTypeMap.ContainsKey(response.StatusCode.ToString()))
			{
				global::System.Type type = statusCodeToTypeMap[response.StatusCode.ToString()];
				if ((type != null && response.IsHttpError) || response.IsNetworkError)
				{
					if (typeof(global::Unity.Services.Relay.Models.IOneOf).IsAssignableFrom(type))
					{
						throw CreateOneOfException(response, type);
					}
					throw CreateHttpException(response, type);
				}
				return;
			}
			throw new global::Unity.Services.Relay.Http.HttpException(response);
		}

		private static global::Unity.Services.Relay.Http.HttpException CreateOneOfException(global::Unity.Services.Relay.Http.HttpClientResponse response, global::System.Type responseType)
		{
			try
			{
				object obj = TryDeserializeResponse(response, responseType);
				return CreateHttpException(response, ((global::Unity.Services.Relay.Models.IOneOf)obj).Type);
			}
			catch (global::System.ArgumentException ex)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
			catch (global::System.MissingFieldException inner)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, inner, "Discriminator field not found in the parsed json response.");
			}
			catch (global::Unity.Services.Relay.Http.ResponseDeserializationException ex2)
			{
				if (ex2.InnerException.GetType() == typeof(global::System.MissingFieldException))
				{
					throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex2.InnerException, "Discriminator field not found in the parsed json response.");
				}
				if (ex2.response == null)
				{
					throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex2.Message);
				}
				throw;
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex3, ex3.Message);
			}
		}

		private static global::Unity.Services.Relay.Http.HttpException CreateHttpException(global::Unity.Services.Relay.Http.HttpClientResponse response, global::System.Type responseType)
		{
			global::System.Type type = typeof(global::Unity.Services.Relay.Http.HttpException<>).MakeGenericType(responseType);
			try
			{
				if (responseType == typeof(global::System.IO.Stream))
				{
					object obj = ((response.Data == null) ? new global::System.IO.MemoryStream() : new global::System.IO.MemoryStream(response.Data));
					return (global::Unity.Services.Relay.Http.HttpException)global::System.Activator.CreateInstance(type, response, obj);
				}
				object obj2 = TryDeserializeResponse(response, responseType);
				return (global::Unity.Services.Relay.Http.HttpException)global::System.Activator.CreateInstance(type, response, obj2);
			}
			catch (global::System.ArgumentException ex)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex, ex.Message);
			}
			catch (global::System.MissingFieldException inner)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, inner, "Discriminator field not found in the parsed json response.");
			}
			catch (global::Unity.Services.Relay.Http.ResponseDeserializationException ex2)
			{
				if (ex2.response == null)
				{
					throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex2.Message);
				}
				throw;
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex3, ex3.Message);
			}
		}

		public static T HandleAsyncResponse<T>(global::Unity.Services.Relay.Http.HttpClientResponse response, global::System.Collections.Generic.Dictionary<string, global::System.Type> statusCodeToTypeMap) where T : class
		{
			HandleAsyncResponse(response, statusCodeToTypeMap);
			try
			{
				if (statusCodeToTypeMap[response.StatusCode.ToString()] == typeof(global::System.IO.Stream))
				{
					return ((response.Data == null) ? new global::System.IO.MemoryStream() : new global::System.IO.MemoryStream(response.Data)) as T;
				}
				return TryDeserializeResponse<T>(response);
			}
			catch (global::System.ArgumentException ex)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex.Message);
			}
			catch (global::System.MissingFieldException inner)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, inner, "Discriminator field not found in the parsed json response.");
			}
			catch (global::Unity.Services.Relay.Http.ResponseDeserializationException ex2)
			{
				if (ex2.response == null)
				{
					throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex2.Message);
				}
				throw;
			}
			catch (global::System.Exception ex3)
			{
				throw new global::Unity.Services.Relay.Http.ResponseDeserializationException(response, ex3, ex3.Message);
			}
		}
	}
}
