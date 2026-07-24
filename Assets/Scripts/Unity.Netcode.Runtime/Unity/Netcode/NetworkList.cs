namespace Unity.Netcode
{
	[global::Unity.Netcode.GenerateSerializationForGenericParameter(0)]
	public class NetworkList<T> : global::Unity.Netcode.NetworkVariableBase where T : unmanaged, global::System.IEquatable<T>
	{
		public delegate void OnListChangedDelegate(global::Unity.Netcode.NetworkListEvent<T> changeEvent);

		private global::Unity.Collections.NativeList<T> m_List = new global::Unity.Collections.NativeList<T>(64, global::Unity.Collections.Allocator.Persistent);

		private global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> m_DirtyEvents = new global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>>(64, global::Unity.Collections.Allocator.Persistent);

		public int Count => m_List.Length;

		public T this[int index]
		{
			get
			{
				return m_List[index];
			}
			set
			{
				Set(index, value);
			}
		}

		public int LastModifiedTick => int.MinValue;

		public event global::Unity.Netcode.NetworkList<T>.OnListChangedDelegate OnListChanged;

		public NetworkList()
		{
		}

		public NetworkList(global::System.Collections.Generic.IEnumerable<T> values = null, global::Unity.Netcode.NetworkVariableReadPermission readPerm = global::Unity.Netcode.NetworkVariableReadPermission.Everyone, global::Unity.Netcode.NetworkVariableWritePermission writePerm = global::Unity.Netcode.NetworkVariableWritePermission.Server)
			: base(readPerm, writePerm)
		{
			if (values == null)
			{
				return;
			}
			foreach (T value2 in values)
			{
				T value = value2;
				m_List.Add(in value);
			}
		}

		~NetworkList()
		{
			Dispose();
		}

		public override void ResetDirty()
		{
			base.ResetDirty();
			if (m_DirtyEvents.Length > 0)
			{
				m_DirtyEvents.Clear();
			}
		}

		public override bool IsDirty()
		{
			if (!base.IsDirty())
			{
				return m_DirtyEvents.Length > 0;
			}
			return true;
		}

		internal void MarkNetworkObjectDirty()
		{
			MarkNetworkBehaviourDirty();
		}

		public override void WriteDelta(global::Unity.Netcode.FastBufferWriter writer)
		{
			if (base.IsDirty())
			{
				writer.WriteValueSafe<ushort>((ushort)1, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				writer.WriteValueSafe<global::Unity.Netcode.NetworkListEvent<T>.EventType>(in ILSpyHelper_AsRefReadOnly(global::Unity.Netcode.NetworkListEvent<T>.EventType.Full), default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				WriteField(writer);
				return;
			}
			writer.WriteValueSafe<ushort>((ushort)m_DirtyEvents.Length, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < m_DirtyEvents.Length; i++)
			{
				global::Unity.Netcode.NetworkListEvent<T> networkListEvent = m_DirtyEvents.ElementAt(i);
				writer.WriteValueSafe(in networkListEvent.Type, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				switch (networkListEvent.Type)
				{
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Add:
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Write(writer, ref networkListEvent.Value);
					break;
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Insert:
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, networkListEvent.Index);
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Write(writer, ref networkListEvent.Value);
					break;
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Remove:
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Write(writer, ref networkListEvent.Value);
					break;
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.RemoveAt:
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, networkListEvent.Index);
					break;
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Value:
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, networkListEvent.Index);
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Write(writer, ref networkListEvent.Value);
					break;
				}
			}
			static ref readonly T ILSpyHelper_AsRefReadOnly<T>(in T temp)
			{
				//ILSpy generated this function to help ensure overload resolution can pick the overload using 'in'
				return ref temp;
			}
		}

		public override void WriteField(global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteValueSafe<ushort>((ushort)m_List.Length, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < m_List.Length; i++)
			{
				global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Write(writer, ref m_List.ElementAt(i));
			}
		}

		public override void ReadField(global::Unity.Netcode.FastBufferReader reader)
		{
			m_List.Clear();
			reader.ReadValueSafe(out ushort value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value; i++)
			{
				T value2 = new T();
				global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Read(reader, ref value2);
				m_List.Add(in value2);
			}
		}

		public override void ReadDelta(global::Unity.Netcode.FastBufferReader reader, bool keepDirtyDelta)
		{
			bool isServer = m_NetworkManager.IsServer;
			reader.ReadValueSafe(out ushort value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value; i++)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.NetworkListEvent<T>.EventType value2, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				switch (value2)
				{
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Add:
				{
					T value6 = new T();
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Read(reader, ref value6);
					m_List.Add(in value6);
					if (this.OnListChanged != null)
					{
						this.OnListChanged(new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = m_List.Length - 1,
							Value = m_List[m_List.Length - 1]
						});
					}
					if (isServer)
					{
						ref global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> dirtyEvents3 = ref m_DirtyEvents;
						global::Unity.Netcode.NetworkListEvent<T> value3 = new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = m_List.Length - 1,
							Value = m_List[m_List.Length - 1]
						};
						dirtyEvents3.Add(in value3);
						if (keepDirtyDelta)
						{
							MarkNetworkObjectDirty();
						}
					}
					break;
				}
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Insert:
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value4);
					T value5 = new T();
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Read(reader, ref value5);
					if (value4 < m_List.Length)
					{
						m_List.InsertRangeWithBeginEnd(value4, value4 + 1);
						m_List[value4] = value5;
					}
					else
					{
						m_List.Add(in value5);
					}
					if (this.OnListChanged != null)
					{
						this.OnListChanged(new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = value4,
							Value = m_List[value4]
						});
					}
					if (isServer)
					{
						ref global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> dirtyEvents2 = ref m_DirtyEvents;
						global::Unity.Netcode.NetworkListEvent<T> value3 = new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = value4,
							Value = m_List[value4]
						};
						dirtyEvents2.Add(in value3);
						if (keepDirtyDelta)
						{
							MarkNetworkObjectDirty();
						}
					}
					break;
				}
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Remove:
				{
					T value7 = new T();
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Read(reader, ref value7);
					int num = global::Unity.Collections.NativeListExtensions.IndexOf(m_List, value7);
					if (num == -1)
					{
						break;
					}
					m_List.RemoveAt(num);
					if (this.OnListChanged != null)
					{
						this.OnListChanged(new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = num,
							Value = value7
						});
					}
					if (isServer)
					{
						ref global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> dirtyEvents4 = ref m_DirtyEvents;
						global::Unity.Netcode.NetworkListEvent<T> value3 = new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = num,
							Value = value7
						};
						dirtyEvents4.Add(in value3);
						if (keepDirtyDelta)
						{
							MarkNetworkObjectDirty();
						}
					}
					break;
				}
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.RemoveAt:
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value8);
					T value9 = m_List[value8];
					m_List.RemoveAt(value8);
					if (this.OnListChanged != null)
					{
						this.OnListChanged(new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = value8,
							Value = value9
						});
					}
					if (isServer)
					{
						ref global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> dirtyEvents5 = ref m_DirtyEvents;
						global::Unity.Netcode.NetworkListEvent<T> value3 = new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = value8,
							Value = value9
						};
						dirtyEvents5.Add(in value3);
						if (keepDirtyDelta)
						{
							MarkNetworkObjectDirty();
						}
					}
					break;
				}
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Value:
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value10);
					T value11 = new T();
					global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer.Read(reader, ref value11);
					if (value10 >= m_List.Length)
					{
						throw new global::System.Exception("Shouldn't be here, index is higher than list length");
					}
					T previousValue = m_List[value10];
					m_List[value10] = value11;
					if (this.OnListChanged != null)
					{
						this.OnListChanged(new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = value10,
							Value = value11,
							PreviousValue = previousValue
						});
					}
					if (isServer)
					{
						ref global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> dirtyEvents6 = ref m_DirtyEvents;
						global::Unity.Netcode.NetworkListEvent<T> value3 = new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2,
							Index = value10,
							Value = value11,
							PreviousValue = previousValue
						};
						dirtyEvents6.Add(in value3);
						if (keepDirtyDelta)
						{
							MarkNetworkObjectDirty();
						}
					}
					break;
				}
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Clear:
					m_List.Clear();
					if (this.OnListChanged != null)
					{
						this.OnListChanged(new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2
						});
					}
					if (isServer)
					{
						ref global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkListEvent<T>> dirtyEvents = ref m_DirtyEvents;
						global::Unity.Netcode.NetworkListEvent<T> value3 = new global::Unity.Netcode.NetworkListEvent<T>
						{
							Type = value2
						};
						dirtyEvents.Add(in value3);
						if (keepDirtyDelta)
						{
							MarkNetworkObjectDirty();
						}
					}
					break;
				case global::Unity.Netcode.NetworkListEvent<T>.EventType.Full:
					ReadField(reader);
					ResetDirty();
					break;
				}
			}
		}

		internal override void PostDeltaRead()
		{
			if (m_NetworkManager.IsServer)
			{
				ResetDirty();
			}
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return m_List.GetEnumerator();
		}

		public void Add(T item)
		{
			if (CannotWrite())
			{
				LogWritePermissionError();
				return;
			}
			m_List.Add(in item);
			global::Unity.Netcode.NetworkListEvent<T> listEvent = new global::Unity.Netcode.NetworkListEvent<T>
			{
				Type = global::Unity.Netcode.NetworkListEvent<T>.EventType.Add,
				Value = item,
				Index = m_List.Length - 1
			};
			HandleAddListEvent(listEvent);
		}

		public void Clear()
		{
			if (CannotWrite())
			{
				LogWritePermissionError();
				return;
			}
			m_List.Clear();
			global::Unity.Netcode.NetworkListEvent<T> listEvent = new global::Unity.Netcode.NetworkListEvent<T>
			{
				Type = global::Unity.Netcode.NetworkListEvent<T>.EventType.Clear
			};
			HandleAddListEvent(listEvent);
		}

		public bool Contains(T item)
		{
			return global::Unity.Collections.NativeListExtensions.IndexOf(m_List, item) != -1;
		}

		public bool Remove(T item)
		{
			if (CannotWrite())
			{
				LogWritePermissionError();
				return false;
			}
			int num = global::Unity.Collections.NativeListExtensions.IndexOf(m_List, item);
			if (num == -1)
			{
				return false;
			}
			m_List.RemoveAt(num);
			global::Unity.Netcode.NetworkListEvent<T> listEvent = new global::Unity.Netcode.NetworkListEvent<T>
			{
				Type = global::Unity.Netcode.NetworkListEvent<T>.EventType.Remove,
				Value = item
			};
			HandleAddListEvent(listEvent);
			return true;
		}

		public int IndexOf(T item)
		{
			return global::Unity.Collections.NativeListExtensions.IndexOf(m_List, item);
		}

		public void Insert(int index, T item)
		{
			if (CannotWrite())
			{
				LogWritePermissionError();
				return;
			}
			if (index < m_List.Length)
			{
				m_List.InsertRangeWithBeginEnd(index, index + 1);
				m_List[index] = item;
			}
			else
			{
				m_List.Add(in item);
			}
			global::Unity.Netcode.NetworkListEvent<T> listEvent = new global::Unity.Netcode.NetworkListEvent<T>
			{
				Type = global::Unity.Netcode.NetworkListEvent<T>.EventType.Insert,
				Index = index,
				Value = item
			};
			HandleAddListEvent(listEvent);
		}

		public void RemoveAt(int index)
		{
			if (CannotWrite())
			{
				throw new global::System.InvalidOperationException("Client is not allowed to write to this NetworkList");
			}
			T value = m_List[index];
			m_List.RemoveAt(index);
			global::Unity.Netcode.NetworkListEvent<T> listEvent = new global::Unity.Netcode.NetworkListEvent<T>
			{
				Type = global::Unity.Netcode.NetworkListEvent<T>.EventType.RemoveAt,
				Index = index,
				Value = value
			};
			HandleAddListEvent(listEvent);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Set(int index, T value, bool forceUpdate = false)
		{
			if (CannotWrite())
			{
				LogWritePermissionError();
				return;
			}
			T a = m_List[index];
			if (forceUpdate || !global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref a, ref value))
			{
				m_List[index] = value;
				global::Unity.Netcode.NetworkListEvent<T> listEvent = new global::Unity.Netcode.NetworkListEvent<T>
				{
					Type = global::Unity.Netcode.NetworkListEvent<T>.EventType.Value,
					Index = index,
					Value = value,
					PreviousValue = a
				};
				HandleAddListEvent(listEvent);
			}
		}

		public global::Unity.Collections.NativeArray<T>.ReadOnly AsNativeArray()
		{
			return m_List.AsReadOnly();
		}

		private void HandleAddListEvent(global::Unity.Netcode.NetworkListEvent<T> listEvent)
		{
			m_DirtyEvents.Add(in listEvent);
			MarkNetworkObjectDirty();
			this.OnListChanged?.Invoke(listEvent);
		}

		public override void Dispose()
		{
			if (m_List.IsCreated)
			{
				m_List.Dispose();
			}
			if (m_DirtyEvents.IsCreated)
			{
				m_DirtyEvents.Dispose();
			}
			base.Dispose();
		}
	}
}
