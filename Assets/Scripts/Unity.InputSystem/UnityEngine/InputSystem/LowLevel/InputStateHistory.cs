namespace UnityEngine.InputSystem.LowLevel
{
	public class InputStateHistory : global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record>, global::System.Collections.IEnumerable, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
	{
		private struct Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private readonly global::UnityEngine.InputSystem.LowLevel.InputStateHistory m_History;

			private int m_Index;

			public global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record Current => m_History[m_Index];

			object global::System.Collections.IEnumerator.Current => Current;

			public Enumerator(global::UnityEngine.InputSystem.LowLevel.InputStateHistory history)
			{
				m_History = history;
				m_Index = -1;
			}

			public bool MoveNext()
			{
				if (m_Index + 1 >= m_History.Count)
				{
					return false;
				}
				m_Index++;
				return true;
			}

			public void Reset()
			{
				m_Index = -1;
			}

			public void Dispose()
			{
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		protected internal struct RecordHeader
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public double time;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public uint version;

			[global::System.Runtime.InteropServices.FieldOffset(12)]
			public int controlIndex;

			[global::System.Runtime.InteropServices.FieldOffset(12)]
			private unsafe fixed byte m_StateWithoutControlIndex[1];

			[global::System.Runtime.InteropServices.FieldOffset(16)]
			private unsafe fixed byte m_StateWithControlIndex[1];

			public const int kSizeWithControlIndex = 16;

			public const int kSizeWithoutControlIndex = 12;

			public unsafe byte* statePtrWithControlIndex
			{
				get
				{
					fixed (byte* stateWithControlIndex = m_StateWithControlIndex)
					{
						return stateWithControlIndex;
					}
				}
			}

			public unsafe byte* statePtrWithoutControlIndex
			{
				get
				{
					fixed (byte* stateWithoutControlIndex = m_StateWithoutControlIndex)
					{
						return stateWithoutControlIndex;
					}
				}
			}
		}

		public struct Record : global::System.IEquatable<global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record>
		{
			private readonly global::UnityEngine.InputSystem.LowLevel.InputStateHistory m_Owner;

			private readonly int m_IndexPlusOne;

			private uint m_Version;

			internal unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header => m_Owner.GetRecord(recordIndex);

			internal int recordIndex => m_IndexPlusOne - 1;

			internal uint version => m_Version;

			public unsafe bool valid
			{
				get
				{
					if (m_Owner != null && m_IndexPlusOne != 0)
					{
						return header->version == m_Version;
					}
					return false;
				}
			}

			public global::UnityEngine.InputSystem.LowLevel.InputStateHistory owner => m_Owner;

			public int index
			{
				get
				{
					CheckValid();
					return m_Owner.RecordIndexToUserIndex(recordIndex);
				}
			}

			public unsafe double time
			{
				get
				{
					CheckValid();
					return header->time;
				}
			}

			public unsafe global::UnityEngine.InputSystem.InputControl control
			{
				get
				{
					CheckValid();
					global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> controls = m_Owner.controls;
					if (controls.Count == 1 && !m_Owner.m_AddNewControls)
					{
						return controls[0];
					}
					return controls[header->controlIndex];
				}
			}

			public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record next
			{
				get
				{
					CheckValid();
					int num = m_Owner.RecordIndexToUserIndex(recordIndex);
					if (num + 1 >= m_Owner.Count)
					{
						return default(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record);
					}
					int num2 = m_Owner.UserIndexToRecordIndex(num + 1);
					return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(m_Owner, num2, m_Owner.GetRecord(num2));
				}
			}

			public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record previous
			{
				get
				{
					CheckValid();
					int num = m_Owner.RecordIndexToUserIndex(recordIndex);
					if (num - 1 < 0)
					{
						return default(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record);
					}
					int num2 = m_Owner.UserIndexToRecordIndex(num - 1);
					return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(m_Owner, num2, m_Owner.GetRecord(num2));
				}
			}

			internal unsafe Record(global::UnityEngine.InputSystem.LowLevel.InputStateHistory owner, int index, global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header)
			{
				m_Owner = owner;
				m_IndexPlusOne = index + 1;
				m_Version = header->version;
			}

			public unsafe TValue ReadValue<TValue>() where TValue : struct
			{
				CheckValid();
				return m_Owner.ReadValue<TValue>(header);
			}

			public unsafe object ReadValueAsObject()
			{
				CheckValid();
				return m_Owner.ReadValueAsObject(header);
			}

			public unsafe void* GetUnsafeMemoryPtr()
			{
				CheckValid();
				return GetUnsafeMemoryPtrUnchecked();
			}

			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (m_Owner.controls.Count == 1 && !m_Owner.m_AddNewControls)
				{
					return header->statePtrWithoutControlIndex;
				}
				return header->statePtrWithControlIndex;
			}

			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				CheckValid();
				return GetUnsafeExtraMemoryPtrUnchecked();
			}

			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (m_Owner.extraMemoryPerRecord == 0)
				{
					throw new global::System.InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (byte*)header + m_Owner.bytesPerRecord - m_Owner.extraMemoryPerRecord;
			}

			public unsafe void CopyFrom(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record)
			{
				if (!record.valid)
				{
					throw new global::System.ArgumentException("Given history record is not valid", "record");
				}
				CheckValid();
				global::UnityEngine.InputSystem.InputControl value = record.control;
				int num = global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.IndexOfReference(m_Owner.controls, value);
				if (num == -1)
				{
					if (!m_Owner.m_AddNewControls)
					{
						throw new global::System.InvalidOperationException($"Control '{record.control}' is not tracked by target history");
					}
					num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Owner.m_Controls, ref m_Owner.m_ControlCount, value);
				}
				int stateSizeInBytes = m_Owner.m_StateSizeInBytes;
				if (stateSizeInBytes != record.m_Owner.m_StateSizeInBytes)
				{
					throw new global::System.InvalidOperationException($"Cannot copy record from owner with state size '{record.m_Owner.m_StateSizeInBytes}' to owner with state size '{stateSizeInBytes}'");
				}
				global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* ptr = header;
				global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* ptr2 = record.header;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, ptr2, 12L);
				ptr->version = ++m_Owner.m_CurrentVersion;
				m_Version = ptr->version;
				byte* destination = ptr->statePtrWithoutControlIndex;
				if (m_Owner.controls.Count > 1 || m_Owner.m_AddNewControls)
				{
					ptr->controlIndex = num;
					destination = ptr->statePtrWithControlIndex;
				}
				byte* source = ((record.m_Owner.m_ControlCount > 1 || record.m_Owner.m_AddNewControls) ? ptr2->statePtrWithControlIndex : ptr2->statePtrWithoutControlIndex);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, stateSizeInBytes);
				int extraMemoryPerRecord = m_Owner.m_ExtraMemoryPerRecord;
				if (extraMemoryPerRecord > 0 && extraMemoryPerRecord == record.m_Owner.m_ExtraMemoryPerRecord)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(GetUnsafeExtraMemoryPtr(), record.GetUnsafeExtraMemoryPtr(), extraMemoryPerRecord);
				}
				m_Owner.onRecordAdded?.Invoke(this);
			}

			internal unsafe void CheckValid()
			{
				if (m_Owner == null || m_IndexPlusOne == 0)
				{
					throw new global::System.InvalidOperationException("Value not initialized");
				}
				if (header->version != m_Version)
				{
					throw new global::System.InvalidOperationException("Record is no longer valid");
				}
			}

			public bool Equals(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record other)
			{
				if (m_Owner == other.m_Owner && m_IndexPlusOne == other.m_IndexPlusOne)
				{
					return m_Version == other.m_Version;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj is global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (((((m_Owner != null) ? m_Owner.GetHashCode() : 0) * 397) ^ m_IndexPlusOne) * 397) ^ (int)m_Version;
			}

			public override string ToString()
			{
				if (!valid)
				{
					return "<Invalid>";
				}
				return $"{{ control={control} value={ReadValueAsObject()} time={time} }}";
			}
		}

		private const int kDefaultHistorySize = 128;

		internal global::UnityEngine.InputSystem.InputControl[] m_Controls;

		internal int m_ControlCount;

		private global::Unity.Collections.NativeArray<byte> m_RecordBuffer;

		private int m_StateSizeInBytes;

		private int m_RecordCount;

		private int m_HistoryDepth = 128;

		private int m_ExtraMemoryPerRecord;

		internal int m_HeadIndex;

		internal uint m_CurrentVersion;

		private global::UnityEngine.InputSystem.LowLevel.InputUpdateType? m_UpdateMask;

		internal readonly bool m_AddNewControls;

		public int Count => m_RecordCount;

		public uint version => m_CurrentVersion;

		public int historyDepth
		{
			get
			{
				return m_HistoryDepth;
			}
			set
			{
				if (value < 0)
				{
					throw new global::System.ArgumentException("History depth cannot be negative", "value");
				}
				if (m_RecordBuffer.IsCreated)
				{
					throw new global::System.NotImplementedException();
				}
				m_HistoryDepth = value;
			}
		}

		public int extraMemoryPerRecord
		{
			get
			{
				return m_ExtraMemoryPerRecord;
			}
			set
			{
				if (value < 0)
				{
					throw new global::System.ArgumentException("Memory size cannot be negative", "value");
				}
				if (m_RecordBuffer.IsCreated)
				{
					throw new global::System.NotImplementedException();
				}
				m_ExtraMemoryPerRecord = value;
			}
		}

		public global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateMask
		{
			get
			{
				return m_UpdateMask ?? (global::UnityEngine.InputSystem.InputSystem.s_Manager.updateMask & ~global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Editor);
			}
			set
			{
				if (value == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
				{
					throw new global::System.ArgumentException("'InputUpdateType.None' is not a valid update mask", "value");
				}
				m_UpdateMask = value;
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> controls => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl>(m_Controls, 0, m_ControlCount);

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= m_RecordCount)
				{
					throw new global::System.ArgumentOutOfRangeException($"Index {index} is out of range for history with {m_RecordCount} entries", "index");
				}
				int index2 = UserIndexToRecordIndex(index);
				return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(this, index2, GetRecord(index2));
			}
			set
			{
				if (index < 0 || index >= m_RecordCount)
				{
					throw new global::System.ArgumentOutOfRangeException($"Index {index} is out of range for history with {m_RecordCount} entries", "index");
				}
				int index2 = UserIndexToRecordIndex(index);
				new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(this, index2, GetRecord(index2)).CopyFrom(value);
			}
		}

		public global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record> onRecordAdded { get; set; }

		public global::System.Func<global::UnityEngine.InputSystem.InputControl, double, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, bool> onShouldRecordStateChange { get; set; }

		internal int bytesPerRecord => global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(m_StateSizeInBytes + m_ExtraMemoryPerRecord + ((m_ControlCount == 1 && !m_AddNewControls) ? 12 : 16), 4);

		public InputStateHistory(int maxStateSizeInBytes)
		{
			if (maxStateSizeInBytes <= 0)
			{
				throw new global::System.ArgumentException("State size must be >= 0", "maxStateSizeInBytes");
			}
			m_AddNewControls = true;
			m_StateSizeInBytes = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(maxStateSizeInBytes, 4);
		}

		public InputStateHistory(string path)
		{
			using global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> inputControlList = global::UnityEngine.InputSystem.InputSystem.FindControls(path);
			m_Controls = inputControlList.ToArray();
			m_ControlCount = m_Controls.Length;
		}

		public InputStateHistory(global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			m_Controls = new global::UnityEngine.InputSystem.InputControl[1] { control };
			m_ControlCount = 1;
		}

		public InputStateHistory(global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControl> controls)
		{
			if (controls != null)
			{
				m_Controls = global::System.Linq.Enumerable.ToArray(controls);
				m_ControlCount = m_Controls.Length;
			}
		}

		~InputStateHistory()
		{
			Dispose();
		}

		public void Clear()
		{
			m_HeadIndex = 0;
			m_RecordCount = 0;
			m_CurrentVersion++;
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record AddRecord(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record)
		{
			int index;
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header = AllocateRecord(out index);
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record result = new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(this, index, header);
			result.CopyFrom(record);
			return result;
		}

		public void StartRecording()
		{
			foreach (global::UnityEngine.InputSystem.InputControl control in controls)
			{
				global::UnityEngine.InputSystem.LowLevel.InputState.AddChangeMonitor(control, this, -1L);
			}
		}

		public void StopRecording()
		{
			foreach (global::UnityEngine.InputSystem.InputControl control in controls)
			{
				global::UnityEngine.InputSystem.LowLevel.InputState.RemoveChangeMonitor(control, this, -1L);
			}
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record RecordStateChange(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.IsA<global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent>())
			{
				throw new global::System.NotImplementedException();
			}
			if (!eventPtr.IsA<global::UnityEngine.InputSystem.LowLevel.StateEvent>())
			{
				throw new global::System.ArgumentException($"Event must be a state event but is '{eventPtr}' instead", "eventPtr");
			}
			byte* statePtr = (byte*)global::UnityEngine.InputSystem.LowLevel.StateEvent.From(eventPtr)->state - control.device.stateBlock.byteOffset;
			return RecordStateChange(control, statePtr, eventPtr.time);
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record RecordStateChange(global::UnityEngine.InputSystem.InputControl control, void* statePtr, double time)
		{
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(m_Controls, control, m_ControlCount);
			if (num == -1)
			{
				if (!m_AddNewControls)
				{
					throw new global::System.ArgumentException($"Control '{control}' is not part of InputStateHistory", "control");
				}
				if (control.stateBlock.alignedSizeInBytes > m_StateSizeInBytes)
				{
					throw new global::System.InvalidOperationException($"Cannot add control '{control}' with state larger than {m_StateSizeInBytes} bytes");
				}
				num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Controls, ref m_ControlCount, control);
			}
			int index;
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* ptr = AllocateRecord(out index);
			ptr->time = time;
			ptr->version = ++m_CurrentVersion;
			byte* destination = ptr->statePtrWithoutControlIndex;
			if (m_ControlCount > 1 || m_AddNewControls)
			{
				ptr->controlIndex = num;
				destination = ptr->statePtrWithControlIndex;
			}
			uint alignedSizeInBytes = control.stateBlock.alignedSizeInBytes;
			uint byteOffset = control.stateBlock.byteOffset;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, (byte*)statePtr + byteOffset, alignedSizeInBytes);
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record = new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(this, index, ptr);
			onRecordAdded?.Invoke(record);
			return record;
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			StopRecording();
			Destroy();
			global::System.GC.SuppressFinalize(this);
		}

		protected void Destroy()
		{
			if (m_RecordBuffer.IsCreated)
			{
				m_RecordBuffer.Dispose();
				m_RecordBuffer = default(global::Unity.Collections.NativeArray<byte>);
			}
		}

		private void Allocate()
		{
			if (!m_AddNewControls)
			{
				m_StateSizeInBytes = 0;
				foreach (global::UnityEngine.InputSystem.InputControl control in controls)
				{
					m_StateSizeInBytes = (int)global::System.Math.Max((uint)m_StateSizeInBytes, control.stateBlock.alignedSizeInBytes);
				}
			}
			int length = bytesPerRecord * m_HistoryDepth;
			m_RecordBuffer = new global::Unity.Collections.NativeArray<byte>(length, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
		}

		protected internal int RecordIndexToUserIndex(int index)
		{
			if (index < m_HeadIndex)
			{
				return m_HistoryDepth - m_HeadIndex + index;
			}
			return index - m_HeadIndex;
		}

		protected internal int UserIndexToRecordIndex(int index)
		{
			return (m_HeadIndex + index) % m_HistoryDepth;
		}

		protected internal unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* GetRecord(int index)
		{
			if (!m_RecordBuffer.IsCreated)
			{
				throw new global::System.InvalidOperationException("History buffer has been disposed");
			}
			if (index < 0 || index >= m_HistoryDepth)
			{
				throw new global::System.ArgumentOutOfRangeException("index");
			}
			return GetRecordUnchecked(index);
		}

		internal unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* GetRecordUnchecked(int index)
		{
			return (global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader*)((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_RecordBuffer) + index * bytesPerRecord);
		}

		protected internal unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* AllocateRecord(out int index)
		{
			if (!m_RecordBuffer.IsCreated)
			{
				Allocate();
			}
			index = (m_HeadIndex + m_RecordCount) % m_HistoryDepth;
			if (m_RecordCount == m_HistoryDepth)
			{
				m_HeadIndex = (m_HeadIndex + 1) % m_HistoryDepth;
			}
			else
			{
				m_RecordCount++;
			}
			return (global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader*)((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_RecordBuffer) + bytesPerRecord * index);
		}

		protected unsafe TValue ReadValue<TValue>(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* data) where TValue : struct
		{
			int num;
			global::UnityEngine.InputSystem.InputControl inputControl;
			if (m_ControlCount == 1)
			{
				num = ((!m_AddNewControls) ? 1 : 0);
				if (num != 0)
				{
					inputControl = controls[0];
					goto IL_003d;
				}
			}
			else
			{
				num = 0;
			}
			inputControl = controls[data->controlIndex];
			goto IL_003d;
			IL_003d:
			global::UnityEngine.InputSystem.InputControl inputControl2 = inputControl;
			if (!(inputControl2 is global::UnityEngine.InputSystem.InputControl<TValue> inputControl3))
			{
				throw new global::System.InvalidOperationException($"Cannot read value of type '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(typeof(TValue)))}' from control '{inputControl2}' with value type '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(inputControl2.valueType))}'");
			}
			byte* ptr = ((num != 0) ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			ptr -= inputControl2.stateBlock.byteOffset;
			return inputControl3.ReadValueFromState(ptr);
		}

		protected unsafe object ReadValueAsObject(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* data)
		{
			int num;
			global::UnityEngine.InputSystem.InputControl inputControl;
			if (m_ControlCount == 1)
			{
				num = ((!m_AddNewControls) ? 1 : 0);
				if (num != 0)
				{
					inputControl = controls[0];
					goto IL_003d;
				}
			}
			else
			{
				num = 0;
			}
			inputControl = controls[data->controlIndex];
			goto IL_003d;
			IL_003d:
			global::UnityEngine.InputSystem.InputControl inputControl2 = inputControl;
			byte* ptr = ((num != 0) ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			ptr -= inputControl2.stateBlock.byteOffset;
			return inputControl2.ReadValueFromStateAsObject(ptr);
		}

		unsafe void global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyControlStateChanged(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, long monitorIndex)
		{
			global::UnityEngine.InputSystem.LowLevel.InputUpdateType currentUpdateType = global::UnityEngine.InputSystem.LowLevel.InputState.currentUpdateType;
			global::UnityEngine.InputSystem.LowLevel.InputUpdateType inputUpdateType = updateMask;
			if ((currentUpdateType & inputUpdateType) != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None && (onShouldRecordStateChange == null || onShouldRecordStateChange(control, time, eventPtr)))
			{
				RecordStateChange(control, control.currentStatePtr, time);
			}
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyTimerExpired(global::UnityEngine.InputSystem.InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}
	}
	public class InputStateHistory<TValue> : global::UnityEngine.InputSystem.LowLevel.InputStateHistory, global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record>, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IReadOnlyCollection<global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record> where TValue : struct
	{
		private struct Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private readonly global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> m_History;

			private int m_Index;

			public global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record Current => m_History[m_Index];

			object global::System.Collections.IEnumerator.Current => Current;

			public Enumerator(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> history)
			{
				m_History = history;
				m_Index = -1;
			}

			public bool MoveNext()
			{
				if (m_Index + 1 >= m_History.Count)
				{
					return false;
				}
				m_Index++;
				return true;
			}

			public void Reset()
			{
				m_Index = -1;
			}

			public void Dispose()
			{
			}
		}

		public new struct Record : global::System.IEquatable<global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record>
		{
			private readonly global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> m_Owner;

			private readonly int m_IndexPlusOne;

			private uint m_Version;

			internal unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header => m_Owner.GetRecord(recordIndex);

			internal int recordIndex => m_IndexPlusOne - 1;

			public unsafe bool valid
			{
				get
				{
					if (m_Owner != null && m_IndexPlusOne != 0)
					{
						return header->version == m_Version;
					}
					return false;
				}
			}

			public global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> owner => m_Owner;

			public int index
			{
				get
				{
					CheckValid();
					return m_Owner.RecordIndexToUserIndex(recordIndex);
				}
			}

			public unsafe double time
			{
				get
				{
					CheckValid();
					return header->time;
				}
			}

			public unsafe global::UnityEngine.InputSystem.InputControl<TValue> control
			{
				get
				{
					CheckValid();
					global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> controls = m_Owner.controls;
					if (controls.Count == 1 && !m_Owner.m_AddNewControls)
					{
						return (global::UnityEngine.InputSystem.InputControl<TValue>)controls[0];
					}
					return (global::UnityEngine.InputSystem.InputControl<TValue>)controls[header->controlIndex];
				}
			}

			public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record next
			{
				get
				{
					CheckValid();
					int num = m_Owner.RecordIndexToUserIndex(recordIndex);
					if (num + 1 >= m_Owner.Count)
					{
						return default(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record);
					}
					int num2 = m_Owner.UserIndexToRecordIndex(num + 1);
					return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record(m_Owner, num2, m_Owner.GetRecord(num2));
				}
			}

			public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record previous
			{
				get
				{
					CheckValid();
					int num = m_Owner.RecordIndexToUserIndex(recordIndex);
					if (num - 1 < 0)
					{
						return default(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record);
					}
					int num2 = m_Owner.UserIndexToRecordIndex(num - 1);
					return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record(m_Owner, num2, m_Owner.GetRecord(num2));
				}
			}

			internal unsafe Record(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> owner, int index, global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header)
			{
				m_Owner = owner;
				m_IndexPlusOne = index + 1;
				m_Version = header->version;
			}

			internal Record(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue> owner, int index)
			{
				m_Owner = owner;
				m_IndexPlusOne = index + 1;
				m_Version = 0u;
			}

			public unsafe TValue ReadValue()
			{
				CheckValid();
				return m_Owner.ReadValue<TValue>(header);
			}

			public unsafe void* GetUnsafeMemoryPtr()
			{
				CheckValid();
				return GetUnsafeMemoryPtrUnchecked();
			}

			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (m_Owner.controls.Count == 1 && !m_Owner.m_AddNewControls)
				{
					return header->statePtrWithoutControlIndex;
				}
				return header->statePtrWithControlIndex;
			}

			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				CheckValid();
				return GetUnsafeExtraMemoryPtrUnchecked();
			}

			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (m_Owner.extraMemoryPerRecord == 0)
				{
					throw new global::System.InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (byte*)header + m_Owner.bytesPerRecord - m_Owner.extraMemoryPerRecord;
			}

			public unsafe void CopyFrom(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record record)
			{
				CheckValid();
				if (!record.valid)
				{
					throw new global::System.ArgumentException("Given history record is not valid", "record");
				}
				global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record2 = new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(m_Owner, recordIndex, header);
				record2.CopyFrom(new global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record(record.m_Owner, record.recordIndex, record.header));
				m_Version = record2.version;
			}

			private unsafe void CheckValid()
			{
				if (m_Owner == null || m_IndexPlusOne == 0)
				{
					throw new global::System.InvalidOperationException("Value not initialized");
				}
				if (header->version != m_Version)
				{
					throw new global::System.InvalidOperationException("Record is no longer valid");
				}
			}

			public bool Equals(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record other)
			{
				if (m_Owner == other.m_Owner && m_IndexPlusOne == other.m_IndexPlusOne)
				{
					return m_Version == other.m_Version;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj is global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (((((m_Owner != null) ? m_Owner.GetHashCode() : 0) * 397) ^ m_IndexPlusOne) * 397) ^ (int)m_Version;
			}

			public override string ToString()
			{
				if (!valid)
				{
					return "<Invalid>";
				}
				return $"{{ control={control} value={ReadValue()} time={time} }}";
			}
		}

		public new unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new global::System.ArgumentOutOfRangeException($"Index {index} is out of range for history with {base.Count} entries", "index");
				}
				int index2 = UserIndexToRecordIndex(index);
				return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record(this, index2, GetRecord(index2));
			}
			set
			{
				if (index < 0 || index >= base.Count)
				{
					throw new global::System.ArgumentOutOfRangeException($"Index {index} is out of range for history with {base.Count} entries", "index");
				}
				int index2 = UserIndexToRecordIndex(index);
				new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record(this, index2, GetRecord(index2)).CopyFrom(value);
			}
		}

		public InputStateHistory(int? maxStateSizeInBytes = null)
			: base(maxStateSizeInBytes ?? global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>())
		{
			if (maxStateSizeInBytes < global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>())
			{
				throw new global::System.ArgumentException("Max state size cannot be smaller than sizeof(TValue)", "maxStateSizeInBytes");
			}
		}

		public InputStateHistory(global::UnityEngine.InputSystem.InputControl<TValue> control)
			: base(control)
		{
		}

		public InputStateHistory(string path)
			: base(path)
		{
			foreach (global::UnityEngine.InputSystem.InputControl control in base.controls)
			{
				if (!typeof(TValue).IsAssignableFrom(control.valueType))
				{
					throw new global::System.ArgumentException($"Control '{control}' matched by '{path}' has value type '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(control.valueType))}' which is incompatible with '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(typeof(TValue)))}'");
				}
			}
		}

		~InputStateHistory()
		{
			Destroy();
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record AddRecord(global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record record)
		{
			int index;
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* header = AllocateRecord(out index);
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record result = new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record(this, index, header);
			result.CopyFrom(record);
			return result;
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record RecordStateChange(global::UnityEngine.InputSystem.InputControl<TValue> control, TValue value, double time = -1.0)
		{
			global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;
			using (global::UnityEngine.InputSystem.LowLevel.StateEvent.From(control.device, out eventPtr))
			{
				byte* statePtr = (byte*)global::UnityEngine.InputSystem.LowLevel.StateEvent.From(eventPtr)->state - control.device.stateBlock.byteOffset;
				control.WriteValueIntoState(value, statePtr);
				if (time >= 0.0)
				{
					eventPtr.time = time;
				}
				global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record = RecordStateChange(control, eventPtr);
				return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record(this, record.recordIndex, record.header);
			}
		}

		public new global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Record> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<TValue>.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
