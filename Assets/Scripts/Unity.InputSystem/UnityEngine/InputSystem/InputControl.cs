namespace UnityEngine.InputSystem
{
	[global::System.Diagnostics.DebuggerDisplay("{DebuggerDisplay(),nq}")]
	public abstract class InputControl
	{
		[global::System.Flags]
		internal enum ControlFlags
		{
			ConfigUpToDate = 1,
			IsNoisy = 2,
			IsSynthetic = 4,
			IsButton = 8,
			DontReset = 0x10,
			SetupFinished = 0x20,
			UsesStateFromOtherControl = 0x40
		}

		protected internal global::UnityEngine.InputSystem.LowLevel.InputStateBlock m_StateBlock;

		internal global::UnityEngine.InputSystem.Utilities.InternedString m_Name;

		internal string m_Path;

		internal string m_DisplayName;

		internal string m_DisplayNameFromLayout;

		internal string m_ShortDisplayName;

		internal string m_ShortDisplayNameFromLayout;

		internal global::UnityEngine.InputSystem.Utilities.InternedString m_Layout;

		internal global::UnityEngine.InputSystem.Utilities.InternedString m_Variants;

		internal global::UnityEngine.InputSystem.InputDevice m_Device;

		internal global::UnityEngine.InputSystem.InputControl m_Parent;

		internal int m_UsageCount;

		internal int m_UsageStartIndex;

		internal int m_AliasCount;

		internal int m_AliasStartIndex;

		internal int m_ChildCount;

		internal int m_ChildStartIndex;

		internal global::UnityEngine.InputSystem.InputControl.ControlFlags m_ControlFlags;

		internal bool m_CachedValueIsStale = true;

		internal bool m_UnprocessedCachedValueIsStale = true;

		internal global::UnityEngine.InputSystem.Utilities.PrimitiveValue m_DefaultState;

		internal global::UnityEngine.InputSystem.Utilities.PrimitiveValue m_MinValue;

		internal global::UnityEngine.InputSystem.Utilities.PrimitiveValue m_MaxValue;

		internal global::UnityEngine.InputSystem.Utilities.FourCC m_OptimizedControlDataType;

		public string name => m_Name;

		public string displayName
		{
			get
			{
				RefreshConfigurationIfNeeded();
				if (m_DisplayName != null)
				{
					return m_DisplayName;
				}
				if (m_DisplayNameFromLayout != null)
				{
					return m_DisplayNameFromLayout;
				}
				return m_Name;
			}
			protected set
			{
				m_DisplayName = value;
			}
		}

		public string shortDisplayName
		{
			get
			{
				RefreshConfigurationIfNeeded();
				if (m_ShortDisplayName != null)
				{
					return m_ShortDisplayName;
				}
				if (m_ShortDisplayNameFromLayout != null)
				{
					return m_ShortDisplayNameFromLayout;
				}
				return null;
			}
			protected set
			{
				m_ShortDisplayName = value;
			}
		}

		public string path
		{
			get
			{
				if (m_Path == null)
				{
					m_Path = global::UnityEngine.InputSystem.InputControlPath.Combine(m_Parent, m_Name);
				}
				return m_Path;
			}
		}

		public string layout => m_Layout;

		public string variants => m_Variants;

		public global::UnityEngine.InputSystem.InputDevice device => m_Device;

		public global::UnityEngine.InputSystem.InputControl parent => m_Parent;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> children => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl>(m_Device.m_ChildrenForEachControl, m_ChildStartIndex, m_ChildCount);

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> usages => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(m_Device.m_UsagesForEachControl, m_UsageStartIndex, m_UsageCount);

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> aliases => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(m_Device.m_AliasesForEachControl, m_AliasStartIndex, m_AliasCount);

		public global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock => m_StateBlock;

		public bool noisy
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.IsNoisy) != 0;
			}
			internal set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.IsNoisy;
					global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> readOnlyArray = children;
					for (int i = 0; i < readOnlyArray.Count; i++)
					{
						if (readOnlyArray[i] != null)
						{
							readOnlyArray[i].noisy = true;
						}
					}
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.IsNoisy;
				}
			}
		}

		public bool synthetic
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.IsSynthetic) != 0;
			}
			internal set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.IsSynthetic;
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.IsSynthetic;
				}
			}
		}

		public global::UnityEngine.InputSystem.InputControl this[string path] => global::UnityEngine.InputSystem.InputControlPath.TryFindChild(this, path) ?? throw new global::System.Collections.Generic.KeyNotFoundException($"Cannot find control '{path}' as child of '{this}'");

		public abstract global::System.Type valueType { get; }

		public abstract int valueSizeInBytes { get; }

		public float magnitude => EvaluateMagnitude();

		protected internal unsafe void* currentStatePtr => global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.GetFrontBufferForDevice(GetDeviceIndex());

		protected internal unsafe void* previousFrameStatePtr => global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.GetBackBufferForDevice(GetDeviceIndex());

		protected internal unsafe void* defaultStatePtr => global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_DefaultStateBuffer;

		protected internal unsafe void* noiseMaskPtr => global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_NoiseMaskBuffer;

		protected internal uint stateOffsetRelativeToDeviceRoot
		{
			get
			{
				uint byteOffset = device.m_StateBlock.byteOffset;
				return m_StateBlock.byteOffset - byteOffset;
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC optimizedControlDataType => m_OptimizedControlDataType;

		internal bool isSetupFinished
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.SetupFinished) == global::UnityEngine.InputSystem.InputControl.ControlFlags.SetupFinished;
			}
			set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.SetupFinished;
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.SetupFinished;
				}
			}
		}

		internal bool isButton
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.IsButton) == global::UnityEngine.InputSystem.InputControl.ControlFlags.IsButton;
			}
			set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.IsButton;
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.IsButton;
				}
			}
		}

		internal bool isConfigUpToDate
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.ConfigUpToDate) == global::UnityEngine.InputSystem.InputControl.ControlFlags.ConfigUpToDate;
			}
			set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.ConfigUpToDate;
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.ConfigUpToDate;
				}
			}
		}

		internal bool dontReset
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.DontReset) == global::UnityEngine.InputSystem.InputControl.ControlFlags.DontReset;
			}
			set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.DontReset;
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.DontReset;
				}
			}
		}

		internal bool usesStateFromOtherControl
		{
			get
			{
				return (m_ControlFlags & global::UnityEngine.InputSystem.InputControl.ControlFlags.UsesStateFromOtherControl) == global::UnityEngine.InputSystem.InputControl.ControlFlags.UsesStateFromOtherControl;
			}
			set
			{
				if (value)
				{
					m_ControlFlags |= global::UnityEngine.InputSystem.InputControl.ControlFlags.UsesStateFromOtherControl;
				}
				else
				{
					m_ControlFlags &= ~global::UnityEngine.InputSystem.InputControl.ControlFlags.UsesStateFromOtherControl;
				}
			}
		}

		internal bool hasDefaultState => !m_DefaultState.isEmpty;

		public override string ToString()
		{
			return layout + ":" + path;
		}

		private string DebuggerDisplay()
		{
			if (!device.added)
			{
				return ToString();
			}
			try
			{
				return $"{layout}:{path}={this.ReadValueAsObject()}";
			}
			catch (global::System.Exception)
			{
				return ToString();
			}
		}

		public unsafe float EvaluateMagnitude()
		{
			return EvaluateMagnitude(currentStatePtr);
		}

		public unsafe virtual float EvaluateMagnitude(void* statePtr)
		{
			return -1f;
		}

		public unsafe abstract object ReadValueFromBufferAsObject(void* buffer, int bufferSize);

		public unsafe abstract object ReadValueFromStateAsObject(void* statePtr);

		public unsafe abstract void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize);

		public unsafe virtual void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			throw new global::System.NotSupportedException($"Control '{this}' does not support writing");
		}

		public unsafe virtual void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			throw new global::System.NotSupportedException($"Control '{this}' does not support writing");
		}

		public unsafe abstract bool CompareValue(void* firstStatePtr, void* secondStatePtr);

		public global::UnityEngine.InputSystem.InputControl TryGetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			return global::UnityEngine.InputSystem.InputControlPath.TryFindChild(this, path);
		}

		public TControl TryGetChildControl<TControl>(string path) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			global::UnityEngine.InputSystem.InputControl inputControl = TryGetChildControl(path);
			if (inputControl == null)
			{
				return null;
			}
			if (!(inputControl is TControl result))
			{
				throw new global::System.InvalidOperationException("Expected control '" + path + "' to be of type '" + typeof(TControl).Name + "' but is of type '" + inputControl.GetType().Name + "' instead!");
			}
			return result;
		}

		public global::UnityEngine.InputSystem.InputControl GetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			return TryGetChildControl(path) ?? throw new global::System.ArgumentException("Cannot find input control '" + MakeChildPath(path) + "'", "path");
		}

		public TControl GetChildControl<TControl>(string path) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			global::UnityEngine.InputSystem.InputControl childControl = GetChildControl(path);
			if (!(childControl is TControl result))
			{
				throw new global::System.ArgumentException("Expected control '" + path + "' to be of type '" + typeof(TControl).Name + "' but is of type '" + childControl.GetType().Name + "' instead!", "path");
			}
			return result;
		}

		protected InputControl()
		{
			m_StateBlock.byteOffset = 4294967294u;
		}

		protected virtual void FinishSetup()
		{
		}

		protected void RefreshConfigurationIfNeeded()
		{
			if (!isConfigUpToDate)
			{
				RefreshConfiguration();
				isConfigUpToDate = true;
			}
		}

		protected virtual void RefreshConfiguration()
		{
		}

		protected virtual global::UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
		{
			return 0;
		}

		public void ApplyParameterChanges()
		{
			SetOptimizedControlDataTypeRecursively();
			for (global::UnityEngine.InputSystem.InputControl inputControl = parent; inputControl != null; inputControl = inputControl.parent)
			{
				inputControl.SetOptimizedControlDataType();
			}
			MarkAsStaleRecursively();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void SetOptimizedControlDataType()
		{
			m_OptimizedControlDataType = (global::UnityEngine.InputSystem.InputSystem.s_Manager.optimizedControlsFeatureEnabled ? CalculateOptimizedControlDataType() : ((global::UnityEngine.InputSystem.Utilities.FourCC)0));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void SetOptimizedControlDataTypeRecursively()
		{
			if (m_ChildCount > 0)
			{
				foreach (global::UnityEngine.InputSystem.InputControl child in children)
				{
					child.SetOptimizedControlDataTypeRecursively();
				}
			}
			SetOptimizedControlDataType();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void EnsureOptimizationTypeHasNotChanged()
		{
			if (!global::UnityEngine.InputSystem.InputSystem.s_Manager.optimizedControlsFeatureEnabled)
			{
				return;
			}
			global::UnityEngine.InputSystem.Utilities.FourCC fourCC = CalculateOptimizedControlDataType();
			if (fourCC != optimizedControlDataType)
			{
				global::UnityEngine.Debug.LogError("Control '" + name + "' / '" + path + "' suddenly changed optimization state due to either format " + $"change or control parameters change (was '{optimizedControlDataType}' but became '{fourCC}'), " + "this hinders control hot path optimization, please call control.ApplyParameterChanges() after the changes to the control to fix this error.");
				m_OptimizedControlDataType = fourCC;
			}
			if (m_ChildCount <= 0)
			{
				return;
			}
			foreach (global::UnityEngine.InputSystem.InputControl child in children)
			{
				_ = child;
			}
		}

		internal void CallFinishSetupRecursive()
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> readOnlyArray = children;
			for (int i = 0; i < readOnlyArray.Count; i++)
			{
				readOnlyArray[i].CallFinishSetupRecursive();
			}
			FinishSetup();
			SetOptimizedControlDataTypeRecursively();
		}

		internal string MakeChildPath(string path)
		{
			if (this is global::UnityEngine.InputSystem.InputDevice)
			{
				return path;
			}
			return this.path + "/" + path;
		}

		internal void BakeOffsetIntoStateBlockRecursive(uint offset)
		{
			m_StateBlock.byteOffset += offset;
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> readOnlyArray = children;
			for (int i = 0; i < readOnlyArray.Count; i++)
			{
				readOnlyArray[i].BakeOffsetIntoStateBlockRecursive(offset);
			}
		}

		internal int GetDeviceIndex()
		{
			int deviceIndex = m_Device.m_DeviceIndex;
			if (deviceIndex == -1)
			{
				throw new global::System.InvalidOperationException("Cannot query value of control '" + path + "' before '" + device.name + "' has been added to system!");
			}
			return deviceIndex;
		}

		internal bool IsValueConsideredPressed(float value)
		{
			if (isButton)
			{
				return ((global::UnityEngine.InputSystem.Controls.ButtonControl)this).IsValueConsideredPressed(value);
			}
			return value >= global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		internal virtual void AddProcessor(object first)
		{
		}

		internal void MarkAsStale()
		{
			m_CachedValueIsStale = true;
			m_UnprocessedCachedValueIsStale = true;
		}

		internal void MarkAsStaleRecursively()
		{
			MarkAsStale();
			foreach (global::UnityEngine.InputSystem.InputControl child in children)
			{
				child.MarkAsStale();
				if (child is global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl)
				{
					buttonControl.UpdateWasPressed();
				}
			}
		}
	}
	public abstract class InputControl<TValue> : global::UnityEngine.InputSystem.InputControl where TValue : struct
	{
		internal global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputProcessor<TValue>> m_ProcessorStack;

		private TValue m_CachedValue;

		private TValue m_UnprocessedCachedValue;

		internal bool evaluateProcessorsEveryRead;

		public override global::System.Type valueType => typeof(TValue);

		public override int valueSizeInBytes => global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();

		public ref readonly TValue value
		{
			get
			{
				if (!global::UnityEngine.InputSystem.InputSystem.s_Manager.readValueCachingFeatureEnabled || m_CachedValueIsStale || evaluateProcessorsEveryRead)
				{
					m_CachedValue = ProcessValue(unprocessedValue);
					m_CachedValueIsStale = false;
				}
				return ref m_CachedValue;
			}
		}

		internal unsafe ref readonly TValue unprocessedValue
		{
			get
			{
				if (base.currentStatePtr == null)
				{
					return ref m_UnprocessedCachedValue;
				}
				if (!global::UnityEngine.InputSystem.InputSystem.s_Manager.readValueCachingFeatureEnabled || m_UnprocessedCachedValueIsStale)
				{
					m_UnprocessedCachedValue = ReadUnprocessedValueFromState(base.currentStatePtr);
					m_UnprocessedCachedValueIsStale = false;
				}
				return ref m_UnprocessedCachedValue;
			}
		}

		internal global::UnityEngine.InputSystem.InputProcessor<TValue>[] processors => m_ProcessorStack.ToArray();

		public TValue ReadValue()
		{
			return value;
		}

		public unsafe TValue ReadValueFromPreviousFrame()
		{
			return ReadValueFromState(base.previousFrameStatePtr);
		}

		public unsafe TValue ReadDefaultValue()
		{
			return ReadValueFromState(base.defaultStatePtr);
		}

		public unsafe TValue ReadValueFromState(void* statePtr)
		{
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			return ProcessValue(ReadUnprocessedValueFromState(statePtr));
		}

		public unsafe TValue ReadValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return ReadValueFromState(statePtr);
			}
			return value;
		}

		public unsafe TValue ReadUnprocessedValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return ReadUnprocessedValueFromState(statePtr);
			}
			return unprocessedValue;
		}

		public TValue ReadUnprocessedValue()
		{
			return unprocessedValue;
		}

		public unsafe abstract TValue ReadUnprocessedValueFromState(void* statePtr);

		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			return ReadValueFromState(statePtr);
		}

		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new global::System.ArgumentNullException("bufferPtr");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new global::System.ArgumentException($"bufferSize={bufferSize} < sizeof(TValue)={num}", "bufferSize");
			}
			TValue output = ReadValueFromState(statePtr);
			void* source = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(bufferPtr, source, num);
		}

		public unsafe override void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			if (bufferPtr == null)
			{
				throw new global::System.ArgumentNullException("bufferPtr");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new global::System.ArgumentException($"bufferSize={bufferSize} < sizeof(TValue)={num}", "bufferSize");
			}
			TValue output = default(TValue);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), bufferPtr, num);
			WriteValueIntoState(output, statePtr);
		}

		public unsafe override void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			if (value == null)
			{
				throw new global::System.ArgumentNullException("value");
			}
			if (!(value is TValue))
			{
				value = global::System.Convert.ChangeType(value, typeof(TValue));
			}
			TValue val = (TValue)value;
			WriteValueIntoState(val, statePtr);
		}

		public unsafe virtual void WriteValueIntoState(TValue value, void* statePtr)
		{
			throw new global::System.NotSupportedException($"Control '{this}' does not support writing");
		}

		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new global::System.ArgumentNullException("buffer");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new global::System.ArgumentException($"Expecting buffer of at least {num} bytes for value of type {typeof(TValue).Name} but got buffer of only {bufferSize} bytes instead", "bufferSize");
			}
			TValue output = default(TValue);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), buffer, num);
			return output;
		}

		private unsafe static bool CompareValue(ref TValue firstValue, ref TValue secondValue)
		{
			void* ptr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref firstValue);
			void* ptr2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref secondValue);
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr, ptr2, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>()) != 0;
		}

		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			TValue firstValue = ReadValueFromState(firstStatePtr);
			TValue secondValue = ReadValueFromState(secondStatePtr);
			return CompareValue(ref firstValue, ref secondValue);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public TValue ProcessValue(TValue value)
		{
			ProcessValue(ref value);
			return value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ProcessValue(ref TValue value)
		{
			if (m_ProcessorStack.length <= 0)
			{
				return;
			}
			value = m_ProcessorStack.firstValue.Process(value, this);
			if (m_ProcessorStack.additionalValues != null)
			{
				for (int i = 0; i < m_ProcessorStack.length - 1; i++)
				{
					value = m_ProcessorStack.additionalValues[i].Process(value, this);
				}
			}
		}

		internal TProcessor TryGetProcessor<TProcessor>() where TProcessor : global::UnityEngine.InputSystem.InputProcessor<TValue>
		{
			if (m_ProcessorStack.length > 0)
			{
				if (m_ProcessorStack.firstValue is TProcessor result)
				{
					return result;
				}
				if (m_ProcessorStack.additionalValues != null)
				{
					for (int i = 0; i < m_ProcessorStack.length - 1; i++)
					{
						if (m_ProcessorStack.additionalValues[i] is TProcessor result2)
						{
							return result2;
						}
					}
				}
			}
			return null;
		}

		internal override void AddProcessor(object processor)
		{
			if (!(processor is global::UnityEngine.InputSystem.InputProcessor<TValue> inputProcessor))
			{
				throw new global::System.ArgumentException("Cannot add processor of type '" + processor.GetType().Name + "' to control of type '" + GetType().Name + "'", "processor");
			}
			m_ProcessorStack.Append(inputProcessor);
		}

		protected override void FinishSetup()
		{
			foreach (global::UnityEngine.InputSystem.InputProcessor<TValue> item in m_ProcessorStack)
			{
				if (item.cachingPolicy == global::UnityEngine.InputSystem.InputProcessor.CachingPolicy.EvaluateOnEveryRead)
				{
					evaluateProcessorsEveryRead = true;
				}
			}
			base.FinishSetup();
		}
	}
}
