namespace Newtonsoft.Json.Serialization
{
	public class DefaultSerializationBinder : global::System.Runtime.Serialization.SerializationBinder, global::Newtonsoft.Json.Serialization.ISerializationBinder
	{
		internal static readonly global::Newtonsoft.Json.Serialization.DefaultSerializationBinder Instance = new global::Newtonsoft.Json.Serialization.DefaultSerializationBinder();

		private readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::Newtonsoft.Json.Utilities.StructMultiKey<string?, string>, global::System.Type> _typeCache;

		public DefaultSerializationBinder()
		{
			_typeCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::Newtonsoft.Json.Utilities.StructMultiKey<string, string>, global::System.Type>(GetTypeFromTypeNameKey);
		}

		private global::System.Type GetTypeFromTypeNameKey(global::Newtonsoft.Json.Utilities.StructMultiKey<string?, string> typeNameKey)
		{
			string value = typeNameKey.Value1;
			string value2 = typeNameKey.Value2;
			if (value != null)
			{
				global::System.Reflection.Assembly assembly = global::System.Reflection.Assembly.LoadWithPartialName(value);
				if (assembly == null)
				{
					global::System.Reflection.Assembly[] assemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
					foreach (global::System.Reflection.Assembly assembly2 in assemblies)
					{
						if (assembly2.FullName == value || assembly2.GetName().Name == value)
						{
							assembly = assembly2;
							break;
						}
					}
				}
				if (assembly == null)
				{
					throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not load assembly '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, value));
				}
				global::System.Type type = assembly.GetType(value2);
				if (type == null)
				{
					if (global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(value2, '`') >= 0)
					{
						try
						{
							type = GetGenericTypeFromTypeName(value2, assembly);
						}
						catch (global::System.Exception innerException)
						{
							throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not find type '{0}' in assembly '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, value2, assembly.FullName), innerException);
						}
					}
					if (type == null)
					{
						throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not find type '{0}' in assembly '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, value2, assembly.FullName));
					}
				}
				return type;
			}
			return global::System.Type.GetType(value2);
		}

		private global::System.Type? GetGenericTypeFromTypeName(string typeName, global::System.Reflection.Assembly assembly)
		{
			global::System.Type result = null;
			int num = global::Newtonsoft.Json.Utilities.StringUtils.IndexOf(typeName, '[');
			if (num >= 0)
			{
				string name = typeName.Substring(0, num);
				global::System.Type type = assembly.GetType(name);
				if (type != null)
				{
					global::System.Collections.Generic.List<global::System.Type> list = new global::System.Collections.Generic.List<global::System.Type>();
					int num2 = 0;
					int num3 = 0;
					int num4 = typeName.Length - 1;
					for (int i = num + 1; i < num4; i++)
					{
						switch (typeName[i])
						{
						case '[':
							if (num2 == 0)
							{
								num3 = i + 1;
							}
							num2++;
							break;
						case ']':
							num2--;
							if (num2 == 0)
							{
								global::Newtonsoft.Json.Utilities.StructMultiKey<string, string> typeNameKey = global::Newtonsoft.Json.Utilities.ReflectionUtils.SplitFullyQualifiedTypeName(typeName.Substring(num3, i - num3));
								list.Add(GetTypeByName(typeNameKey));
							}
							break;
						}
					}
					result = type.MakeGenericType(list.ToArray());
				}
			}
			return result;
		}

		private global::System.Type GetTypeByName(global::Newtonsoft.Json.Utilities.StructMultiKey<string?, string> typeNameKey)
		{
			return _typeCache.Get(typeNameKey);
		}

		public override global::System.Type BindToType(string? assemblyName, string typeName)
		{
			return GetTypeByName(new global::Newtonsoft.Json.Utilities.StructMultiKey<string, string>(assemblyName, typeName));
		}

		public override void BindToName(global::System.Type serializedType, out string? assemblyName, out string? typeName)
		{
			assemblyName = serializedType.Assembly.FullName;
			typeName = serializedType.FullName;
		}
	}
}
