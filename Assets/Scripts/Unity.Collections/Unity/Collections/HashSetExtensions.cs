namespace Unity.Collections
{
	public static class HashSetExtensions
	{
		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList128Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList128Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList128Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList32Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList32Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList32Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList4096Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList4096Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList4096Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList512Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList512Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList512Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList64Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList64Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.FixedList64Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count, global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeHashSet<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList128Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList128Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList128Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList32Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList32Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList32Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList4096Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList4096Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList4096Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList512Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList512Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList512Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList64Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList64Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.FixedList64Bytes<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeParallelHashSet<T>.ReadOnly other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		public static void ExceptWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		public static void IntersectWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(container.Count(), global::Unity.Collections.Allocator.Temp);
			foreach (T item in other)
			{
				T value = item;
				if (container.Contains(value))
				{
					other2.Add(in value);
				}
			}
			container.Clear();
			global::Unity.Collections.LowLevel.Unsafe.HashSetExtensions.UnionWith(ref container, other2);
			other2.Dispose();
		}

		public static void UnionWith<T>(this ref global::Unity.Collections.NativeParallelHashSet<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}
	}
}
