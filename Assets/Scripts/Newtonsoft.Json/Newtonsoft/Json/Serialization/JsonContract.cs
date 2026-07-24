namespace Newtonsoft.Json.Serialization
{
	public abstract class JsonContract
	{
		internal bool IsNullable;

		internal bool IsConvertable;

		internal bool IsEnum;

		internal global::System.Type NonNullableUnderlyingType;

		internal global::Newtonsoft.Json.ReadType InternalReadType;

		internal global::Newtonsoft.Json.Serialization.JsonContractType ContractType;

		internal bool IsReadOnlyOrFixedSize;

		internal bool IsSealed;

		internal bool IsInstantiable;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? _onDeserializedCallbacks;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? _onDeserializingCallbacks;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? _onSerializedCallbacks;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? _onSerializingCallbacks;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationErrorCallback>? _onErrorCallbacks;

		private global::System.Type _createdType;

		public global::System.Type UnderlyingType { get; }

		public global::System.Type CreatedType
		{
			get
			{
				return _createdType;
			}
			set
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				_createdType = value;
				IsSealed = global::Newtonsoft.Json.Utilities.TypeExtensions.IsSealed(_createdType);
				IsInstantiable = !global::Newtonsoft.Json.Utilities.TypeExtensions.IsInterface(_createdType) && !global::Newtonsoft.Json.Utilities.TypeExtensions.IsAbstract(_createdType);
			}
		}

		public bool? IsReference { get; set; }

		public global::Newtonsoft.Json.JsonConverter? Converter { get; set; }

		public global::Newtonsoft.Json.JsonConverter? InternalConverter { get; internal set; }

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (_onDeserializedCallbacks == null)
				{
					_onDeserializedCallbacks = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
				}
				return _onDeserializedCallbacks;
			}
		}

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (_onDeserializingCallbacks == null)
				{
					_onDeserializingCallbacks = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
				}
				return _onDeserializingCallbacks;
			}
		}

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (_onSerializedCallbacks == null)
				{
					_onSerializedCallbacks = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
				}
				return _onSerializedCallbacks;
			}
		}

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (_onSerializingCallbacks == null)
				{
					_onSerializingCallbacks = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
				}
				return _onSerializingCallbacks;
			}
		}

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (_onErrorCallbacks == null)
				{
					_onErrorCallbacks = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationErrorCallback>();
				}
				return _onErrorCallbacks;
			}
		}

		public global::System.Func<object>? DefaultCreator { get; set; }

		public bool DefaultCreatorNonPublic { get; set; }

		internal JsonContract(global::System.Type underlyingType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			UnderlyingType = underlyingType;
			underlyingType = global::Newtonsoft.Json.Utilities.ReflectionUtils.EnsureNotByRefType(underlyingType);
			IsNullable = global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(underlyingType);
			NonNullableUnderlyingType = ((IsNullable && global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(underlyingType)) ? global::System.Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			_createdType = (CreatedType = NonNullableUnderlyingType);
			IsConvertable = global::Newtonsoft.Json.Utilities.ConvertUtils.IsConvertible(NonNullableUnderlyingType);
			IsEnum = global::Newtonsoft.Json.Utilities.TypeExtensions.IsEnum(NonNullableUnderlyingType);
			InternalReadType = global::Newtonsoft.Json.ReadType.Read;
		}

		internal void InvokeOnSerializing(object o, global::System.Runtime.Serialization.StreamingContext context)
		{
			if (_onSerializingCallbacks == null)
			{
				return;
			}
			foreach (global::Newtonsoft.Json.Serialization.SerializationCallback onSerializingCallback in _onSerializingCallbacks)
			{
				onSerializingCallback(o, context);
			}
		}

		internal void InvokeOnSerialized(object o, global::System.Runtime.Serialization.StreamingContext context)
		{
			if (_onSerializedCallbacks == null)
			{
				return;
			}
			foreach (global::Newtonsoft.Json.Serialization.SerializationCallback onSerializedCallback in _onSerializedCallbacks)
			{
				onSerializedCallback(o, context);
			}
		}

		internal void InvokeOnDeserializing(object o, global::System.Runtime.Serialization.StreamingContext context)
		{
			if (_onDeserializingCallbacks == null)
			{
				return;
			}
			foreach (global::Newtonsoft.Json.Serialization.SerializationCallback onDeserializingCallback in _onDeserializingCallbacks)
			{
				onDeserializingCallback(o, context);
			}
		}

		internal void InvokeOnDeserialized(object o, global::System.Runtime.Serialization.StreamingContext context)
		{
			if (_onDeserializedCallbacks == null)
			{
				return;
			}
			foreach (global::Newtonsoft.Json.Serialization.SerializationCallback onDeserializedCallback in _onDeserializedCallbacks)
			{
				onDeserializedCallback(o, context);
			}
		}

		internal void InvokeOnError(object o, global::System.Runtime.Serialization.StreamingContext context, global::Newtonsoft.Json.Serialization.ErrorContext errorContext)
		{
			if (_onErrorCallbacks == null)
			{
				return;
			}
			foreach (global::Newtonsoft.Json.Serialization.SerializationErrorCallback onErrorCallback in _onErrorCallbacks)
			{
				onErrorCallback(o, context, errorContext);
			}
		}

		internal static global::Newtonsoft.Json.Serialization.SerializationCallback CreateSerializationCallback(global::System.Reflection.MethodInfo callbackMethodInfo)
		{
			return delegate(object o, global::System.Runtime.Serialization.StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[1] { context });
			};
		}

		internal static global::Newtonsoft.Json.Serialization.SerializationErrorCallback CreateSerializationErrorCallback(global::System.Reflection.MethodInfo callbackMethodInfo)
		{
			return delegate(object o, global::System.Runtime.Serialization.StreamingContext context, global::Newtonsoft.Json.Serialization.ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[2] { context, econtext });
			};
		}
	}
}
