namespace UnityEngine.U2D.Animation
{
	internal class TransformAccessJob
	{
		public struct TransformData
		{
			public int transformIndex;

			public int refCount;

			public TransformData(int index)
			{
				transformIndex = index;
				refCount = 1;
			}
		}

		private global::UnityEngine.Transform[] m_Transform;

		private global::UnityEngine.Jobs.TransformAccessArray m_TransformAccessArray;

		private global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> m_TransformData;

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> m_TransformMatrix;

		private global::Unity.Collections.NativeArray<bool> m_TransformChanged;

		private bool m_Dirty;

		private global::Unity.Jobs.JobHandle m_JobHandle;

		public global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> transformData => m_TransformData;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> transformMatrix => m_TransformMatrix;

		public global::Unity.Collections.NativeArray<bool> transformChanged => m_TransformChanged;

		public TransformAccessJob()
		{
			InitializeDataStructures();
			m_Dirty = false;
			m_JobHandle = default(global::Unity.Jobs.JobHandle);
		}

		public void Destroy()
		{
			ClearDataStructures();
		}

		private void InitializeDataStructures()
		{
			m_TransformMatrix = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4>(1, global::Unity.Collections.Allocator.Persistent);
			m_TransformData = new global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData>(1, global::Unity.Collections.Allocator.Persistent);
			m_Transform = global::System.Array.Empty<global::UnityEngine.Transform>();
		}

		private void ClearDataStructures()
		{
			if (m_TransformMatrix.IsCreated)
			{
				m_TransformMatrix.Dispose();
			}
			if (m_TransformChanged.IsCreated)
			{
				m_TransformChanged.Dispose();
			}
			if (m_TransformAccessArray.isCreated)
			{
				m_TransformAccessArray.Dispose();
			}
			if (m_TransformData.IsCreated)
			{
				m_TransformData.Dispose();
			}
			m_Transform = null;
		}

		public void ResetCache()
		{
			ClearDataStructures();
			InitializeDataStructures();
		}

		public void AddTransform(global::UnityEngine.Transform t)
		{
			if (!(t == null) && m_TransformData.IsCreated)
			{
				m_JobHandle.Complete();
				int instanceID = t.GetInstanceID();
				if (m_TransformData.ContainsKey(instanceID))
				{
					global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData value = m_TransformData[instanceID];
					value.refCount++;
					m_TransformData[instanceID] = value;
				}
				else
				{
					m_TransformData.TryAdd(instanceID, new global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData(-1));
					ArrayAdd(ref m_Transform, t);
					m_Dirty = true;
				}
			}
		}

		private static void ArrayAdd<T>(ref T[] array, T item)
		{
			int num = array.Length;
			global::System.Array.Resize(ref array, num + 1);
			array[num] = item;
		}

		private static void ArrayRemoveAt<T>(ref T[] array, int index)
		{
			int num = array.Length;
			if (index >= num)
			{
				throw new global::System.ArgumentOutOfRangeException("index");
			}
			for (int i = index; i < num - 1; i++)
			{
				array[i] = array[i + 1];
			}
			global::System.Array.Resize(ref array, num - 1);
		}

		private static bool CompactArray<T>(ref T[] array)
		{
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					if (num != i)
					{
						array[num] = array[i];
					}
					num++;
				}
			}
			bool num2 = num < array.Length;
			if (num2)
			{
				global::System.Array.Resize(ref array, num);
			}
			return num2;
		}

		private void UpdateTransformIndex()
		{
			if (!m_Dirty)
			{
				return;
			}
			m_Dirty = false;
			if (m_TransformMatrix.IsCreated)
			{
				m_TransformMatrix.Dispose();
			}
			m_TransformMatrix = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4>(m_Transform.Length, global::Unity.Collections.Allocator.Persistent);
			if (!m_TransformAccessArray.isCreated)
			{
				global::UnityEngine.Jobs.TransformAccessArray.Allocate(m_Transform.Length, -1, out m_TransformAccessArray);
			}
			else if (m_TransformAccessArray.capacity != m_Transform.Length)
			{
				m_TransformAccessArray.capacity = m_Transform.Length;
			}
			m_TransformAccessArray.SetTransforms(m_Transform);
			for (int i = 0; i < m_Transform.Length; i++)
			{
				if (m_Transform[i] != null)
				{
					int instanceID = m_Transform[i].GetInstanceID();
					global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData value = m_TransformData[instanceID];
					value.transformIndex = i;
					m_TransformData[instanceID] = value;
				}
			}
		}

		public global::Unity.Jobs.JobHandle StartLocalToWorldAndChangeDetectionJob()
		{
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_TransformChanged, m_Transform.Length, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			if (m_Transform.Length != 0)
			{
				m_JobHandle.Complete();
				UpdateTransformIndex();
				global::UnityEngine.U2D.Animation.LocalToWorldAndChangeDetectionTransformAccessJob jobData = new global::UnityEngine.U2D.Animation.LocalToWorldAndChangeDetectionTransformAccessJob
				{
					outMatrix = transformMatrix,
					hasChanged = transformChanged
				};
				m_JobHandle = global::UnityEngine.Jobs.IJobParallelForTransformExtensions.ScheduleReadOnly(jobData, m_TransformAccessArray, 16);
				return m_JobHandle;
			}
			return default(global::Unity.Jobs.JobHandle);
		}

		public global::Unity.Jobs.JobHandle StartWorldToLocalJob()
		{
			if (m_Transform.Length != 0)
			{
				m_JobHandle.Complete();
				UpdateTransformIndex();
				global::UnityEngine.U2D.Animation.WorldToLocalTransformAccessJob jobData = new global::UnityEngine.U2D.Animation.WorldToLocalTransformAccessJob
				{
					outMatrix = transformMatrix
				};
				m_JobHandle = global::UnityEngine.Jobs.IJobParallelForTransformExtensions.ScheduleReadOnly(jobData, m_TransformAccessArray, 16);
				return m_JobHandle;
			}
			return default(global::Unity.Jobs.JobHandle);
		}

		internal string GetDebugLog()
		{
			string text = "";
			text = text + "TransformData Count: " + m_TransformData.Count + "\n";
			text = text + "Transform Count: " + m_Transform.Length + "\n";
			global::UnityEngine.Transform[] transform = m_Transform;
			foreach (global::UnityEngine.Transform transform2 in transform)
			{
				text += ((transform2 == null) ? "null" : (transform2.name + " " + transform2.GetInstanceID()));
				text += "\n";
				if (transform2 != null)
				{
					text = text + "RefCount: " + m_TransformData[transform2.GetInstanceID()].refCount + "\n";
				}
				text += "\n";
			}
			return text;
		}

		internal int RemoveTransformsIfNull()
		{
			int num = 0;
			for (int num2 = m_Transform.Length - 1; num2 >= 0; num2--)
			{
				if (!m_Transform[num2])
				{
					m_TransformData.Remove(m_Transform[num2].GetInstanceID());
					m_Transform[num2] = null;
					num++;
				}
			}
			if (CompactArray(ref m_Transform))
			{
				m_Dirty = true;
			}
			return num;
		}

		internal void RemoveTransformsByIds(global::System.Collections.Generic.List<int> idsToRemove)
		{
			if (!m_TransformData.IsCreated)
			{
				return;
			}
			m_JobHandle.Complete();
			global::System.Collections.Generic.List<int> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<int>, int>.Get();
			for (int num = idsToRemove.Count - 1; num >= 0; num--)
			{
				int num2 = idsToRemove[num];
				if (!m_TransformData.ContainsKey(num2))
				{
					idsToRemove.Remove(num2);
				}
				else
				{
					global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData value = m_TransformData[num2];
					if (value.refCount > 1)
					{
						value.refCount--;
						m_TransformData[num2] = value;
						idsToRemove.Remove(num2);
					}
					else
					{
						m_TransformData.Remove(num2);
						if (0 <= value.transformIndex)
						{
							list.Add(value.transformIndex);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				list.Sort();
				for (int num3 = list.Count - 1; num3 >= 0; num3--)
				{
					int num4 = list[num3];
					if (num4 < m_Transform.Length)
					{
						m_Transform[num4] = null;
					}
				}
				if (CompactArray(ref m_Transform))
				{
					m_Dirty = true;
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<int>, int>.Release(list);
		}

		internal void RemoveTransformById(int transformId)
		{
			if (!m_TransformData.IsCreated)
			{
				return;
			}
			m_JobHandle.Complete();
			if (!m_TransformData.TryGetValue(transformId, out var item))
			{
				return;
			}
			if (item.refCount == 1)
			{
				m_TransformData.Remove(transformId);
				int num = global::System.Array.FindIndex(m_Transform, (global::UnityEngine.Transform t) => t.GetInstanceID() == transformId);
				if (num >= 0)
				{
					ArrayRemoveAt(ref m_Transform, num);
				}
				m_Dirty = true;
			}
			else
			{
				item.refCount--;
				m_TransformData[transformId] = item;
			}
		}
	}
}
