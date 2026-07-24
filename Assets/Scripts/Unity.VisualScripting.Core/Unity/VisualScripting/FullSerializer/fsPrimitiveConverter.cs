namespace Unity.VisualScripting.FullSerializer
{
	public class fsPrimitiveConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			if (!global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).IsPrimitive && !(type == typeof(string)))
			{
				return type == typeof(decimal);
			}
			return true;
		}

		public override bool RequestCycleSupport(global::System.Type storageType)
		{
			return false;
		}

		public override bool RequestInheritanceSupport(global::System.Type storageType)
		{
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::System.Type type = instance.GetType();
			if (Serializer.Config.Serialize64BitIntegerAsString && (type == typeof(long) || type == typeof(ulong)))
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData((string)global::System.Convert.ChangeType(instance, typeof(string)));
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (UseBool(type))
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData((bool)instance);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (UseInt64(type))
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData((long)global::System.Convert.ChangeType(instance, typeof(long)));
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (UseDouble(type))
			{
				if (instance.GetType() == typeof(float) && (float)instance != float.MinValue && (float)instance != float.MaxValue && !float.IsInfinity((float)instance) && !float.IsNaN((float)instance))
				{
					serialized = new global::Unity.VisualScripting.FullSerializer.fsData((double)(decimal)(float)instance);
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData((double)global::System.Convert.ChangeType(instance, typeof(double)));
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (UseString(type))
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData((string)global::System.Convert.ChangeType(instance, typeof(string)));
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			serialized = null;
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unhandled primitive type " + instance.GetType());
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData storage, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			if (UseBool(storageType))
			{
				global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(storage, global::Unity.VisualScripting.FullSerializer.fsDataType.Boolean));
				if (fsResult2.Succeeded)
				{
					instance = storage.AsBool;
				}
				return success;
			}
			if (UseDouble(storageType) || UseInt64(storageType))
			{
				if (storage.IsDouble)
				{
					instance = global::System.Convert.ChangeType(storage.AsDouble, storageType);
				}
				else if (storage.IsInt64)
				{
					instance = global::System.Convert.ChangeType(storage.AsInt64, storageType);
				}
				else
				{
					if (!Serializer.Config.Serialize64BitIntegerAsString || !storage.IsString || (!(storageType == typeof(long)) && !(storageType == typeof(ulong))))
					{
						return global::Unity.VisualScripting.FullSerializer.fsResult.Fail(GetType().Name + " expected number but got " + storage.Type.ToString() + " in " + storage);
					}
					instance = global::System.Convert.ChangeType(storage.AsString, storageType);
				}
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (UseString(storageType))
			{
				global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(storage, global::Unity.VisualScripting.FullSerializer.fsDataType.String));
				if (fsResult2.Succeeded)
				{
					string asString = storage.AsString;
					if (storageType == typeof(char))
					{
						if (storageType == typeof(char))
						{
							if (asString.Length == 1)
							{
								instance = asString[0];
							}
							else
							{
								instance = '\0';
							}
						}
					}
					else
					{
						instance = asString;
					}
				}
				return success;
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail(GetType().Name + ": Bad data; expected bool, number, string, but got " + storage);
		}

		private static bool UseBool(global::System.Type type)
		{
			return type == typeof(bool);
		}

		private static bool UseInt64(global::System.Type type)
		{
			if (!(type == typeof(sbyte)) && !(type == typeof(byte)) && !(type == typeof(short)) && !(type == typeof(ushort)) && !(type == typeof(int)) && !(type == typeof(uint)) && !(type == typeof(long)))
			{
				return type == typeof(ulong);
			}
			return true;
		}

		private static bool UseDouble(global::System.Type type)
		{
			if (!(type == typeof(float)) && !(type == typeof(double)))
			{
				return type == typeof(decimal);
			}
			return true;
		}

		private static bool UseString(global::System.Type type)
		{
			if (!(type == typeof(string)))
			{
				return type == typeof(char);
			}
			return true;
		}
	}
}
