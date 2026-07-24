namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Serializable]
	public sealed class InputEventTrace : global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>, global::System.Collections.IEnumerable
	{
		private class Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::UnityEngine.InputSystem.LowLevel.InputEventTrace m_Trace;

			private int m_ChangeCounter;

			internal global::UnityEngine.InputSystem.LowLevel.InputEventPtr m_Current;

			public global::UnityEngine.InputSystem.LowLevel.InputEventPtr Current => m_Current;

			object global::System.Collections.IEnumerator.Current => Current;

			public Enumerator(global::UnityEngine.InputSystem.LowLevel.InputEventTrace trace)
			{
				m_Trace = trace;
				m_ChangeCounter = trace.m_ChangeCounter;
			}

			public void Dispose()
			{
				m_Trace = null;
				m_Current = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
			}

			public bool MoveNext()
			{
				if (m_Trace == null)
				{
					throw new global::System.ObjectDisposedException(ToString());
				}
				if (m_Trace.m_ChangeCounter != m_ChangeCounter)
				{
					throw new global::System.InvalidOperationException("Trace has been modified while enumerating!");
				}
				return m_Trace.GetNextEvent(ref m_Current);
			}

			public void Reset()
			{
				m_Current = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
				m_ChangeCounter = m_Trace.m_ChangeCounter;
			}
		}

		[global::System.Flags]
		private enum FileFlags
		{
			FixedUpdate = 1
		}

		public class ReplayController : global::System.IDisposable
		{
			private global::UnityEngine.InputSystem.LowLevel.InputEventTrace m_EventTrace;

			private global::UnityEngine.InputSystem.LowLevel.InputEventTrace.Enumerator m_Enumerator;

			private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::System.Collections.Generic.KeyValuePair<int, int>> m_DeviceIDMappings;

			private bool m_CreateNewDevices;

			private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputDevice> m_CreatedDevices;

			private global::System.Action m_OnFinished;

			private global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> m_OnEvent;

			private double m_StartTimeAsPerFirstEvent;

			private double m_StartTimeAsPerRuntime;

			private int m_AllEventsByTimeIndex;

			private global::System.Collections.Generic.List<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> m_AllEventsByTime;

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace trace => m_EventTrace;

			public bool finished { get; private set; }

			public bool paused { get; set; }

			public int position { get; private set; }

			public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputDevice> createdDevices => m_CreatedDevices;

			internal ReplayController(global::UnityEngine.InputSystem.LowLevel.InputEventTrace trace)
			{
				if (trace == null)
				{
					throw new global::System.ArgumentNullException("trace");
				}
				m_EventTrace = trace;
			}

			public void Dispose()
			{
				global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate -= OnBeginFrame;
				finished = true;
				foreach (global::UnityEngine.InputSystem.InputDevice createdDevice in m_CreatedDevices)
				{
					global::UnityEngine.InputSystem.InputSystem.RemoveDevice(createdDevice);
				}
				m_CreatedDevices = default(global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputDevice>);
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController WithDeviceMappedFromTo(global::UnityEngine.InputSystem.InputDevice recordedDevice, global::UnityEngine.InputSystem.InputDevice playbackDevice)
			{
				if (recordedDevice == null)
				{
					throw new global::System.ArgumentNullException("recordedDevice");
				}
				if (playbackDevice == null)
				{
					throw new global::System.ArgumentNullException("playbackDevice");
				}
				WithDeviceMappedFromTo(recordedDevice.deviceId, playbackDevice.deviceId);
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController WithDeviceMappedFromTo(int recordedDeviceId, int playbackDeviceId)
			{
				for (int i = 0; i < m_DeviceIDMappings.length; i++)
				{
					if (m_DeviceIDMappings[i].Key == recordedDeviceId)
					{
						if (recordedDeviceId == playbackDeviceId)
						{
							m_DeviceIDMappings.RemoveAtWithCapacity(i);
						}
						else
						{
							m_DeviceIDMappings[i] = new global::System.Collections.Generic.KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId);
						}
						return this;
					}
				}
				if (recordedDeviceId == playbackDeviceId)
				{
					return this;
				}
				m_DeviceIDMappings.AppendWithCapacity(new global::System.Collections.Generic.KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId));
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController WithAllDevicesMappedToNewInstances()
			{
				m_CreateNewDevices = true;
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController OnFinished(global::System.Action action)
			{
				m_OnFinished = action;
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController OnEvent(global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> action)
			{
				m_OnEvent = action;
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayOneEvent()
			{
				if (!MoveNext(skipFrameEvents: true, out var eventPtr))
				{
					throw new global::System.InvalidOperationException("No more events");
				}
				QueueEvent(eventPtr);
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController Rewind()
			{
				m_Enumerator = null;
				m_AllEventsByTime = null;
				m_AllEventsByTimeIndex = -1;
				position = 0;
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayAllFramesOneByOne()
			{
				finished = false;
				global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate += OnBeginFrame;
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayAllEvents()
			{
				finished = false;
				try
				{
					global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;
					while (MoveNext(skipFrameEvents: true, out eventPtr))
					{
						QueueEvent(eventPtr);
					}
				}
				finally
				{
					Finished();
				}
				return this;
			}

			public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController PlayAllEventsAccordingToTimestamps()
			{
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>();
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;
				while (MoveNext(skipFrameEvents: true, out eventPtr))
				{
					list.Add(eventPtr);
				}
				list.Sort((global::UnityEngine.InputSystem.LowLevel.InputEventPtr a, global::UnityEngine.InputSystem.LowLevel.InputEventPtr b) => a.time.CompareTo(b.time));
				m_Enumerator.Dispose();
				m_Enumerator = null;
				m_AllEventsByTime = list;
				position = 0;
				finished = false;
				m_StartTimeAsPerFirstEvent = -1.0;
				m_AllEventsByTimeIndex = -1;
				global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate += OnBeginFrame;
				return this;
			}

			private void OnBeginFrame()
			{
				if (paused)
				{
					return;
				}
				if (!MoveNext(skipFrameEvents: false, out var eventPtr))
				{
					if (m_AllEventsByTime == null || m_AllEventsByTimeIndex >= m_AllEventsByTime.Count)
					{
						Finished();
					}
					return;
				}
				if (eventPtr.type == FrameMarkerEvent)
				{
					if (!MoveNext(skipFrameEvents: false, out var eventPtr2))
					{
						Finished();
						return;
					}
					if (eventPtr2.type == FrameMarkerEvent)
					{
						int num = position - 1;
						position = num;
						m_Enumerator.m_Current = eventPtr;
						return;
					}
					eventPtr = eventPtr2;
				}
				while (true)
				{
					QueueEvent(eventPtr);
					if (!MoveNext(skipFrameEvents: false, out var eventPtr3))
					{
						if (m_AllEventsByTime == null || m_AllEventsByTimeIndex >= m_AllEventsByTime.Count)
						{
							Finished();
						}
						break;
					}
					if (eventPtr3.type == FrameMarkerEvent)
					{
						m_Enumerator.m_Current = eventPtr;
						int num = position - 1;
						position = num;
						break;
					}
					eventPtr = eventPtr3;
				}
			}

			private void Finished()
			{
				finished = true;
				global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate -= OnBeginFrame;
				m_OnFinished?.Invoke();
			}

			private void QueueEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
			{
				double internalTime = eventPtr.internalTime;
				if (m_AllEventsByTime != null)
				{
					eventPtr.internalTime = m_StartTimeAsPerRuntime + (eventPtr.internalTime - m_StartTimeAsPerFirstEvent);
				}
				else
				{
					eventPtr.internalTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
				}
				int id = eventPtr.id;
				int deviceId = eventPtr.deviceId;
				eventPtr.deviceId = ApplyDeviceMapping(deviceId);
				m_OnEvent?.Invoke(eventPtr);
				try
				{
					global::UnityEngine.InputSystem.InputSystem.QueueEvent(eventPtr);
				}
				finally
				{
					eventPtr.internalTime = internalTime;
					eventPtr.id = id;
					eventPtr.deviceId = deviceId;
				}
			}

			private bool MoveNext(bool skipFrameEvents, out global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
			{
				eventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
				if (m_AllEventsByTime != null)
				{
					if (m_AllEventsByTimeIndex + 1 >= m_AllEventsByTime.Count)
					{
						position = m_AllEventsByTime.Count;
						m_AllEventsByTimeIndex = m_AllEventsByTime.Count;
						return false;
					}
					if (m_AllEventsByTimeIndex < 0)
					{
						m_StartTimeAsPerFirstEvent = m_AllEventsByTime[0].internalTime;
						m_StartTimeAsPerRuntime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
					}
					else if (m_AllEventsByTimeIndex < m_AllEventsByTime.Count - 1 && m_AllEventsByTime[m_AllEventsByTimeIndex + 1].internalTime > m_StartTimeAsPerFirstEvent + (global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime - m_StartTimeAsPerRuntime))
					{
						return false;
					}
					m_AllEventsByTimeIndex++;
					int num = position + 1;
					position = num;
					eventPtr = m_AllEventsByTime[m_AllEventsByTimeIndex];
				}
				else
				{
					if (m_Enumerator == null)
					{
						m_Enumerator = new global::UnityEngine.InputSystem.LowLevel.InputEventTrace.Enumerator(m_EventTrace);
					}
					do
					{
						if (!m_Enumerator.MoveNext())
						{
							return false;
						}
						int num = position + 1;
						position = num;
						eventPtr = m_Enumerator.Current;
					}
					while (skipFrameEvents && eventPtr.type == FrameMarkerEvent);
				}
				return true;
			}

			private int ApplyDeviceMapping(int originalDeviceId)
			{
				for (int i = 0; i < m_DeviceIDMappings.length; i++)
				{
					global::System.Collections.Generic.KeyValuePair<int, int> keyValuePair = m_DeviceIDMappings[i];
					if (keyValuePair.Key == originalDeviceId)
					{
						return keyValuePair.Value;
					}
				}
				if (m_CreateNewDevices)
				{
					try
					{
						int num = m_EventTrace.deviceInfos.IndexOf((global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo x) => x.deviceId == originalDeviceId);
						if (num != -1)
						{
							global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo deviceInfo = m_EventTrace.deviceInfos[num];
							global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(deviceInfo.layout);
							if (!global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.HasLayout(internedString))
							{
								if (string.IsNullOrEmpty(deviceInfo.m_FullLayoutJson))
								{
									return originalDeviceId;
								}
								global::UnityEngine.InputSystem.InputSystem.RegisterLayout(deviceInfo.m_FullLayoutJson);
							}
							global::UnityEngine.InputSystem.InputDevice inputDevice = global::UnityEngine.InputSystem.InputSystem.AddDevice(internedString);
							WithDeviceMappedFromTo(originalDeviceId, inputDevice.deviceId);
							m_CreatedDevices.AppendWithCapacity(inputDevice);
							return inputDevice.deviceId;
						}
					}
					catch
					{
					}
				}
				return originalDeviceId;
			}
		}

		[global::System.Serializable]
		public struct DeviceInfo
		{
			[global::UnityEngine.SerializeField]
			internal int m_DeviceId;

			[global::UnityEngine.SerializeField]
			internal string m_Layout;

			[global::UnityEngine.SerializeField]
			internal global::UnityEngine.InputSystem.Utilities.FourCC m_StateFormat;

			[global::UnityEngine.SerializeField]
			internal int m_StateSizeInBytes;

			[global::UnityEngine.SerializeField]
			internal string m_FullLayoutJson;

			public int deviceId
			{
				get
				{
					return m_DeviceId;
				}
				set
				{
					m_DeviceId = value;
				}
			}

			public string layout
			{
				get
				{
					return m_Layout;
				}
				set
				{
					m_Layout = value;
				}
			}

			public global::UnityEngine.InputSystem.Utilities.FourCC stateFormat
			{
				get
				{
					return m_StateFormat;
				}
				set
				{
					m_StateFormat = value;
				}
			}

			public int stateSizeInBytes
			{
				get
				{
					return m_StateSizeInBytes;
				}
				set
				{
					m_StateSizeInBytes = value;
				}
			}
		}

		private const int kDefaultBufferSize = 1048576;

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputEvenTraceMarker = new global::Unity.Profiling.ProfilerMarker("InputEventTrace");

		[global::System.NonSerialized]
		private int m_ChangeCounter;

		[global::System.NonSerialized]
		private bool m_Enabled;

		[global::System.NonSerialized]
		private global::System.Func<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice, bool> m_OnFilterEvent;

		[global::UnityEngine.SerializeField]
		private int m_DeviceId;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>> m_EventListeners;

		[global::UnityEngine.SerializeField]
		private long m_EventBufferSize;

		[global::UnityEngine.SerializeField]
		private long m_MaxEventBufferSize;

		[global::UnityEngine.SerializeField]
		private long m_GrowIncrementSize;

		[global::UnityEngine.SerializeField]
		private long m_EventCount;

		[global::UnityEngine.SerializeField]
		private long m_EventSizeInBytes;

		[global::UnityEngine.SerializeField]
		private ulong m_EventBufferStorage;

		[global::UnityEngine.SerializeField]
		private ulong m_EventBufferHeadStorage;

		[global::UnityEngine.SerializeField]
		private ulong m_EventBufferTailStorage;

		[global::UnityEngine.SerializeField]
		private bool m_HasWrapped;

		[global::UnityEngine.SerializeField]
		private bool m_RecordFrameMarkers;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo[] m_DeviceInfos;

		private static int kFileVersion = 1;

		public static global::UnityEngine.InputSystem.Utilities.FourCC FrameMarkerEvent => new global::UnityEngine.InputSystem.Utilities.FourCC('F', 'R', 'M', 'E');

		public int deviceId
		{
			get
			{
				return m_DeviceId;
			}
			set
			{
				m_DeviceId = value;
			}
		}

		public bool enabled => m_Enabled;

		public bool recordFrameMarkers
		{
			get
			{
				return m_RecordFrameMarkers;
			}
			set
			{
				if (m_RecordFrameMarkers == value)
				{
					return;
				}
				m_RecordFrameMarkers = value;
				if (m_Enabled)
				{
					if (value)
					{
						global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate += OnBeforeUpdate;
					}
					else
					{
						global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate -= OnBeforeUpdate;
					}
				}
			}
		}

		public long eventCount => m_EventCount;

		public long totalEventSizeInBytes => m_EventSizeInBytes;

		public unsafe long allocatedSizeInBytes
		{
			get
			{
				if (m_EventBuffer == null)
				{
					return 0L;
				}
				return m_EventBufferSize;
			}
		}

		public long maxSizeInBytes => m_MaxEventBufferSize;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo> deviceInfos => m_DeviceInfos;

		public global::System.Func<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice, bool> onFilterEvent
		{
			get
			{
				return m_OnFilterEvent;
			}
			set
			{
				m_OnFilterEvent = value;
			}
		}

		private unsafe byte* m_EventBuffer
		{
			get
			{
				return (byte*)m_EventBufferStorage;
			}
			set
			{
				m_EventBufferStorage = (ulong)value;
			}
		}

		private unsafe byte* m_EventBufferHead
		{
			get
			{
				return (byte*)m_EventBufferHeadStorage;
			}
			set
			{
				m_EventBufferHeadStorage = (ulong)value;
			}
		}

		private unsafe byte* m_EventBufferTail
		{
			get
			{
				return (byte*)m_EventBufferTailStorage;
			}
			set
			{
				m_EventBufferTailStorage = (ulong)value;
			}
		}

		private static global::UnityEngine.InputSystem.Utilities.FourCC kFileFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('I', 'E', 'V', 'T');

		public event global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> onEvent
		{
			add
			{
				m_EventListeners.AddCallback(value);
			}
			remove
			{
				m_EventListeners.RemoveCallback(value);
			}
		}

		public InputEventTrace(global::UnityEngine.InputSystem.InputDevice device, long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
			: this(bufferSizeInBytes, growBuffer, maxBufferSizeInBytes, growIncrementSizeInBytes)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			m_DeviceId = device.deviceId;
		}

		public InputEventTrace(long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
		{
			m_EventBufferSize = (uint)bufferSizeInBytes;
			if (growBuffer)
			{
				if (maxBufferSizeInBytes < 0)
				{
					m_MaxEventBufferSize = 268435456L;
				}
				else
				{
					m_MaxEventBufferSize = maxBufferSizeInBytes;
				}
				if (growIncrementSizeInBytes < 0)
				{
					m_GrowIncrementSize = 1048576L;
				}
				else
				{
					m_GrowIncrementSize = growIncrementSizeInBytes;
				}
			}
			else
			{
				m_MaxEventBufferSize = m_EventBufferSize;
			}
		}

		public void WriteTo(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new global::System.ArgumentNullException("filePath");
			}
			using global::System.IO.FileStream stream = global::System.IO.File.OpenWrite(filePath);
			WriteTo(stream);
		}

		public unsafe void WriteTo(global::System.IO.Stream stream)
		{
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new global::System.ArgumentException("Stream does not support seeking", "stream");
			}
			global::System.IO.BinaryWriter binaryWriter = new global::System.IO.BinaryWriter(stream);
			global::UnityEngine.InputSystem.LowLevel.InputEventTrace.FileFlags fileFlags = (global::UnityEngine.InputSystem.LowLevel.InputEventTrace.FileFlags)0;
			if (global::UnityEngine.InputSystem.InputSystem.settings.updateMode == global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInFixedUpdate)
			{
				fileFlags |= global::UnityEngine.InputSystem.LowLevel.InputEventTrace.FileFlags.FixedUpdate;
			}
			binaryWriter.Write(kFileFormat);
			binaryWriter.Write(kFileVersion);
			binaryWriter.Write((int)fileFlags);
			binaryWriter.Write((int)global::UnityEngine.Application.platform);
			binaryWriter.Write((ulong)m_EventCount);
			binaryWriter.Write((ulong)m_EventSizeInBytes);
			using (global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					global::UnityEngine.InputSystem.LowLevel.InputEventPtr current = enumerator.Current;
					uint sizeInBytes = current.sizeInBytes;
					byte[] array = new byte[sizeInBytes];
					fixed (byte* destination = array)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, current.data, sizeInBytes);
						binaryWriter.Write(array);
					}
				}
			}
			binaryWriter.Flush();
			long position = stream.Position;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_DeviceInfos);
			binaryWriter.Write(num);
			for (int i = 0; i < num; i++)
			{
				ref global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo reference = ref m_DeviceInfos[i];
				binaryWriter.Write(reference.deviceId);
				binaryWriter.Write(reference.layout);
				binaryWriter.Write(reference.stateFormat);
				binaryWriter.Write(reference.stateSizeInBytes);
				binaryWriter.Write(reference.m_FullLayoutJson ?? string.Empty);
			}
			binaryWriter.Flush();
			long value = stream.Position - position;
			binaryWriter.Write(value);
		}

		public void ReadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new global::System.ArgumentNullException("filePath");
			}
			using global::System.IO.FileStream stream = global::System.IO.File.OpenRead(filePath);
			ReadFrom(stream);
		}

		public unsafe void ReadFrom(global::System.IO.Stream stream)
		{
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new global::System.ArgumentException("Stream does not support reading", "stream");
			}
			global::System.IO.BinaryReader binaryReader = new global::System.IO.BinaryReader(stream);
			if (binaryReader.ReadInt32() != kFileFormat)
			{
				throw new global::System.IO.IOException($"Stream does not appear to be an InputEventTrace (no '{kFileFormat}' code)");
			}
			if (binaryReader.ReadInt32() > kFileVersion)
			{
				throw new global::System.IO.IOException($"Stream is an InputEventTrace but a newer version (expected version {kFileVersion} or below)");
			}
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			ulong num = binaryReader.ReadUInt64();
			ulong num2 = binaryReader.ReadUInt64();
			byte* eventBuffer = m_EventBuffer;
			if (num != 0 && num2 != 0)
			{
				byte* ptr;
				if (m_EventBuffer != null && m_EventBufferSize >= (long)num2)
				{
					ptr = m_EventBuffer;
				}
				else
				{
					ptr = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc((long)num2, 4, global::Unity.Collections.Allocator.Persistent);
					m_EventBufferSize = (long)num2;
				}
				try
				{
					byte* ptr2 = ptr;
					byte* ptr3 = ptr2 + num2;
					long num3 = 0L;
					for (ulong num4 = 0uL; num4 < num; num4++)
					{
						int num5 = binaryReader.ReadInt32();
						uint num6 = binaryReader.ReadUInt16();
						uint num7 = binaryReader.ReadUInt16();
						if (num6 > ptr3 - ptr2)
						{
							break;
						}
						*(int*)ptr2 = num5;
						ptr2 += 4;
						*(ushort*)ptr2 = (ushort)num6;
						ptr2 += 2;
						*(ushort*)ptr2 = (ushort)num7;
						ptr2 += 2;
						int num8 = (int)(num6 - 4 - 2 - 2);
						byte[] array = binaryReader.ReadBytes(num8);
						fixed (byte* source = array)
						{
							global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr2, source, num8);
						}
						ptr2 += global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num8, 4);
						num3 += global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num6, 4u);
						if (ptr2 >= ptr3)
						{
							break;
						}
					}
					int num9 = binaryReader.ReadInt32();
					global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo[] array2 = new global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo[num9];
					for (int i = 0; i < num9; i++)
					{
						array2[i] = new global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo
						{
							deviceId = binaryReader.ReadInt32(),
							layout = binaryReader.ReadString(),
							stateFormat = binaryReader.ReadInt32(),
							stateSizeInBytes = binaryReader.ReadInt32(),
							m_FullLayoutJson = binaryReader.ReadString()
						};
					}
					m_EventBuffer = ptr;
					m_EventBufferHead = m_EventBuffer;
					m_EventBufferTail = ptr3;
					m_EventCount = (long)num;
					m_EventSizeInBytes = num3;
					m_DeviceInfos = array2;
				}
				catch
				{
					if (ptr != eventBuffer)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(ptr, global::Unity.Collections.Allocator.Persistent);
					}
					throw;
				}
			}
			else
			{
				m_EventBuffer = null;
				m_EventBufferHead = null;
				m_EventBufferTail = null;
			}
			if (m_EventBuffer != eventBuffer && eventBuffer != null)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(eventBuffer, global::Unity.Collections.Allocator.Persistent);
			}
			m_ChangeCounter++;
		}

		public static global::UnityEngine.InputSystem.LowLevel.InputEventTrace LoadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new global::System.ArgumentNullException("filePath");
			}
			using global::System.IO.FileStream stream = global::System.IO.File.OpenRead(filePath);
			return LoadFrom(stream);
		}

		public static global::UnityEngine.InputSystem.LowLevel.InputEventTrace LoadFrom(global::System.IO.Stream stream)
		{
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new global::System.ArgumentException("Stream must be readable", "stream");
			}
			global::UnityEngine.InputSystem.LowLevel.InputEventTrace inputEventTrace = new global::UnityEngine.InputSystem.LowLevel.InputEventTrace(1048576L, growBuffer: false, -1L, -1L);
			inputEventTrace.ReadFrom(stream);
			return inputEventTrace;
		}

		public global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController Replay()
		{
			Disable();
			return new global::UnityEngine.InputSystem.LowLevel.InputEventTrace.ReplayController(this);
		}

		public unsafe bool Resize(long newBufferSize, long newMaxBufferSize = -1L)
		{
			if (newBufferSize <= 0)
			{
				throw new global::System.ArgumentException("Size must be positive", "newBufferSize");
			}
			if (m_EventBufferSize == newBufferSize)
			{
				return true;
			}
			if (newMaxBufferSize < newBufferSize)
			{
				newMaxBufferSize = newBufferSize;
			}
			byte* ptr = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(newBufferSize, 4, global::Unity.Collections.Allocator.Persistent);
			if (ptr == null)
			{
				return false;
			}
			if (m_EventCount > 0)
			{
				if (newBufferSize < m_EventBufferSize || m_HasWrapped)
				{
					global::UnityEngine.InputSystem.LowLevel.InputEventPtr current = new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)m_EventBufferHead);
					global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr2 = (global::UnityEngine.InputSystem.LowLevel.InputEvent*)ptr;
					int num = 0;
					int num2 = 0;
					long num3 = m_EventSizeInBytes;
					for (int i = 0; i < m_EventCount; i++)
					{
						uint sizeInBytes = current.sizeInBytes;
						uint num4 = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(sizeInBytes, 4u);
						if (num3 <= newBufferSize)
						{
							global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr2, current.ToPointer(), sizeInBytes);
							ptr2 = global::UnityEngine.InputSystem.LowLevel.InputEvent.GetNextInMemory(ptr2);
							num2 += (int)num4;
							num++;
						}
						num3 -= num4;
						if (!GetNextEvent(ref current))
						{
							break;
						}
					}
					m_HasWrapped = false;
					m_EventCount = num;
					m_EventSizeInBytes = num2;
				}
				else
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, m_EventBufferHead, m_EventSizeInBytes);
				}
			}
			if (m_EventBuffer != null)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(m_EventBuffer, global::Unity.Collections.Allocator.Persistent);
			}
			m_EventBufferSize = newBufferSize;
			m_EventBuffer = ptr;
			m_EventBufferHead = ptr;
			m_EventBufferTail = m_EventBuffer + m_EventSizeInBytes;
			m_MaxEventBufferSize = newMaxBufferSize;
			m_ChangeCounter++;
			return true;
		}

		public unsafe void Clear()
		{
			byte* eventBufferHead = (m_EventBufferTail = default(byte*));
			m_EventBufferHead = eventBufferHead;
			m_EventCount = 0L;
			m_EventSizeInBytes = 0L;
			m_ChangeCounter++;
			m_DeviceInfos = null;
		}

		public unsafe void Enable()
		{
			if (!m_Enabled)
			{
				if (m_EventBuffer == null)
				{
					Allocate();
				}
				global::UnityEngine.InputSystem.InputSystem.onEvent += new global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice>(OnInputEvent);
				if (m_RecordFrameMarkers)
				{
					global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate += OnBeforeUpdate;
				}
				m_Enabled = true;
			}
		}

		public void Disable()
		{
			if (m_Enabled)
			{
				global::UnityEngine.InputSystem.InputSystem.onEvent -= new global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice>(OnInputEvent);
				global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate -= OnBeforeUpdate;
				m_Enabled = false;
			}
		}

		public unsafe bool GetNextEvent(ref global::UnityEngine.InputSystem.LowLevel.InputEventPtr current)
		{
			if (m_EventBuffer == null)
			{
				return false;
			}
			if (m_EventBufferHead == null)
			{
				return false;
			}
			if (!current.valid)
			{
				current = new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)m_EventBufferHead);
				return true;
			}
			byte* ptr = (byte*)current.Next().data;
			byte* ptr2 = m_EventBuffer + m_EventBufferSize;
			if (ptr == m_EventBufferTail)
			{
				return false;
			}
			if (ptr2 - ptr < 20 || ((global::UnityEngine.InputSystem.LowLevel.InputEvent*)ptr)->sizeInBytes == 0)
			{
				ptr = m_EventBuffer;
				if (ptr == current.ToPointer())
				{
					return false;
				}
			}
			current = new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)ptr);
			return true;
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputEventTrace.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			Disable();
			Release();
		}

		private unsafe void Allocate()
		{
			m_EventBuffer = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(m_EventBufferSize, 4, global::Unity.Collections.Allocator.Persistent);
		}

		private unsafe void Release()
		{
			Clear();
			if (m_EventBuffer != null)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(m_EventBuffer, global::Unity.Collections.Allocator.Persistent);
				m_EventBuffer = null;
			}
		}

		private unsafe void OnBeforeUpdate()
		{
			if (m_RecordFrameMarkers)
			{
				global::UnityEngine.InputSystem.LowLevel.InputEvent output = new global::UnityEngine.InputSystem.LowLevel.InputEvent
				{
					type = FrameMarkerEvent,
					internalTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime,
					sizeInBytes = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.InputEvent>()
				};
				OnInputEvent(new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output)), null);
			}
		}

		private unsafe void OnInputEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent, global::UnityEngine.InputSystem.InputDevice device)
		{
			if (inputEvent.handled || (m_DeviceId != 0 && inputEvent.deviceId != m_DeviceId && inputEvent.type != FrameMarkerEvent) || (m_OnFilterEvent != null && !m_OnFilterEvent(inputEvent, device)) || m_EventBuffer == null)
			{
				return;
			}
			uint num = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(inputEvent.sizeInBytes, 4u);
			if (num > m_MaxEventBufferSize)
			{
				return;
			}
			if (m_EventBufferTail == null)
			{
				m_EventBufferHead = m_EventBuffer;
				m_EventBufferTail = m_EventBuffer;
			}
			byte* ptr = m_EventBufferTail + num;
			bool flag = ptr > m_EventBufferHead && m_EventBufferHead != m_EventBuffer;
			if (ptr > m_EventBuffer + m_EventBufferSize)
			{
				if (m_EventBufferSize < m_MaxEventBufferSize && !m_HasWrapped)
				{
					long num2 = global::System.Math.Max(m_GrowIncrementSize, global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num, 4u));
					long num3 = m_EventBufferSize + num2;
					if (num3 > m_MaxEventBufferSize)
					{
						num3 = m_MaxEventBufferSize;
					}
					if (num3 < num)
					{
						return;
					}
					Resize(num3, -1L);
					ptr = m_EventBufferTail + num;
				}
				long num4 = m_EventBufferSize - (m_EventBufferTail - m_EventBuffer);
				if (num4 < num)
				{
					m_HasWrapped = true;
					if (num4 >= 20)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(m_EventBufferTail, 20L);
					}
					m_EventBufferTail = m_EventBuffer;
					ptr = m_EventBuffer + num;
					if (flag)
					{
						m_EventBufferHead = m_EventBuffer;
					}
					flag = ptr > m_EventBufferHead;
				}
			}
			if (flag)
			{
				byte* ptr2 = m_EventBufferHead;
				byte* ptr3 = m_EventBuffer + m_EventBufferSize - 20;
				while (ptr2 < ptr)
				{
					uint sizeInBytes = ((global::UnityEngine.InputSystem.LowLevel.InputEvent*)ptr2)->sizeInBytes;
					ptr2 += sizeInBytes;
					m_EventCount--;
					m_EventSizeInBytes -= sizeInBytes;
					if (ptr2 > ptr3 || ((global::UnityEngine.InputSystem.LowLevel.InputEvent*)ptr2)->sizeInBytes == 0)
					{
						ptr2 = m_EventBuffer;
						break;
					}
				}
				m_EventBufferHead = ptr2;
			}
			byte* eventBufferTail = m_EventBufferTail;
			m_EventBufferTail = ptr;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(eventBufferTail, inputEvent.data, inputEvent.sizeInBytes);
			m_ChangeCounter++;
			m_EventCount++;
			m_EventSizeInBytes += num;
			if (device != null)
			{
				bool flag2 = false;
				if (m_DeviceInfos != null)
				{
					for (int i = 0; i < m_DeviceInfos.Length; i++)
					{
						if (m_DeviceInfos[i].deviceId == device.deviceId)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_DeviceInfos, new global::UnityEngine.InputSystem.LowLevel.InputEventTrace.DeviceInfo
					{
						m_DeviceId = device.deviceId,
						m_Layout = device.layout,
						m_StateFormat = device.stateBlock.format,
						m_StateSizeInBytes = (int)device.stateBlock.alignedSizeInBytes,
						m_FullLayoutJson = (global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.IsGeneratedLayout(device.m_Layout) ? global::UnityEngine.InputSystem.InputSystem.LoadLayout(device.layout).ToJson() : null)
					});
				}
			}
			if (m_EventListeners.length > 0)
			{
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_EventListeners, new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventBufferTail), "InputEventTrace.onEvent");
			}
		}
	}
}
