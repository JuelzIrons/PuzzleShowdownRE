namespace Unity.VisualScripting
{
	public static class Serialization
	{
		public const string ConstructorWarning = "This parameterless constructor is only made public for serialization. Use another constructor instead.";

		private static readonly global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.SerializationOperation> freeOperations;

		private static readonly global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.SerializationOperation> busyOperations;

		private static readonly object @lock;

		private static readonly global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.ISerializationDepender> awaitingDependers;

		public static bool isUnitySerializing { get; set; }

		public static bool isCustomSerializing => busyOperations.Count > 0;

		public static bool isSerializing
		{
			get
			{
				if (!isUnitySerializing)
				{
					return isCustomSerializing;
				}
				return true;
			}
		}

		static Serialization()
		{
			@lock = new object();
			awaitingDependers = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.ISerializationDepender>();
			freeOperations = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.SerializationOperation>();
			busyOperations = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.SerializationOperation>();
		}

		private static global::Unity.VisualScripting.SerializationOperation StartOperation()
		{
			lock (@lock)
			{
				if (freeOperations.Count == 0)
				{
					freeOperations.Add(new global::Unity.VisualScripting.SerializationOperation());
				}
				global::Unity.VisualScripting.SerializationOperation serializationOperation = global::System.Linq.Enumerable.First(freeOperations);
				freeOperations.Remove(serializationOperation);
				busyOperations.Add(serializationOperation);
				return serializationOperation;
			}
		}

		private static void EndOperation(global::Unity.VisualScripting.SerializationOperation operation)
		{
			lock (@lock)
			{
				if (!busyOperations.Contains(operation))
				{
					throw new global::System.InvalidOperationException("Trying to finish an operation that isn't started.");
				}
				operation.Reset();
				busyOperations.Remove(operation);
				freeOperations.Add(operation);
			}
		}

		public static T CloneViaSerialization<T>(this T value, bool forceReflected = false)
		{
			return (T)value.Serialize(forceReflected).Deserialize(forceReflected);
		}

		public static void CloneViaSerializationInto<TSource, TDestination>(this TSource value, ref TDestination instance, bool forceReflected = false) where TDestination : TSource
		{
			object instance2 = instance;
			value.Serialize(forceReflected).DeserializeInto(ref instance2, forceReflected);
		}

		public static global::Unity.VisualScripting.SerializationData Serialize(this object value, bool forceReflected = false)
		{
			global::Unity.VisualScripting.SerializationOperation serializationOperation = StartOperation();
			try
			{
				string json = SerializeJson(serializationOperation.serializer, value, forceReflected);
				global::UnityEngine.Object[] objectReferences = serializationOperation.objectReferences.ToArray();
				return new global::Unity.VisualScripting.SerializationData(json, objectReferences);
			}
			catch (global::System.Exception innerException)
			{
				throw new global::System.Runtime.Serialization.SerializationException("Serialization of '" + (value?.GetType().ToString() ?? "null") + "' failed.", innerException);
			}
			finally
			{
				EndOperation(serializationOperation);
			}
		}

		public static void DeserializeInto(this global::Unity.VisualScripting.SerializationData data, ref object instance, bool forceReflected = false)
		{
			try
			{
				if (string.IsNullOrEmpty(data.json))
				{
					instance = null;
					return;
				}
				global::Unity.VisualScripting.SerializationOperation serializationOperation = StartOperation();
				try
				{
					serializationOperation.objectReferences.AddRange(data.objectReferences);
					DeserializeJson(serializationOperation.serializer, data.json, ref instance, forceReflected);
				}
				finally
				{
					EndOperation(serializationOperation);
				}
			}
			catch (global::System.Exception innerException)
			{
				try
				{
					global::UnityEngine.Debug.LogWarning(data.ToString("Deserialization Failure Data"), instance as global::UnityEngine.Object);
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogWarning("Failed to log deserialization failure data:\n" + ex, instance as global::UnityEngine.Object);
				}
				throw new global::System.Runtime.Serialization.SerializationException("Deserialization into '" + (instance?.GetType().ToString() ?? "null") + "' failed.", innerException);
			}
		}

		public static object Deserialize(this global::Unity.VisualScripting.SerializationData data, bool forceReflected = false)
		{
			object instance = null;
			data.DeserializeInto(ref instance, forceReflected);
			return instance;
		}

		private static string SerializeJson(global::Unity.VisualScripting.FullSerializer.fsSerializer serializer, object instance, bool forceReflected)
		{
			using (global::Unity.VisualScripting.ProfilingUtility.SampleBlock("SerializeJson"))
			{
				global::Unity.VisualScripting.FullSerializer.fsData data;
				global::Unity.VisualScripting.FullSerializer.fsResult result = ((!forceReflected) ? serializer.TrySerialize(instance, out data) : serializer.TrySerialize(instance.GetType(), typeof(global::Unity.VisualScripting.FullSerializer.fsReflectedConverter), instance, out data));
				HandleResult("Serialization", result, instance as global::UnityEngine.Object);
				return global::Unity.VisualScripting.FullSerializer.fsJsonPrinter.CompressedJson(data);
			}
		}

		private static global::Unity.VisualScripting.FullSerializer.fsResult DeserializeJsonUtil(global::Unity.VisualScripting.FullSerializer.fsSerializer serializer, string json, ref object instance, bool forceReflected)
		{
			global::Unity.VisualScripting.FullSerializer.fsData data = global::Unity.VisualScripting.FullSerializer.fsJsonParser.Parse(json);
			if (forceReflected)
			{
				return serializer.TryDeserialize(data, instance.GetType(), typeof(global::Unity.VisualScripting.FullSerializer.fsReflectedConverter), ref instance);
			}
			return serializer.TryDeserialize(data, ref instance);
		}

		private static void DeserializeJson(global::Unity.VisualScripting.FullSerializer.fsSerializer serializer, string json, ref object instance, bool forceReflected)
		{
			using (global::Unity.VisualScripting.ProfilingUtility.SampleBlock("DeserializeJson"))
			{
				global::Unity.VisualScripting.FullSerializer.fsResult result = DeserializeJsonUtil(serializer, json, ref instance, forceReflected);
				HandleResult("Deserialization", result, instance as global::UnityEngine.Object);
			}
		}

		private static void HandleResult(string label, global::Unity.VisualScripting.FullSerializer.fsResult result, global::UnityEngine.Object context = null)
		{
			result.AssertSuccess();
			if (!result.HasWarnings)
			{
				return;
			}
			foreach (string rawMessage in result.RawMessages)
			{
				global::UnityEngine.Debug.LogWarning("[" + label + "] " + rawMessage + "\n", context);
			}
		}

		public static string PrettyPrint(string json)
		{
			return global::Unity.VisualScripting.FullSerializer.fsJsonPrinter.PrettyJson(global::Unity.VisualScripting.FullSerializer.fsJsonParser.Parse(json));
		}

		public static void AwaitDependencies(global::Unity.VisualScripting.ISerializationDepender depender)
		{
			awaitingDependers.Add(depender);
			CheckIfDependenciesMet(depender);
		}

		public static void NotifyDependencyDeserializing(global::Unity.VisualScripting.ISerializationDependency dependency)
		{
			NotifyDependencyUnavailable(dependency);
		}

		public static void NotifyDependencyDeserialized(global::Unity.VisualScripting.ISerializationDependency dependency)
		{
			NotifyDependencyAvailable(dependency);
		}

		public static void NotifyDependencyUnavailable(global::Unity.VisualScripting.ISerializationDependency dependency)
		{
			dependency.IsDeserialized = false;
		}

		public static void NotifyDependencyAvailable(global::Unity.VisualScripting.ISerializationDependency dependency)
		{
			dependency.IsDeserialized = true;
			global::Unity.VisualScripting.ISerializationDepender[] array = global::System.Linq.Enumerable.ToArray(awaitingDependers);
			foreach (global::Unity.VisualScripting.ISerializationDepender serializationDepender in array)
			{
				if (awaitingDependers.Contains(serializationDepender))
				{
					CheckIfDependenciesMet(serializationDepender);
				}
			}
		}

		private static void CheckIfDependenciesMet(global::Unity.VisualScripting.ISerializationDepender depender)
		{
			bool flag = true;
			foreach (global::Unity.VisualScripting.ISerializationDependency deserializationDependency in depender.deserializationDependencies)
			{
				if (!deserializationDependency.IsDeserialized)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				awaitingDependers.Remove(depender);
				depender.OnAfterDependenciesDeserialized();
			}
		}

		public static void LogStuckDependers()
		{
			if (global::System.Linq.Enumerable.Any(awaitingDependers))
			{
				string text = awaitingDependers.Count + " awaiting dependers: \n";
				foreach (global::Unity.VisualScripting.ISerializationDepender awaitingDepender in awaitingDependers)
				{
					global::System.Collections.Generic.HashSet<object> hashSet = new global::System.Collections.Generic.HashSet<object>();
					foreach (global::Unity.VisualScripting.ISerializationDependency deserializationDependency in awaitingDepender.deserializationDependencies)
					{
						if (!deserializationDependency.IsDeserialized)
						{
							hashSet.Add(deserializationDependency);
							break;
						}
					}
					text += $"{awaitingDepender} is missing {hashSet.ToCommaSeparatedString()}\n";
				}
				global::UnityEngine.Debug.LogWarning(text);
			}
			else
			{
				global::UnityEngine.Debug.Log("No stuck awaiting depender.");
			}
		}
	}
}
