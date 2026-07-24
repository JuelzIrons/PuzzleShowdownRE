namespace Unity.Netcode
{
	internal class FallbackSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<T>
	{
		private void ThrowArgumentError()
		{
			throw new global::System.ArgumentException("Serialization has not been generated for type " + typeof(T).FullName + ". This can be addressed by adding a [GenerateSerializationForGenericParameterAttribute] to your generic class that serializes this value (if you are using one), adding [GenerateSerializationForTypeAttribute(typeof(" + typeof(T).FullName + ")] to the class or method that is attempting to serialize it, or creating a field on a NetworkBehaviour of type NetworkVariable. If this error continues to appear after doing one of those things and this is a type you can change, then either implement INetworkSerializable or mark it as serializable by memcpy by adding INetworkSerializeByMemcpy to its interface list to enable automatic serialization generation. If not, assign serialization code to UserNetworkVariableSerialization.WriteValue, UserNetworkVariableSerialization.ReadValue, and UserNetworkVariableSerialization.DuplicateValue, or if it's serializable by memcpy (contains no pointers), wrap it in " + typeof(global::Unity.Netcode.ForceNetworkSerializeByMemcpy<>).Name + ".");
		}

		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValue == null)
			{
				ThrowArgumentError();
			}
			global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue(writer, in value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValue == null)
			{
				ThrowArgumentError();
			}
			global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref T value, ref T previousValue)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValue == null)
			{
				ThrowArgumentError();
			}
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDelta == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDelta == null)
			{
				global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue(writer, in value);
			}
			else
			{
				global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDelta(writer, in value, in previousValue);
			}
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValue == null)
			{
				ThrowArgumentError();
			}
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDelta == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDelta == null)
			{
				global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue(reader, out value);
			}
			else
			{
				global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDelta(reader, ref value);
			}
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out T value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in T value, ref T duplicatedValue)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValue == null || global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValue == null)
			{
				ThrowArgumentError();
			}
			global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValue(in value, ref duplicatedValue);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.Duplicate(in T value, ref T duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
