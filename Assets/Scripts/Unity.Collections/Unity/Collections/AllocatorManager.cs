namespace Unity.Collections
{
	public static class AllocatorManager
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public delegate int TryFunction(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block);

		public struct AllocatorHandle : global::Unity.Collections.AllocatorManager.IAllocator, global::System.IDisposable, global::System.IEquatable<global::Unity.Collections.AllocatorManager.AllocatorHandle>, global::System.IComparable<global::Unity.Collections.AllocatorManager.AllocatorHandle>
		{
			public ushort Index;

			public ushort Version;

			internal ref global::Unity.Collections.AllocatorManager.TableEntry TableEntry => ref global::Unity.Collections.AllocatorManager.SharedStatics.TableEntry.Ref.Data.ElementAt(Index);

			internal bool IsInstalled => ((global::Unity.Collections.AllocatorManager.SharedStatics.IsInstalled.Ref.Data.ElementAt(Index >> 6) >> (int)Index) & 1) != 0;

			public int Value => Index;

			public global::Unity.Collections.AllocatorManager.TryFunction Function => null;

			public global::Unity.Collections.AllocatorManager.AllocatorHandle Handle
			{
				get
				{
					return this;
				}
				set
				{
					this = value;
				}
			}

			public global::Unity.Collections.Allocator ToAllocator
			{
				get
				{
					uint index = Index;
					return (global::Unity.Collections.Allocator)((Version << 16) | (int)index);
				}
			}

			public bool IsCustomAllocator => Index >= 64;

			public bool IsAutoDispose => ((global::Unity.Collections.AllocatorManager.SharedStatics.IsAutoDispose.Ref.Data.ElementAt(Index >> 6) >> (int)Index) & 1) != 0;

			internal void IncrementVersion()
			{
			}

			internal void Rewind()
			{
			}

			internal void Install(global::Unity.Collections.AllocatorManager.TableEntry tableEntry)
			{
				Rewind();
				TableEntry = tableEntry;
			}

			public static implicit operator global::Unity.Collections.AllocatorManager.AllocatorHandle(global::Unity.Collections.Allocator a)
			{
				return new global::Unity.Collections.AllocatorManager.AllocatorHandle
				{
					Index = (ushort)(a & (global::Unity.Collections.Allocator)65535),
					Version = 0
				};
			}

			public int TryAllocateBlock<T>(out global::Unity.Collections.AllocatorManager.Block block, int items) where T : unmanaged
			{
				block = new global::Unity.Collections.AllocatorManager.Block
				{
					Range = new global::Unity.Collections.AllocatorManager.Range
					{
						Items = items,
						Allocator = this
					},
					BytesPerItem = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(),
					Alignment = 1 << global::Unity.Mathematics.math.min(3, global::Unity.Mathematics.math.tzcnt(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>()))
				};
				return Try(ref block);
			}

			public global::Unity.Collections.AllocatorManager.Block AllocateBlock<T>(int items) where T : unmanaged
			{
				TryAllocateBlock<T>(out var block, items);
				return block;
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private static void CheckAllocatedSuccessfully(int error)
			{
				if (error != 0)
				{
					throw new global::System.ArgumentException($"Error {error}: Failed to Allocate");
				}
			}

			public int Try(ref global::Unity.Collections.AllocatorManager.Block block)
			{
				block.Range.Allocator = this;
				return global::Unity.Collections.AllocatorManager.Try(ref block);
			}

			public void Dispose()
			{
				Rewind();
				TableEntry = default(global::Unity.Collections.AllocatorManager.TableEntry);
			}

			public override bool Equals(object obj)
			{
				if (obj is global::Unity.Collections.AllocatorManager.AllocatorHandle)
				{
					return Value == ((global::Unity.Collections.AllocatorManager.AllocatorHandle)obj).Value;
				}
				if (obj is global::Unity.Collections.Allocator)
				{
					return ToAllocator == (global::Unity.Collections.Allocator)obj;
				}
				return false;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool Equals(global::Unity.Collections.AllocatorManager.AllocatorHandle other)
			{
				return Value == other.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool Equals(global::Unity.Collections.Allocator other)
			{
				return ToAllocator == other;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public override int GetHashCode()
			{
				return Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public static bool operator ==(global::Unity.Collections.AllocatorManager.AllocatorHandle lhs, global::Unity.Collections.AllocatorManager.AllocatorHandle rhs)
			{
				return lhs.Value == rhs.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public static bool operator !=(global::Unity.Collections.AllocatorManager.AllocatorHandle lhs, global::Unity.Collections.AllocatorManager.AllocatorHandle rhs)
			{
				return lhs.Value != rhs.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public static bool operator <(global::Unity.Collections.AllocatorManager.AllocatorHandle lhs, global::Unity.Collections.AllocatorManager.AllocatorHandle rhs)
			{
				return lhs.Value < rhs.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public static bool operator >(global::Unity.Collections.AllocatorManager.AllocatorHandle lhs, global::Unity.Collections.AllocatorManager.AllocatorHandle rhs)
			{
				return lhs.Value > rhs.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public static bool operator <=(global::Unity.Collections.AllocatorManager.AllocatorHandle lhs, global::Unity.Collections.AllocatorManager.AllocatorHandle rhs)
			{
				return lhs.Value <= rhs.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public static bool operator >=(global::Unity.Collections.AllocatorManager.AllocatorHandle lhs, global::Unity.Collections.AllocatorManager.AllocatorHandle rhs)
			{
				return lhs.Value >= rhs.Value;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public int CompareTo(global::Unity.Collections.AllocatorManager.AllocatorHandle other)
			{
				return Value - other.Value;
			}
		}

		public struct BlockHandle
		{
			public ushort Value;
		}

		public struct Range : global::System.IDisposable
		{
			public global::System.IntPtr Pointer;

			public int Items;

			public global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

			public void Dispose()
			{
				global::Unity.Collections.AllocatorManager.Block block = new global::Unity.Collections.AllocatorManager.Block
				{
					Range = this
				};
				block.Dispose();
				this = block.Range;
			}
		}

		public struct Block : global::System.IDisposable
		{
			public global::Unity.Collections.AllocatorManager.Range Range;

			public int BytesPerItem;

			public int AllocatedItems;

			public byte Log2Alignment;

			public byte Padding0;

			public ushort Padding1;

			public uint Padding2;

			public long Bytes => (long)BytesPerItem * (long)Range.Items;

			public long AllocatedBytes => (long)BytesPerItem * (long)AllocatedItems;

			public int Alignment
			{
				get
				{
					return 1 << (int)Log2Alignment;
				}
				set
				{
					Log2Alignment = (byte)(32 - global::Unity.Mathematics.math.lzcnt(global::Unity.Mathematics.math.max(1, value) - 1));
				}
			}

			public void Dispose()
			{
				TryFree();
			}

			public int TryAllocate()
			{
				Range.Pointer = global::System.IntPtr.Zero;
				return Try(ref this);
			}

			public int TryFree()
			{
				Range.Items = 0;
				return Try(ref this);
			}

			public void Allocate()
			{
				TryAllocate();
			}

			public void Free()
			{
				TryFree();
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckFailedToAllocate(int error)
			{
				if (error != 0)
				{
					throw new global::System.ArgumentException($"Error {error}: Failed to Allocate {this}");
				}
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckFailedToFree(int error)
			{
				if (error != 0)
				{
					throw new global::System.ArgumentException($"Error {error}: Failed to Free {this}");
				}
			}
		}

		public interface IAllocator : global::System.IDisposable
		{
			global::Unity.Collections.AllocatorManager.TryFunction Function { get; }

			global::Unity.Collections.AllocatorManager.AllocatorHandle Handle { get; set; }

			global::Unity.Collections.Allocator ToAllocator { get; }

			bool IsCustomAllocator { get; }

			bool IsAutoDispose => false;

			int Try(ref global::Unity.Collections.AllocatorManager.Block block);
		}

		[global::Unity.Burst.BurstCompile]
		internal struct StackAllocator : global::Unity.Collections.AllocatorManager.IAllocator, global::System.IDisposable
		{
			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
			internal delegate int Try_000000AB_0024PostfixBurstDelegate(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block);

			internal static class Try_000000AB_0024BurstDirectCall
			{
				private static global::System.IntPtr Pointer;

				[global::Unity.Burst.BurstDiscard]
				private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
				{
					if (Pointer == (global::System.IntPtr)0)
					{
						Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Collections.AllocatorManager.StackAllocator.Try_000000AB_0024PostfixBurstDelegate>(Try).Value;
					}
					P_0 = Pointer;
				}

				private static global::System.IntPtr GetFunctionPointer()
				{
					nint result = 0;
					GetFunctionPointerDiscard(ref result);
					return result;
				}

				public unsafe static int Invoke(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block)
				{
					if (global::Unity.Burst.BurstCompiler.IsEnabled)
					{
						global::System.IntPtr functionPointer = GetFunctionPointer();
						if (functionPointer != (global::System.IntPtr)0)
						{
							return ((delegate* unmanaged[Cdecl]<global::System.IntPtr, ref global::Unity.Collections.AllocatorManager.Block, int>)functionPointer)(allocatorState, ref block);
						}
					}
					return Try_0024BurstManaged(allocatorState, ref block);
				}
			}

			internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_handle;

			internal global::Unity.Collections.AllocatorManager.Block m_storage;

			internal long m_top;

			public global::Unity.Collections.AllocatorManager.AllocatorHandle Handle
			{
				get
				{
					return m_handle;
				}
				set
				{
					m_handle = value;
				}
			}

			public global::Unity.Collections.Allocator ToAllocator => m_handle.ToAllocator;

			public bool IsCustomAllocator => m_handle.IsCustomAllocator;

			public global::Unity.Collections.AllocatorManager.TryFunction Function => Try;

			public void Initialize(global::Unity.Collections.AllocatorManager.Block storage)
			{
				m_storage = storage;
				m_top = 0L;
			}

			public unsafe int Try(ref global::Unity.Collections.AllocatorManager.Block block)
			{
				if (block.Range.Pointer == global::System.IntPtr.Zero)
				{
					if (m_top + block.Bytes > m_storage.Bytes)
					{
						return -1;
					}
					block.Range.Pointer = (global::System.IntPtr)((byte*)(void*)m_storage.Range.Pointer + m_top);
					block.AllocatedItems = block.Range.Items;
					m_top += block.Bytes;
					return 0;
				}
				if (block.Bytes == 0L)
				{
					if ((byte*)(void*)block.Range.Pointer - (byte*)(void*)m_storage.Range.Pointer == m_top - block.AllocatedBytes)
					{
						m_top -= block.AllocatedBytes;
						block.Range.Pointer = global::System.IntPtr.Zero;
						block.AllocatedItems = 0;
						return 0;
					}
					return -1;
				}
				return -1;
			}

			[global::Unity.Burst.BurstCompile]
			[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
			public static int Try(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block)
			{
				return global::Unity.Collections.AllocatorManager.StackAllocator.Try_000000AB_0024BurstDirectCall.Invoke(allocatorState, ref block);
			}

			public void Dispose()
			{
				m_handle.Rewind();
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			[global::Unity.Burst.BurstCompile]
			[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
			internal unsafe static int Try_0024BurstManaged(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block)
			{
				return ((global::Unity.Collections.AllocatorManager.StackAllocator*)(void*)allocatorState)->Try(ref block);
			}
		}

		[global::Unity.Burst.BurstCompile]
		internal struct SlabAllocator : global::Unity.Collections.AllocatorManager.IAllocator, global::System.IDisposable
		{
			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
			internal delegate int Try_000000B9_0024PostfixBurstDelegate(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block);

			internal static class Try_000000B9_0024BurstDirectCall
			{
				private static global::System.IntPtr Pointer;

				[global::Unity.Burst.BurstDiscard]
				private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
				{
					if (Pointer == (global::System.IntPtr)0)
					{
						Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Collections.AllocatorManager.SlabAllocator.Try_000000B9_0024PostfixBurstDelegate>(Try).Value;
					}
					P_0 = Pointer;
				}

				private static global::System.IntPtr GetFunctionPointer()
				{
					nint result = 0;
					GetFunctionPointerDiscard(ref result);
					return result;
				}

				public unsafe static int Invoke(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block)
				{
					if (global::Unity.Burst.BurstCompiler.IsEnabled)
					{
						global::System.IntPtr functionPointer = GetFunctionPointer();
						if (functionPointer != (global::System.IntPtr)0)
						{
							return ((delegate* unmanaged[Cdecl]<global::System.IntPtr, ref global::Unity.Collections.AllocatorManager.Block, int>)functionPointer)(allocatorState, ref block);
						}
					}
					return Try_0024BurstManaged(allocatorState, ref block);
				}
			}

			internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_handle;

			internal global::Unity.Collections.AllocatorManager.Block Storage;

			internal int Log2SlabSizeInBytes;

			internal global::Unity.Collections.FixedList4096Bytes<int> Occupied;

			internal long budgetInBytes;

			internal long allocatedBytes;

			public global::Unity.Collections.AllocatorManager.AllocatorHandle Handle
			{
				get
				{
					return m_handle;
				}
				set
				{
					m_handle = value;
				}
			}

			public global::Unity.Collections.Allocator ToAllocator => m_handle.ToAllocator;

			public bool IsCustomAllocator => m_handle.IsCustomAllocator;

			public long BudgetInBytes => budgetInBytes;

			public long AllocatedBytes => allocatedBytes;

			internal int SlabSizeInBytes
			{
				get
				{
					return 1 << Log2SlabSizeInBytes;
				}
				set
				{
					Log2SlabSizeInBytes = (byte)(32 - global::Unity.Mathematics.math.lzcnt(global::Unity.Mathematics.math.max(1, value) - 1));
				}
			}

			internal int Slabs => (int)(Storage.Bytes >> Log2SlabSizeInBytes);

			public global::Unity.Collections.AllocatorManager.TryFunction Function => Try;

			internal void Initialize(global::Unity.Collections.AllocatorManager.Block storage, int slabSizeInBytes, long budget)
			{
				Storage = storage;
				Log2SlabSizeInBytes = 0;
				Occupied = default(global::Unity.Collections.FixedList4096Bytes<int>);
				budgetInBytes = budget;
				allocatedBytes = 0L;
				SlabSizeInBytes = slabSizeInBytes;
				Occupied.Length = (Slabs + 31) / 32;
			}

			public int Try(ref global::Unity.Collections.AllocatorManager.Block block)
			{
				if (block.Range.Pointer == global::System.IntPtr.Zero)
				{
					if (block.Bytes + allocatedBytes > budgetInBytes)
					{
						return -2;
					}
					if (block.Bytes > SlabSizeInBytes)
					{
						return -1;
					}
					for (int i = 0; i < Occupied.Length; i++)
					{
						int num = Occupied[i];
						if (num == -1)
						{
							continue;
						}
						for (int j = 0; j < 32; j++)
						{
							if ((num & (1 << j)) == 0)
							{
								Occupied[i] |= 1 << j;
								block.Range.Pointer = Storage.Range.Pointer + (int)(SlabSizeInBytes * ((long)i * 32L + j));
								block.AllocatedItems = SlabSizeInBytes / block.BytesPerItem;
								allocatedBytes += block.Bytes;
								return 0;
							}
						}
					}
					return -1;
				}
				if (block.Bytes == 0L)
				{
					ulong num2 = (ulong)((long)block.Range.Pointer - (long)Storage.Range.Pointer) >> Log2SlabSizeInBytes;
					int index = (int)(num2 >> 5);
					int num3 = (int)(num2 & 0x1F);
					Occupied[index] &= ~(1 << num3);
					block.Range.Pointer = global::System.IntPtr.Zero;
					int num4 = block.AllocatedItems * block.BytesPerItem;
					allocatedBytes -= num4;
					block.AllocatedItems = 0;
					return 0;
				}
				return -1;
			}

			[global::Unity.Burst.BurstCompile]
			[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
			public static int Try(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block)
			{
				return global::Unity.Collections.AllocatorManager.SlabAllocator.Try_000000B9_0024BurstDirectCall.Invoke(allocatorState, ref block);
			}

			public void Dispose()
			{
				m_handle.Rewind();
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			[global::Unity.Burst.BurstCompile]
			[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Collections.AllocatorManager.TryFunction))]
			internal unsafe static int Try_0024BurstManaged(global::System.IntPtr allocatorState, ref global::Unity.Collections.AllocatorManager.Block block)
			{
				return ((global::Unity.Collections.AllocatorManager.SlabAllocator*)(void*)allocatorState)->Try(ref block);
			}
		}

		internal struct TableEntry
		{
			internal global::System.IntPtr function;

			internal global::System.IntPtr state;
		}

		internal struct Array16<T> where T : unmanaged
		{
			internal T f0;

			internal T f1;

			internal T f2;

			internal T f3;

			internal T f4;

			internal T f5;

			internal T f6;

			internal T f7;

			internal T f8;

			internal T f9;

			internal T f10;

			internal T f11;

			internal T f12;

			internal T f13;

			internal T f14;

			internal T f15;
		}

		internal struct Array256<T> where T : unmanaged
		{
			internal global::Unity.Collections.AllocatorManager.Array16<T> f0;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f1;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f2;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f3;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f4;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f5;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f6;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f7;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f8;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f9;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f10;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f11;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f12;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f13;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f14;

			internal global::Unity.Collections.AllocatorManager.Array16<T> f15;
		}

		internal struct Array4096<T> where T : unmanaged
		{
			internal global::Unity.Collections.AllocatorManager.Array256<T> f0;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f1;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f2;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f3;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f4;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f5;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f6;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f7;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f8;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f9;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f10;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f11;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f12;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f13;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f14;

			internal global::Unity.Collections.AllocatorManager.Array256<T> f15;
		}

		internal struct Array32768<T> : global::Unity.Collections.IIndexable<T> where T : unmanaged
		{
			internal global::Unity.Collections.AllocatorManager.Array4096<T> f0;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f1;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f2;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f3;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f4;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f5;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f6;

			internal global::Unity.Collections.AllocatorManager.Array4096<T> f7;

			public int Length
			{
				get
				{
					return 32768;
				}
				set
				{
				}
			}

			public unsafe ref T ElementAt(int index)
			{
				fixed (global::Unity.Collections.AllocatorManager.Array4096<T>* ptr = &f0)
				{
					return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>((byte*)ptr + (nint)index * (nint)sizeof(T));
				}
			}
		}

		internal sealed class SharedStatics
		{
			internal sealed class IsInstalled
			{
				internal static readonly global::Unity.Burst.SharedStatic<global::Unity.Collections.Long1024> Ref = global::Unity.Burst.SharedStatic<global::Unity.Collections.Long1024>.GetOrCreateUnsafe(0u, -4832911380680317357L, 0L);
			}

			internal sealed class TableEntry
			{
				internal static readonly global::Unity.Burst.SharedStatic<global::Unity.Collections.AllocatorManager.Array32768<global::Unity.Collections.AllocatorManager.TableEntry>> Ref = global::Unity.Burst.SharedStatic<global::Unity.Collections.AllocatorManager.Array32768<global::Unity.Collections.AllocatorManager.TableEntry>>.GetOrCreateUnsafe(0u, -1297938794087215229L, 0L);
			}

			internal sealed class IsAutoDispose
			{
				internal static readonly global::Unity.Burst.SharedStatic<global::Unity.Collections.Long1024> Ref = global::Unity.Burst.SharedStatic<global::Unity.Collections.Long1024>.GetOrCreateUnsafe(0u, -5725630068035020733L, 0L);
			}
		}

		internal static class Managed
		{
			internal static global::Unity.Collections.AllocatorManager.TryFunction[] TryFunctionDelegates = new global::Unity.Collections.AllocatorManager.TryFunction[32768];

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Uses managed delegate")]
			public static void RegisterDelegate(int index, global::Unity.Collections.AllocatorManager.TryFunction function)
			{
				if (index >= 32768)
				{
					throw new global::System.ArgumentException("index to be registered in TryFunction delegate table exceeds maximum.");
				}
				TryFunctionDelegates[index] = function;
			}

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Uses managed delegate")]
			public static void UnregisterDelegate(int index)
			{
				if (index >= 32768)
				{
					throw new global::System.ArgumentException("index to be unregistered in TryFunction delegate table exceeds maximum.");
				}
				TryFunctionDelegates[index] = null;
			}
		}

		private static class AllocatorCache<T> where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			public static global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction> TryFunction;

			public static global::Unity.Collections.AllocatorManager.TryFunction CachedFunction;
		}

		public static readonly global::Unity.Collections.AllocatorManager.AllocatorHandle Invalid = new global::Unity.Collections.AllocatorManager.AllocatorHandle
		{
			Index = 0
		};

		public static readonly global::Unity.Collections.AllocatorManager.AllocatorHandle None = new global::Unity.Collections.AllocatorManager.AllocatorHandle
		{
			Index = 1
		};

		public static readonly global::Unity.Collections.AllocatorManager.AllocatorHandle Temp = new global::Unity.Collections.AllocatorManager.AllocatorHandle
		{
			Index = 2
		};

		public static readonly global::Unity.Collections.AllocatorManager.AllocatorHandle TempJob = new global::Unity.Collections.AllocatorManager.AllocatorHandle
		{
			Index = 3
		};

		public static readonly global::Unity.Collections.AllocatorManager.AllocatorHandle Persistent = new global::Unity.Collections.AllocatorManager.AllocatorHandle
		{
			Index = 4
		};

		public static readonly global::Unity.Collections.AllocatorManager.AllocatorHandle AudioKernel = new global::Unity.Collections.AllocatorManager.AllocatorHandle
		{
			Index = 5
		};

		public const int kErrorNone = 0;

		public const int kErrorBufferOverflow = -1;

		public const ushort FirstUserIndex = 64;

		public const ushort MaxNumCustomAllocators = 32768;

		internal static readonly ushort NumGlobalScratchAllocators = (ushort)global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;

		internal static readonly ushort MaxNumGlobalAllocators = (ushort)global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;

		internal static readonly uint GlobalAllocatorBaseIndex = (uint)(32768 - MaxNumGlobalAllocators);

		internal static readonly uint FirstGlobalScratchpadAllocatorIndex = GlobalAllocatorBaseIndex;

		internal static global::Unity.Collections.AllocatorManager.Block AllocateBlock<T>(this ref T t, int sizeOf, int alignOf, int items) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.AllocatorManager.Block block = new global::Unity.Collections.AllocatorManager.Block
			{
				Range = 
				{
					Pointer = global::System.IntPtr.Zero,
					Items = items,
					Allocator = t.Handle
				},
				BytesPerItem = sizeOf,
				Alignment = global::Unity.Mathematics.math.max(64, alignOf)
			};
			t.Try(ref block);
			return block;
		}

		internal static global::Unity.Collections.AllocatorManager.Block AllocateBlock<T, U>(this ref T t, U u, int items) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator where U : unmanaged
		{
			return AllocateBlock(ref t, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<U>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<U>(), items);
		}

		public unsafe static void* Allocate<T>(this ref T t, int sizeOf, int alignOf, int items = 1) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			return (void*)AllocateBlock(ref t, sizeOf, alignOf, items).Range.Pointer;
		}

		internal unsafe static U* Allocate<T, U>(this ref T t, U u, int items) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator where U : unmanaged
		{
			return (U*)Allocate(ref t, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<U>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<U>(), items);
		}

		internal unsafe static void* AllocateStruct<T, U>(this ref T t, U u, int items) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator where U : unmanaged
		{
			return Allocate(ref t, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<U>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<U>(), items);
		}

		internal static void FreeBlock<T>(this ref T t, ref global::Unity.Collections.AllocatorManager.Block block) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			block.Range.Items = 0;
			t.Try(ref block);
		}

		internal unsafe static void Free<T>(this ref T t, void* pointer, int sizeOf, int alignOf, int items) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			if (pointer != null)
			{
				global::Unity.Collections.AllocatorManager.Block block = new global::Unity.Collections.AllocatorManager.Block
				{
					AllocatedItems = items,
					Range = 
					{
						Pointer = (global::System.IntPtr)pointer
					},
					BytesPerItem = sizeOf,
					Alignment = alignOf
				};
				FreeBlock(ref t, ref block);
			}
		}

		internal unsafe static void Free<T, U>(this ref T t, U* pointer, int items) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator where U : unmanaged
		{
			Free(ref t, pointer, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<U>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<U>(), items);
		}

		public unsafe static void* Allocate(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, int itemSizeInBytes, int alignmentInBytes, int items = 1)
		{
			return Allocate(ref handle, itemSizeInBytes, alignmentInBytes, items);
		}

		public unsafe static T* Allocate<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, int items = 1) where T : unmanaged
		{
			return Allocate(ref handle, default(T), items);
		}

		public unsafe static void Free(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, void* pointer, int itemSizeInBytes, int alignmentInBytes, int items = 1)
		{
			Free(ref handle, pointer, itemSizeInBytes, alignmentInBytes, items);
		}

		public unsafe static void Free(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, void* pointer)
		{
			Free(ref handle, (byte*)pointer, 1);
		}

		public unsafe static void Free<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, T* pointer, int items = 1) where T : unmanaged
		{
			Free(ref handle, pointer, items);
		}

		public static global::Unity.Collections.AllocatorManager.AllocatorHandle ConvertToAllocatorHandle(global::Unity.Collections.Allocator a)
		{
			ushort index = (ushort)(a & (global::Unity.Collections.Allocator)65535);
			ushort version = (ushort)((uint)a >> 16);
			return new global::Unity.Collections.AllocatorManager.AllocatorHandle
			{
				Index = index,
				Version = version
			};
		}

		[global::Unity.Burst.BurstDiscard]
		private static void CheckDelegate(ref bool useDelegate)
		{
			useDelegate = true;
		}

		private static bool UseDelegate()
		{
			bool useDelegate = false;
			CheckDelegate(ref useDelegate);
			return useDelegate;
		}

		private static int allocate_block(ref global::Unity.Collections.AllocatorManager.Block block)
		{
			global::Unity.Collections.AllocatorManager.TableEntry tableEntry = default(global::Unity.Collections.AllocatorManager.TableEntry);
			tableEntry = block.Range.Allocator.TableEntry;
			return new global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction>(tableEntry.function).Invoke(tableEntry.state, ref block);
		}

		[global::Unity.Burst.BurstDiscard]
		private static void forward_mono_allocate_block(ref global::Unity.Collections.AllocatorManager.Block block, ref int error)
		{
			global::Unity.Collections.AllocatorManager.TableEntry tableEntry = default(global::Unity.Collections.AllocatorManager.TableEntry);
			tableEntry = block.Range.Allocator.TableEntry;
			if (block.Range.Allocator.Handle.Index >= 32768)
			{
				throw new global::System.ArgumentException("Allocator index into TryFunction delegate table exceeds maximum.");
			}
			error = global::Unity.Collections.AllocatorManager.Managed.TryFunctionDelegates[block.Range.Allocator.Handle.Index](tableEntry.state, ref block);
		}

		internal static global::Unity.Collections.Allocator LegacyOf(global::Unity.Collections.AllocatorManager.AllocatorHandle handle)
		{
			if (handle.Value >= 64)
			{
				return global::Unity.Collections.Allocator.Persistent;
			}
			return (global::Unity.Collections.Allocator)handle.Value;
		}

		private unsafe static int TryLegacy(ref global::Unity.Collections.AllocatorManager.Block block)
		{
			if (block.Range.Pointer == global::System.IntPtr.Zero)
			{
				block.Range.Pointer = (global::System.IntPtr)global::Unity.Collections.Memory.Unmanaged.Allocate(block.Bytes, block.Alignment, LegacyOf(block.Range.Allocator));
				block.AllocatedItems = block.Range.Items;
				if (!(block.Range.Pointer == global::System.IntPtr.Zero))
				{
					return 0;
				}
				return -1;
			}
			if (block.Bytes == 0L)
			{
				if (LegacyOf(block.Range.Allocator) != global::Unity.Collections.Allocator.None)
				{
					global::Unity.Collections.Memory.Unmanaged.Free((void*)block.Range.Pointer, LegacyOf(block.Range.Allocator));
				}
				block.Range.Pointer = global::System.IntPtr.Zero;
				block.AllocatedItems = 0;
				return 0;
			}
			return -1;
		}

		public static int Try(ref global::Unity.Collections.AllocatorManager.Block block)
		{
			if (block.Range.Allocator.Value < 64)
			{
				return TryLegacy(ref block);
			}
			global::Unity.Collections.AllocatorManager.TableEntry tableEntry = default(global::Unity.Collections.AllocatorManager.TableEntry);
			tableEntry = block.Range.Allocator.TableEntry;
			new global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction>(tableEntry.function);
			if (UseDelegate())
			{
				int error = 0;
				forward_mono_allocate_block(ref block, ref error);
				return error;
			}
			return allocate_block(ref block);
		}

		public static void Initialize()
		{
		}

		internal static void Install(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, global::System.IntPtr allocatorState, global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction> functionPointer, global::Unity.Collections.AllocatorManager.TryFunction function, bool IsAutoDispose = false)
		{
			if (functionPointer.Value == global::System.IntPtr.Zero)
			{
				Unregister(ref handle);
			}
			else if (global::Unity.Collections.ConcurrentMask.Succeeded(global::Unity.Collections.ConcurrentMask.TryAllocate(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsInstalled.Ref.Data, handle.Value, 1)))
			{
				handle.Install(new global::Unity.Collections.AllocatorManager.TableEntry
				{
					state = allocatorState,
					function = functionPointer.Value
				});
				global::Unity.Collections.AllocatorManager.Managed.RegisterDelegate(handle.Index, function);
				if (IsAutoDispose)
				{
					global::Unity.Collections.ConcurrentMask.TryAllocate(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsAutoDispose.Ref.Data, handle.Value, 1);
				}
			}
		}

		internal static void Install(global::Unity.Collections.AllocatorManager.AllocatorHandle handle, global::System.IntPtr allocatorState, global::Unity.Collections.AllocatorManager.TryFunction function)
		{
			global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction> functionPointer = ((function == null) ? new global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction>(global::System.IntPtr.Zero) : global::Unity.Burst.BurstCompiler.CompileFunctionPointer(function));
			Install(handle, allocatorState, functionPointer, function);
		}

		internal static global::Unity.Collections.AllocatorManager.AllocatorHandle Register(global::System.IntPtr allocatorState, global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction> functionPointer, bool IsAutoDispose = false, bool isGlobal = false, int globalIndex = 0)
		{
			int error;
			int offset;
			if (isGlobal)
			{
				if (globalIndex < GlobalAllocatorBaseIndex)
				{
					throw new global::System.ArgumentException($"Error: {globalIndex} is less than GlobalAllocatorBaseIndex");
				}
				error = global::Unity.Collections.ConcurrentMask.TryAllocate(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsInstalled.Ref.Data, globalIndex, 1);
				offset = globalIndex;
			}
			else
			{
				error = global::Unity.Collections.ConcurrentMask.TryAllocate(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsInstalled.Ref.Data, out offset, 1, (int)(GlobalAllocatorBaseIndex - 1), 1);
			}
			global::Unity.Collections.AllocatorManager.TableEntry tableEntry = new global::Unity.Collections.AllocatorManager.TableEntry
			{
				state = allocatorState,
				function = functionPointer.Value
			};
			global::Unity.Collections.AllocatorManager.AllocatorHandle result = default(global::Unity.Collections.AllocatorManager.AllocatorHandle);
			if (global::Unity.Collections.ConcurrentMask.Succeeded(error))
			{
				result.Index = (ushort)offset;
				result.Install(tableEntry);
				if (IsAutoDispose)
				{
					global::Unity.Collections.ConcurrentMask.TryAllocate(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsAutoDispose.Ref.Data, offset, 1);
				}
			}
			return result;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Uses managed delegate")]
		public unsafe static void Register<T>(this ref T t, bool IsAutoDispose = false, bool isGlobal = false, int globalIndex = 0) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.AllocatorManager.TryFunction function = t.Function;
			global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction> functionPointer;
			if (function == null)
			{
				functionPointer = new global::Unity.Burst.FunctionPointer<global::Unity.Collections.AllocatorManager.TryFunction>(global::System.IntPtr.Zero);
			}
			else
			{
				if (function != global::Unity.Collections.AllocatorManager.AllocatorCache<T>.CachedFunction)
				{
					global::Unity.Collections.AllocatorManager.AllocatorCache<T>.TryFunction = global::Unity.Burst.BurstCompiler.CompileFunctionPointer(function);
					global::Unity.Collections.AllocatorManager.AllocatorCache<T>.CachedFunction = function;
				}
				functionPointer = global::Unity.Collections.AllocatorManager.AllocatorCache<T>.TryFunction;
			}
			t.Handle = Register((global::System.IntPtr)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref t), functionPointer, IsAutoDispose, isGlobal, globalIndex);
			global::Unity.Collections.AllocatorManager.Managed.RegisterDelegate(t.Handle.Index, t.Function);
		}

		public static void UnmanagedUnregister<T>(this ref T t) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			if (t.Handle.IsInstalled)
			{
				t.Handle.Install(default(global::Unity.Collections.AllocatorManager.TableEntry));
				global::Unity.Collections.ConcurrentMask.TryFree(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsInstalled.Ref.Data, t.Handle.Value, 1);
				global::Unity.Collections.ConcurrentMask.TryFree(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsAutoDispose.Ref.Data, t.Handle.Value, 1);
			}
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Uses managed delegate")]
		public static void Unregister<T>(this ref T t) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			if (t.Handle.IsInstalled)
			{
				t.Handle.Dispose();
				global::Unity.Collections.ConcurrentMask.TryFree(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsInstalled.Ref.Data, t.Handle.Value, 1);
				global::Unity.Collections.ConcurrentMask.TryFree(ref global::Unity.Collections.AllocatorManager.SharedStatics.IsAutoDispose.Ref.Data, t.Handle.Value, 1);
				global::Unity.Collections.AllocatorManager.Managed.UnregisterDelegate(t.Handle.Index);
			}
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Register uses managed delegate")]
		internal unsafe static ref T CreateAllocator<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocator, bool isGlobal = false, int globalIndex = 0) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			T* ptr = (T*)global::Unity.Collections.Memory.Unmanaged.Allocate(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), 16, backingAllocator);
			*ptr = default(T);
			ref T reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(ptr);
			Register(ref reference, ptr->IsAutoDispose, isGlobal, globalIndex);
			return ref reference;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Registration uses managed delegates")]
		internal unsafe static void DestroyAllocator<T>(this ref T t, global::Unity.Collections.AllocatorManager.AllocatorHandle backingAllocator) where T : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			Unregister(ref t);
			global::Unity.Collections.Memory.Unmanaged.Free(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref t), backingAllocator);
		}

		public static void Shutdown()
		{
		}

		internal static bool IsCustomAllocator(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return allocator.Index >= 64;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckFailedToAllocate(int error)
		{
			if (error != 0)
			{
				throw new global::System.ArgumentException("failed to allocate");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckFailedToFree(int error)
		{
			if (error != 0)
			{
				throw new global::System.ArgumentException("failed to free");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckValid(global::Unity.Collections.AllocatorManager.AllocatorHandle handle)
		{
		}
	}
}
