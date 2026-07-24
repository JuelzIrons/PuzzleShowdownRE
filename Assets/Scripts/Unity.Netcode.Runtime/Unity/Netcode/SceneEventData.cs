namespace Unity.Netcode
{
	internal class SceneEventData : global::System.IDisposable
	{
		internal global::Unity.Netcode.SceneEventType SceneEventType;

		internal global::UnityEngine.SceneManagement.LoadSceneMode LoadSceneMode;

		internal global::Unity.Netcode.ForceNetworkSerializeByMemcpy<global::System.Guid> SceneEventProgressId;

		internal uint SceneEventId;

		internal uint ActiveSceneHash;

		internal uint SceneHash;

		internal global::Unity.Netcode.NetworkSceneHandle SceneHandle;

		internal uint ClientSceneHash;

		internal global::Unity.Netcode.NetworkSceneHandle NetworkSceneHandle;

		internal ulong TargetClientId;

		internal ulong SenderClientId;

		private global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> m_SceneNetworkObjects;

		private global::System.Collections.Generic.Dictionary<uint, long> m_SceneNetworkObjectDataOffsets;

		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> m_NetworkObjectsSync = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> m_DespawnedInSceneObjectsSync = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();

		private global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<uint>> m_DespawnedInSceneObjects = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<uint>>();

		private global::System.Collections.Generic.List<ulong> m_NetworkObjectsToBeRemoved = new global::System.Collections.Generic.List<ulong>();

		private bool m_HasInternalBuffer;

		internal global::Unity.Netcode.FastBufferReader InternalBuffer;

		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		internal global::System.Collections.Generic.List<ulong> ClientsCompleted;

		internal global::System.Collections.Generic.List<ulong> ClientsTimedOut;

		internal global::System.Collections.Generic.Queue<uint> ScenesToSynchronize;

		internal global::System.Collections.Generic.Queue<global::Unity.Netcode.NetworkSceneHandle> SceneHandlesToSynchronize;

		internal global::UnityEngine.SceneManagement.LoadSceneMode ClientSynchronizationMode;

		internal static bool LogSerializationOrder;

		internal bool EnableSerializationLogs;

		internal bool ForwardSynchronization;

		private int m_InternalBufferSize;

		internal bool IsForwarding;

		private ulong m_OwnerId;

		internal void AddSceneToSynchronize(uint sceneHash, global::Unity.Netcode.NetworkSceneHandle sceneHandle)
		{
			ScenesToSynchronize.Enqueue(sceneHash);
			SceneHandlesToSynchronize.Enqueue(sceneHandle);
		}

		internal uint GetNextSceneSynchronizationHash()
		{
			return ScenesToSynchronize.Dequeue();
		}

		internal global::Unity.Netcode.NetworkSceneHandle GetNextSceneSynchronizationHandle()
		{
			return SceneHandlesToSynchronize.Dequeue();
		}

		internal bool IsDoneWithSynchronization()
		{
			if (ScenesToSynchronize.Count == 0 && SceneHandlesToSynchronize.Count == 0)
			{
				return true;
			}
			if (ScenesToSynchronize.Count != SceneHandlesToSynchronize.Count)
			{
				throw new global::System.Exception("[SceneEventData-Internal Mismatch Error] ScenesToSynchronize count != SceneHandlesToSynchronize count!");
			}
			return false;
		}

		internal void InitializeForSynch()
		{
			if (m_SceneNetworkObjects == null)
			{
				m_SceneNetworkObjects = new global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>();
			}
			else
			{
				m_SceneNetworkObjects.Clear();
			}
			if (ScenesToSynchronize == null)
			{
				ScenesToSynchronize = new global::System.Collections.Generic.Queue<uint>();
			}
			else
			{
				ScenesToSynchronize.Clear();
			}
			if (SceneHandlesToSynchronize == null)
			{
				SceneHandlesToSynchronize = new global::System.Collections.Generic.Queue<global::Unity.Netcode.NetworkSceneHandle>();
			}
			else
			{
				SceneHandlesToSynchronize.Clear();
			}
			ForwardSynchronization = false;
		}

		private int SortChildrenNetworkObjects(global::Unity.Netcode.NetworkObject first, global::Unity.Netcode.NetworkObject second)
		{
			global::Unity.Netcode.NetworkObject networkObject = first.GetCachedParent()?.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (networkObject != null && networkObject == second)
			{
				return 1;
			}
			global::Unity.Netcode.NetworkObject networkObject2 = second.GetCachedParent()?.GetComponent<global::Unity.Netcode.NetworkObject>();
			if (networkObject2 != null && networkObject2 == first)
			{
				return -1;
			}
			return 0;
		}

		private void SortParentedNetworkObjects()
		{
			foreach (global::Unity.Netcode.NetworkObject item in global::System.Linq.Enumerable.ToList(m_NetworkObjectsSync))
			{
				if (item.transform.childCount <= 0 || !(item.transform.parent == null))
				{
					continue;
				}
				global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> list = global::System.Linq.Enumerable.ToList(item.GetComponentsInChildren<global::Unity.Netcode.NetworkObject>());
				list.Sort(SortChildrenNetworkObjects);
				list.Remove(item);
				foreach (global::Unity.Netcode.NetworkObject item2 in list)
				{
					m_NetworkObjectsSync.Remove(item2);
				}
				int num = m_NetworkObjectsSync.IndexOf(item) + 1;
				if (num == m_NetworkObjectsSync.Count)
				{
					m_NetworkObjectsSync.AddRange(list);
				}
				else
				{
					m_NetworkObjectsSync.InsertRange(num, list);
				}
			}
		}

		internal void AddSpawnedNetworkObjects()
		{
			m_NetworkObjectsSync.Clear();
			bool flag = m_NetworkManager.DistributedAuthorityMode && TargetClientId == 0;
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in m_NetworkManager.SpawnManager.SpawnedObjectsList)
			{
				global::Unity.Netcode.NetworkObject networkObject = spawnedObjects;
				if ((TargetClientId == 0L || !m_NetworkManager.SpawnManager.IsObjectVisibilityPending(TargetClientId, ref networkObject)) && (spawnedObjects.Observers.Contains(TargetClientId) || flag))
				{
					m_NetworkObjectsSync.Add(spawnedObjects);
				}
			}
			SortObjectsToSync();
		}

		private void SortObjectsToSync()
		{
			m_NetworkObjectsSync.Sort(SortNetworkObjects);
			SortParentedNetworkObjects();
			if (!LogSerializationOrder || m_NetworkManager.LogLevel != global::Unity.Netcode.LogLevel.Developer)
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(65535);
			stringBuilder.AppendLine("[Server-Side Client-Synchronization] NetworkObject serialization order:");
			foreach (global::Unity.Netcode.NetworkObject item in m_NetworkObjectsSync)
			{
				stringBuilder.AppendLine(item.name ?? "");
			}
			global::Unity.Netcode.NetworkLog.LogInfo(stringBuilder.ToString());
		}

		internal void AddDespawnedInSceneNetworkObjects()
		{
			m_DespawnedInSceneObjectsSync.Clear();
			foreach (global::Unity.Netcode.NetworkObject item in global::System.Linq.Enumerable.Where(global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: true, orderByIdentifier: true), (global::Unity.Netcode.NetworkObject c) => c.NetworkManager == m_NetworkManager))
			{
				if (item.IsSceneObject.HasValue && item.IsSceneObject.Value && !item.IsSpawned)
				{
					item.NetworkManagerOwner = m_NetworkManager;
					m_DespawnedInSceneObjectsSync.Add(item);
				}
			}
		}

		internal void AddNetworkObjectForSynch(uint sceneIndex, global::Unity.Netcode.NetworkObject networkObject)
		{
			if (!m_SceneNetworkObjects.ContainsKey(sceneIndex))
			{
				m_SceneNetworkObjects.Add(sceneIndex, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
			}
			m_SceneNetworkObjects[sceneIndex].Add(networkObject);
		}

		internal bool IsSceneEventClientSide()
		{
			global::Unity.Netcode.SceneEventType sceneEventType = SceneEventType;
			if (sceneEventType <= global::Unity.Netcode.SceneEventType.UnloadEventCompleted || sceneEventType - 9 <= global::Unity.Netcode.SceneEventType.Unload)
			{
				return true;
			}
			return false;
		}

		private int SortNetworkObjects(global::Unity.Netcode.NetworkObject first, global::Unity.Netcode.NetworkObject second)
		{
			bool flag = m_NetworkManager.PrefabHandler.ContainsHandler(first);
			bool flag2 = m_NetworkManager.PrefabHandler.ContainsHandler(second);
			if (flag != flag2)
			{
				if (flag)
				{
					return 1;
				}
				return -1;
			}
			return 0;
		}

		private void LogArray(byte[] data, int start = 0, int stop = 0, global::System.Text.StringBuilder builder = null)
		{
			bool flag = builder != null;
			if (!flag)
			{
				builder = new global::System.Text.StringBuilder();
			}
			if (stop == 0)
			{
				stop = data.Length;
			}
			builder.AppendLine($"[Start Data Dump][Start = {start}][Stop = {stop}] Size ({stop - start})");
			for (int i = start; i < stop; i++)
			{
				builder.Append($"{data[i]:X2} ");
			}
			builder.Append("\n");
			if (!flag)
			{
				global::UnityEngine.Debug.Log(builder.ToString());
			}
		}

		internal void Serialize(global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteValueSafe(in SceneEventType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			if (m_NetworkManager.DistributedAuthorityMode)
			{
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, TargetClientId);
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, SenderClientId);
			}
			if (SceneEventType == global::Unity.Netcode.SceneEventType.ActiveSceneChanged)
			{
				writer.WriteValueSafe(in ActiveSceneHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				return;
			}
			if (SceneEventType == global::Unity.Netcode.SceneEventType.ObjectSceneChanged)
			{
				SerializeObjectsMovedIntoNewScene(writer);
				return;
			}
			writer.WriteValueSafe<byte>((byte)LoadSceneMode, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (SceneEventType != global::Unity.Netcode.SceneEventType.Synchronize)
			{
				writer.WriteValueSafe(in SceneEventProgressId, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
			}
			else
			{
				writer.WriteValueSafe(in ClientSynchronizationMode, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			}
			writer.WriteValueSafe(in SceneHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe(in SceneHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			switch (SceneEventType)
			{
			case global::Unity.Netcode.SceneEventType.Synchronize:
				writer.WriteValueSafe(in ActiveSceneHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				WriteSceneSynchronizationData(writer);
				if (EnableSerializationLogs)
				{
					LogArray(writer.ToArray(), 0, writer.Length);
				}
				break;
			case global::Unity.Netcode.SceneEventType.Load:
				if (m_NetworkManager.DistributedAuthorityMode && IsForwarding && m_NetworkManager.DAHost)
				{
					CopyInternalBuffer(ref writer);
				}
				else
				{
					SerializeScenePlacedObjects(writer);
				}
				break;
			case global::Unity.Netcode.SceneEventType.SynchronizeComplete:
				WriteClientSynchronizationResults(writer);
				break;
			case global::Unity.Netcode.SceneEventType.ReSynchronize:
				WriteClientReSynchronizationData(writer);
				break;
			case global::Unity.Netcode.SceneEventType.LoadEventCompleted:
			case global::Unity.Netcode.SceneEventType.UnloadEventCompleted:
				WriteSceneEventProgressDone(writer);
				break;
			case global::Unity.Netcode.SceneEventType.Unload:
			case global::Unity.Netcode.SceneEventType.LoadComplete:
			case global::Unity.Netcode.SceneEventType.UnloadComplete:
				break;
			}
		}

		private unsafe void CopyInternalBuffer(ref global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteBytesSafe(InternalBuffer.GetUnsafePtrAtCurrentPosition(), InternalBuffer.Length);
		}

		internal void WriteSceneSynchronizationData(global::Unity.Netcode.FastBufferWriter writer)
		{
			global::System.Text.StringBuilder stringBuilder = null;
			if (EnableSerializationLogs)
			{
				stringBuilder = new global::System.Text.StringBuilder();
				stringBuilder.AppendLine($"[Write][Synchronize-Start][WPos: {writer.Position}] Begin:");
			}
			writer.WriteValueSafe(ScenesToSynchronize.ToArray(), default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe(SceneHandlesToSynchronize.ToArray(), default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			int position = writer.Position;
			if (m_NetworkManager.DistributedAuthorityMode && ForwardSynchronization && m_NetworkManager.DAHost)
			{
				writer.WriteValueSafe(in m_InternalBufferSize, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				CopyInternalBuffer(ref writer);
				if (EnableSerializationLogs)
				{
					LogArray(writer.ToArray(), position);
				}
				return;
			}
			writer.WriteValueSafe<int>(0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			int num = 0;
			writer.WriteValueSafe<int>(m_NetworkObjectsSync.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (EnableSerializationLogs)
			{
				stringBuilder.AppendLine($"[Synchronize Objects][positionStart: {position}][WPos: {writer.Position}][NO-Count: {m_NetworkObjectsSync.Count}] Begin:");
			}
			bool distributedAuthorityMode = m_NetworkManager.DistributedAuthorityMode;
			for (int i = 0; i < m_NetworkObjectsSync.Count; i++)
			{
				global::Unity.Netcode.NetworkObject networkObject = m_NetworkObjectsSync[i];
				int position2 = writer.Position;
				m_NetworkObjectsSync[i].Serialize(TargetClientId, distributedAuthorityMode).Serialize(writer);
				int position3 = writer.Position;
				num += position3 - position2;
				if (EnableSerializationLogs)
				{
					int num2 = position2 - (position + 4);
					int num3 = position3 - (position + 4);
					stringBuilder.AppendLine($"[Head: {num2}][Tail: {num3}][Size: {num3 - num2}][{networkObject.name}][NID-{networkObject.NetworkObjectId}][Children: {networkObject.ChildNetworkBehaviours.Count}]");
					LogArray(writer.ToArray(), position2, position3, stringBuilder);
				}
			}
			if (EnableSerializationLogs)
			{
				global::UnityEngine.Debug.Log(stringBuilder.ToString());
			}
			writer.WriteValueSafe<int>(m_DespawnedInSceneObjectsSync.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int j = 0; j < m_DespawnedInSceneObjectsSync.Count; j++)
			{
				int position4 = writer.Position;
				writer.WriteValueSafe<global::Unity.Netcode.NetworkSceneHandle>(m_DespawnedInSceneObjectsSync[j].GetSceneOriginHandle(), default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				writer.WriteValueSafe(in m_DespawnedInSceneObjectsSync[j].GlobalObjectIdHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				int position5 = writer.Position;
				num += position5 - position4;
			}
			int position6 = writer.Position;
			uint value = (uint)(position6 - (position + 4));
			writer.Seek(position);
			writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.Seek(position6);
			if (EnableSerializationLogs)
			{
				LogArray(writer.ToArray(), position);
			}
		}

		internal void SerializeScenePlacedObjects(global::Unity.Netcode.FastBufferWriter writer)
		{
			ushort value = 0;
			int position = writer.Position;
			writer.WriteValueSafe<ushort>((ushort)0, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			bool distributedAuthorityMode = m_NetworkManager.DistributedAuthorityMode;
			bool flag = distributedAuthorityMode && TargetClientId == 0;
			m_NetworkObjectsSync.Clear();
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>> scenePlacedObject in m_NetworkManager.SceneManager.ScenePlacedObjects)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject> item in scenePlacedObject.Value)
				{
					if (item.Value.Observers.Contains(TargetClientId) || flag)
					{
						m_NetworkObjectsSync.Add(item.Value);
					}
				}
			}
			SortObjectsToSync();
			foreach (global::Unity.Netcode.NetworkObject item2 in m_NetworkObjectsSync)
			{
				item2.Serialize(TargetClientId, distributedAuthorityMode).Serialize(writer);
				value++;
			}
			writer.WriteValueSafe<int>(m_DespawnedInSceneObjectsSync.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < m_DespawnedInSceneObjectsSync.Count; i++)
			{
				writer.WriteValueSafe<global::Unity.Netcode.NetworkSceneHandle>(m_DespawnedInSceneObjectsSync[i].GetSceneOriginHandle(), default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				writer.WriteValueSafe(in m_DespawnedInSceneObjectsSync[i].GlobalObjectIdHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			int position2 = writer.Position;
			writer.Seek(position);
			writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.Seek(position2);
		}

		internal unsafe void Deserialize(global::Unity.Netcode.FastBufferReader reader)
		{
			reader.ReadValueSafe(out SceneEventType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			if (m_NetworkManager.DistributedAuthorityMode)
			{
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out TargetClientId);
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out SenderClientId);
			}
			if (SceneEventType == global::Unity.Netcode.SceneEventType.ActiveSceneChanged)
			{
				reader.ReadValueSafe(out ActiveSceneHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				return;
			}
			if (SceneEventType == global::Unity.Netcode.SceneEventType.ObjectSceneChanged)
			{
				if (!m_NetworkManager.IsConnectedClient)
				{
					DeferObjectsMovedIntoNewScene(reader);
				}
				else
				{
					DeserializeObjectsMovedIntoNewScene(reader);
				}
				return;
			}
			reader.ReadValueSafe(out byte value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			LoadSceneMode = (global::UnityEngine.SceneManagement.LoadSceneMode)value;
			if (SceneEventType != global::Unity.Netcode.SceneEventType.Synchronize)
			{
				reader.ReadValueSafe(out SceneEventProgressId, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
			}
			else
			{
				reader.ReadValueSafe(out ClientSynchronizationMode, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			}
			reader.ReadValueSafe(out SceneHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			reader.ReadValueSafe(out SceneHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			switch (SceneEventType)
			{
			case global::Unity.Netcode.SceneEventType.Synchronize:
				reader.ReadValueSafe(out ActiveSceneHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (EnableSerializationLogs)
				{
					LogArray(reader.ToArray(), 0, reader.Length);
				}
				CopySceneSynchronizationData(reader);
				break;
			case global::Unity.Netcode.SceneEventType.SynchronizeComplete:
				CheckClientSynchronizationResults(reader);
				break;
			case global::Unity.Netcode.SceneEventType.Load:
				m_HasInternalBuffer = true;
				InternalBuffer = new global::Unity.Netcode.FastBufferReader(reader.GetUnsafePtrAtCurrentPosition(), global::Unity.Collections.Allocator.Persistent, reader.Length - reader.Position);
				break;
			case global::Unity.Netcode.SceneEventType.ReSynchronize:
				ReadClientReSynchronizationData(reader);
				break;
			case global::Unity.Netcode.SceneEventType.LoadEventCompleted:
			case global::Unity.Netcode.SceneEventType.UnloadEventCompleted:
				ReadSceneEventProgressDone(reader);
				break;
			case global::Unity.Netcode.SceneEventType.Unload:
			case global::Unity.Netcode.SceneEventType.LoadComplete:
			case global::Unity.Netcode.SceneEventType.UnloadComplete:
				break;
			}
		}

		internal unsafe void CopySceneSynchronizationData(global::Unity.Netcode.FastBufferReader reader)
		{
			m_NetworkObjectsSync.Clear();
			reader.ReadValueSafe(out uint[] value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			reader.ReadValueSafe(out global::Unity.Netcode.NetworkSceneHandle[] value2, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			ScenesToSynchronize = new global::System.Collections.Generic.Queue<uint>(value);
			SceneHandlesToSynchronize = new global::System.Collections.Generic.Queue<global::Unity.Netcode.NetworkSceneHandle>(value2);
			reader.ReadValueSafe(out int value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			m_InternalBufferSize = value3;
			if (!reader.TryBeginRead(value3))
			{
				throw new global::System.OverflowException("Not enough space in the buffer to read recorded synchronization data size.");
			}
			m_HasInternalBuffer = true;
			InternalBuffer = new global::Unity.Netcode.FastBufferReader(reader.GetUnsafePtrAtCurrentPosition(), global::Unity.Collections.Allocator.Persistent, value3);
			if (EnableSerializationLogs)
			{
				LogArray(InternalBuffer.ToArray());
			}
		}

		internal void DeserializeScenePlacedObjects()
		{
			try
			{
				InternalBuffer.ReadValueSafe(out ushort value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> list = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();
				for (ushort num = 0; num < value; num++)
				{
					global::Unity.Netcode.NetworkObject.SerializedObject serializedObject = default(global::Unity.Netcode.NetworkObject.SerializedObject);
					serializedObject.Deserialize(InternalBuffer);
					if (serializedObject.IsSceneObject)
					{
						m_NetworkManager.SceneManager.SetTheSceneBeingSynchronized(serializedObject.NetworkSceneHandle);
					}
					global::Unity.Netcode.NetworkObject item = global::Unity.Netcode.NetworkObject.Deserialize(in serializedObject, InternalBuffer, m_NetworkManager);
					if (serializedObject.IsSceneObject)
					{
						list.Add(item);
					}
				}
				DeserializeDespawnedInScenePlacedNetworkObjects();
				foreach (global::Unity.Netcode.NetworkObject item2 in list)
				{
					item2.InternalInSceneNetworkObjectsSpawned();
				}
			}
			finally
			{
				InternalBuffer.Dispose();
				m_HasInternalBuffer = false;
			}
		}

		internal void ReadClientReSynchronizationData(global::Unity.Netcode.FastBufferReader reader)
		{
			reader.ReadValueSafe(out uint[] value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value.Length == 0)
			{
				return;
			}
			global::Unity.Netcode.NetworkObject[] array = global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: false, orderByIdentifier: true);
			global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject> dictionary = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.NetworkObject>();
			global::Unity.Netcode.NetworkObject[] array2 = array;
			foreach (global::Unity.Netcode.NetworkObject networkObject in array2)
			{
				if (!dictionary.ContainsKey(networkObject.NetworkObjectId))
				{
					dictionary.Add(networkObject.NetworkObjectId, networkObject);
				}
			}
			uint[] array3 = value;
			foreach (uint num in array3)
			{
				if (!dictionary.ContainsKey(num))
				{
					continue;
				}
				global::Unity.Netcode.NetworkObject networkObject2 = dictionary[num];
				dictionary.Remove(num);
				networkObject2.IsSpawned = false;
				if (m_NetworkManager.PrefabHandler.ContainsHandler(networkObject2))
				{
					if (m_NetworkManager.SpawnManager.SpawnedObjects.ContainsKey(num))
					{
						m_NetworkManager.SpawnManager.SpawnedObjects.Remove(num);
					}
					if (m_NetworkManager.SpawnManager.SpawnedObjectsList.Contains(networkObject2))
					{
						m_NetworkManager.SpawnManager.SpawnedObjectsList.Remove(networkObject2);
					}
					global::Unity.Netcode.NetworkManager.Singleton.PrefabHandler.HandleNetworkPrefabDestroy(networkObject2);
				}
				else
				{
					global::UnityEngine.Object.DestroyImmediate(networkObject2.gameObject);
				}
			}
		}

		internal void WriteClientReSynchronizationData(global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteValueSafe(m_NetworkObjectsToBeRemoved.ToArray(), default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		internal bool ClientNeedsReSynchronization()
		{
			return m_NetworkObjectsToBeRemoved.Count > 0;
		}

		internal void CheckClientSynchronizationResults(global::Unity.Netcode.FastBufferReader reader)
		{
			m_NetworkObjectsToBeRemoved.Clear();
			reader.ReadValueSafe(out uint value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value; i++)
			{
				reader.ReadValueSafe(out uint value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (!m_NetworkManager.SpawnManager.SpawnedObjects.ContainsKey(value2))
				{
					m_NetworkObjectsToBeRemoved.Add(value2);
				}
			}
		}

		internal void WriteClientSynchronizationResults(global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteValueSafe<uint>((uint)m_NetworkObjectsSync.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			foreach (global::Unity.Netcode.NetworkObject item in m_NetworkObjectsSync)
			{
				writer.WriteValueSafe<uint>((uint)item.NetworkObjectId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		private void DeserializeDespawnedInScenePlacedNetworkObjects()
		{
			m_DespawnedInSceneObjects.Clear();
			InternalBuffer.ReadValueSafe(out int value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkObject>> dictionary = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkObject>>();
			for (int i = 0; i < value; i++)
			{
				InternalBuffer.ReadValueSafe(out global::Unity.Netcode.NetworkSceneHandle value2, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				InternalBuffer.ReadValueSafe(out uint value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkObject> dictionary2 = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.NetworkObject>();
				if (!dictionary.ContainsKey(value2))
				{
					if (m_NetworkManager.SceneManager.ServerSceneHandleToClientSceneHandle.ContainsKey(value2))
					{
						global::Unity.Netcode.NetworkSceneHandle localSceneHandle = m_NetworkManager.SceneManager.ServerSceneHandleToClientSceneHandle[value2];
						if (m_NetworkManager.SceneManager.ScenesLoaded.ContainsKey(localSceneHandle))
						{
							_ = m_NetworkManager.SceneManager.ScenesLoaded[localSceneHandle];
							foreach (global::Unity.Netcode.NetworkObject item in global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>(includeInactive: true, orderByIdentifier: true), (global::Unity.Netcode.NetworkObject c) => c.GetSceneOriginHandle() == localSceneHandle && c.IsSceneObject != false)))
							{
								if (!dictionary2.ContainsKey(item.GlobalObjectIdHash))
								{
									dictionary2.Add(item.GlobalObjectIdHash, item);
								}
							}
							dictionary.Add(value2, dictionary2);
						}
						else
						{
							global::UnityEngine.Debug.LogError($"In-Scene NetworkObject GlobalObjectIdHash ({value3}) cannot find its relative local scene handle {localSceneHandle}!");
						}
					}
					else
					{
						global::UnityEngine.Debug.LogError($"In-Scene NetworkObject GlobalObjectIdHash ({value3}) cannot find its relative NetworkSceneHandle {value2}!");
					}
				}
				else
				{
					dictionary2 = dictionary[value2];
				}
				if (dictionary2.TryGetValue(value3, out var value4))
				{
					value4.NetworkManagerOwner = m_NetworkManager;
					value4.InvokeBehaviourNetworkDespawn();
					if (!m_NetworkManager.SceneManager.ScenePlacedObjects.ContainsKey(value3))
					{
						m_NetworkManager.SceneManager.ScenePlacedObjects.Add(value3, new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>());
					}
					if (!m_NetworkManager.SceneManager.ScenePlacedObjects[value3].ContainsKey(value4.GetSceneOriginHandle()))
					{
						m_NetworkManager.SceneManager.ScenePlacedObjects[value3].Add(value4.GetSceneOriginHandle(), value4);
					}
				}
				else
				{
					global::UnityEngine.Debug.LogError($"In-Scene NetworkObject GlobalObjectIdHash ({value3}) could not be found!");
				}
			}
		}

		internal void SynchronizeSceneNetworkObjects(global::Unity.Netcode.NetworkManager networkManager)
		{
			global::System.Text.StringBuilder stringBuilder = null;
			if (EnableSerializationLogs)
			{
				stringBuilder = new global::System.Text.StringBuilder();
			}
			try
			{
				InternalBuffer.ReadValueSafe(out int value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (EnableSerializationLogs)
				{
					stringBuilder.AppendLine($"[Read][Synchronize Objects][WPos: {InternalBuffer.Position}][NO-Count: {value}] Begin:");
				}
				for (int i = 0; i < value; i++)
				{
					int position = InternalBuffer.Position;
					global::Unity.Netcode.NetworkObject.SerializedObject serializedObject = default(global::Unity.Netcode.NetworkObject.SerializedObject);
					serializedObject.Deserialize(InternalBuffer);
					if (serializedObject.IsSceneObject)
					{
						m_NetworkManager.SceneManager.SetTheSceneBeingSynchronized(serializedObject.NetworkSceneHandle);
					}
					global::Unity.Netcode.NetworkObject networkObject = global::Unity.Netcode.NetworkObject.Deserialize(in serializedObject, InternalBuffer, networkManager);
					int position2 = InternalBuffer.Position;
					if (EnableSerializationLogs)
					{
						stringBuilder.AppendLine($"[Head: {position}][Tail: {position2}][Size: {position2 - position}][{networkObject.name}][NID-{networkObject.NetworkObjectId}][Children: {networkObject.ChildNetworkBehaviours.Count}]");
						LogArray(InternalBuffer.ToArray(), position, position2, stringBuilder);
					}
					if (networkObject != null && !m_NetworkObjectsSync.Contains(networkObject))
					{
						m_NetworkObjectsSync.Add(networkObject);
					}
				}
				if (EnableSerializationLogs)
				{
					global::UnityEngine.Debug.Log(stringBuilder.ToString());
				}
				foreach (global::Unity.Netcode.NetworkObject item in m_NetworkObjectsSync)
				{
					if (item.IsSceneObject.HasValue && item.IsSceneObject.Value)
					{
						item.InternalInSceneNetworkObjectsSpawned();
					}
				}
				DeserializeDespawnedInScenePlacedNetworkObjects();
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
				global::UnityEngine.Debug.Log(stringBuilder.ToString());
			}
			finally
			{
				InternalBuffer.Dispose();
				m_HasInternalBuffer = false;
			}
		}

		internal void WriteSceneEventProgressDone(global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteValueSafe<ushort>((ushort)ClientsCompleted.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			foreach (ulong item in ClientsCompleted)
			{
				writer.WriteValueSafe<ulong>(item, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			writer.WriteValueSafe<ushort>((ushort)ClientsTimedOut.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			foreach (ulong item2 in ClientsTimedOut)
			{
				writer.WriteValueSafe<ulong>(item2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		internal void ReadSceneEventProgressDone(global::Unity.Netcode.FastBufferReader reader)
		{
			reader.ReadValueSafe(out ushort value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			ClientsCompleted = new global::System.Collections.Generic.List<ulong>();
			for (int i = 0; i < value; i++)
			{
				reader.ReadValueSafe(out ulong value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				ClientsCompleted.Add(value2);
			}
			reader.ReadValueSafe(out ushort value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			ClientsTimedOut = new global::System.Collections.Generic.List<ulong>();
			for (int j = 0; j < value3; j++)
			{
				reader.ReadValueSafe(out ulong value4, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				ClientsTimedOut.Add(value4);
			}
		}

		private void SerializeObjectsMovedIntoNewScene(global::Unity.Netcode.FastBufferWriter writer)
		{
			global::Unity.Netcode.NetworkSceneManager sceneManager = m_NetworkManager.SceneManager;
			ulong value = m_NetworkManager.LocalClientId;
			if (IsForwarding)
			{
				value = m_OwnerId;
			}
			writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe<int>(sceneManager.ObjectsMigratedIntoNewScene.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> item in sceneManager.ObjectsMigratedIntoNewScene)
			{
				if (!sceneManager.ObjectsMigratedIntoNewScene[item.Key].ContainsKey(value))
				{
					throw new global::System.Exception($"Trying to send object scene migration for Client-{value} but the client has no entries to send!");
				}
				writer.WriteValueSafe<global::Unity.Netcode.NetworkSceneHandle>(item.Key, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				writer.WriteValueSafe<int>(item.Value[value].Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				foreach (global::Unity.Netcode.NetworkObject item2 in item.Value[value])
				{
					writer.WriteValueSafe<ulong>(item2.NetworkObjectId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
			}
		}

		private void DeserializeObjectsMovedIntoNewScene(global::Unity.Netcode.FastBufferReader reader)
		{
			global::Unity.Netcode.NetworkSceneManager sceneManager = m_NetworkManager.SceneManager;
			global::Unity.Netcode.NetworkSpawnManager spawnManager = m_NetworkManager.SpawnManager;
			int value = 0;
			int value2 = 0;
			ulong value3 = 0uL;
			ulong value4 = 0uL;
			reader.ReadValueSafe(out value4, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			m_OwnerId = value4;
			reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value; i++)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.NetworkSceneHandle value5, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				if (!sceneManager.ObjectsMigratedIntoNewScene.TryGetValue(value5, out var value6))
				{
					value6 = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>();
					sceneManager.ObjectsMigratedIntoNewScene.Add(value5, value6);
				}
				if (!value6.ContainsKey(value4))
				{
					value6.Add(value4, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
				}
				reader.ReadValueSafe(out value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				for (int j = 0; j < value2; j++)
				{
					reader.ReadValueSafe(out value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					if (!spawnManager.SpawnedObjects.ContainsKey(value3))
					{
						global::Unity.Netcode.NetworkLog.LogError($"[Object Scene Migration] Trying to synchronize NetworkObjectId ({value3}) but it was not spawned or no longer exists!!");
						continue;
					}
					global::Unity.Netcode.NetworkObject item = spawnManager.SpawnedObjects[value3];
					value6[value4].Add(item);
				}
			}
		}

		private void DeferObjectsMovedIntoNewScene(global::Unity.Netcode.FastBufferReader reader)
		{
			global::Unity.Netcode.NetworkSceneManager sceneManager = m_NetworkManager.SceneManager;
			_ = m_NetworkManager.SpawnManager;
			ulong value = 0uL;
			int value2 = 0;
			int value3 = 0;
			ulong value4 = 0uL;
			reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			global::Unity.Netcode.NetworkSceneManager.DeferredObjectsMovedEvent item = new global::Unity.Netcode.NetworkSceneManager.DeferredObjectsMovedEvent
			{
				OwnerId = value,
				ObjectsMigratedTable = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.List<ulong>>()
			};
			reader.ReadValueSafe(out value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value2; i++)
			{
				reader.ReadValueSafe(out global::Unity.Netcode.NetworkSceneHandle value5, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				global::System.Collections.Generic.List<ulong> list = new global::System.Collections.Generic.List<ulong>();
				item.ObjectsMigratedTable.Add(value5, list);
				reader.ReadValueSafe(out value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				for (int j = 0; j < value3; j++)
				{
					reader.ReadValueSafe(out value4, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					list.Add(value4);
				}
			}
			sceneManager.DeferredObjectsMovedEvents.Add(item);
		}

		internal void ProcessDeferredObjectSceneChangedEvents()
		{
			global::Unity.Netcode.NetworkSceneManager sceneManager = m_NetworkManager.SceneManager;
			global::Unity.Netcode.NetworkSpawnManager spawnManager = m_NetworkManager.SpawnManager;
			if (sceneManager.DeferredObjectsMovedEvents.Count == 0)
			{
				return;
			}
			foreach (global::Unity.Netcode.NetworkSceneManager.DeferredObjectsMovedEvent deferredObjectsMovedEvent in sceneManager.DeferredObjectsMovedEvents)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.List<ulong>> item in deferredObjectsMovedEvent.ObjectsMigratedTable)
				{
					if (!sceneManager.ObjectsMigratedIntoNewScene.TryGetValue(item.Key, out var value))
					{
						value = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>();
						sceneManager.ObjectsMigratedIntoNewScene.Add(item.Key, value);
					}
					if (!value.ContainsKey(deferredObjectsMovedEvent.OwnerId))
					{
						value.Add(deferredObjectsMovedEvent.OwnerId, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
					}
					foreach (ulong item2 in item.Value)
					{
						if (!spawnManager.SpawnedObjects.TryGetValue(item2, out var value2))
						{
							global::Unity.Netcode.NetworkLog.LogWarning($"[Deferred][Object Scene Migration] Trying to synchronize NetworkObjectId ({item2}) but it was not spawned or no longer exists!");
						}
						else if (!value[deferredObjectsMovedEvent.OwnerId].Contains(value2))
						{
							value[deferredObjectsMovedEvent.OwnerId].Add(value2);
						}
					}
				}
				deferredObjectsMovedEvent.ObjectsMigratedTable.Clear();
			}
			sceneManager.DeferredObjectsMovedEvents.Clear();
		}

		public void Dispose()
		{
			if (m_HasInternalBuffer)
			{
				InternalBuffer.Dispose();
				m_HasInternalBuffer = false;
			}
		}

		internal SceneEventData(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
			SceneEventId = global::System.Guid.NewGuid().ToString().Hash32();
		}
	}
}
