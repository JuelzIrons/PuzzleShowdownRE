namespace UnityEngine.InputSystem
{
	public abstract class InputProcessor
	{
		public enum CachingPolicy
		{
			CacheResult = 0,
			EvaluateOnEveryRead = 1
		}

		internal static global::UnityEngine.InputSystem.Utilities.TypeTable s_Processors;

		public virtual global::UnityEngine.InputSystem.InputProcessor.CachingPolicy cachingPolicy => global::UnityEngine.InputSystem.InputProcessor.CachingPolicy.CacheResult;

		public abstract object ProcessAsObject(object value, global::UnityEngine.InputSystem.InputControl control);

		public unsafe abstract void Process(void* buffer, int bufferSize, global::UnityEngine.InputSystem.InputControl control);

		internal static global::System.Type GetValueTypeFromType(global::System.Type processorType)
		{
			if (processorType == null)
			{
				throw new global::System.ArgumentNullException("processorType");
			}
			return global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetGenericTypeArgumentFromHierarchy(processorType, typeof(global::UnityEngine.InputSystem.InputProcessor<>), 0);
		}
	}
	public abstract class InputProcessor<TValue> : global::UnityEngine.InputSystem.InputProcessor where TValue : struct
	{
		public abstract TValue Process(TValue value, global::UnityEngine.InputSystem.InputControl control);

		public override object ProcessAsObject(object value, global::UnityEngine.InputSystem.InputControl control)
		{
			if (value == null)
			{
				throw new global::System.ArgumentNullException("value");
			}
			if (!(value is TValue value2))
			{
				throw new global::System.ArgumentException($"Expecting value of type '{typeof(TValue).Name}' but got value '{value}' of type '{value.GetType().Name}'", "value");
			}
			return Process(value2, control);
		}

		public unsafe override void Process(void* buffer, int bufferSize, global::UnityEngine.InputSystem.InputControl control)
		{
			if (buffer == null)
			{
				throw new global::System.ArgumentNullException("buffer");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new global::System.ArgumentException($"Expected buffer of at least {num} bytes but got buffer with just {bufferSize} bytes", "bufferSize");
			}
			TValue output = default(TValue);
			void* destination = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, buffer, num);
			output = Process(output, control);
			destination = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer, destination, num);
		}
	}
}
