namespace UnityEngine.Rendering
{
	public static class SweepLineRectUtils
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct EventComparer : global::System.Collections.Generic.IComparer<global::UnityEngine.Vector4>
		{
			public int Compare(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
			{
				int num = a.x.CompareTo(b.x);
				if (num != 0)
				{
					return num;
				}
				return b.y.CompareTo(a.y);
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct ActiveComparer : global::System.Collections.Generic.IComparer<global::UnityEngine.Vector2>
		{
			public int Compare(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
			{
				return a.x.CompareTo(b.x);
			}
		}

		public static float CalculateRectUnionArea(global::System.Collections.Generic.List<global::UnityEngine.Rect> rects)
		{
			int count = rects.Count;
			global::UnityEngine.Vector4[] array = global::System.Buffers.ArrayPool<global::UnityEngine.Vector4>.Shared.Rent(count * 2);
			global::UnityEngine.Vector2[] array2 = global::System.Buffers.ArrayPool<global::UnityEngine.Vector2>.Shared.Rent(count);
			int eventCount = 0;
			foreach (global::UnityEngine.Rect rect in rects)
			{
				InsertEvents(rect, array, ref eventCount);
			}
			float result = CalculateRectUnionArea(array, array2, eventCount);
			global::System.Buffers.ArrayPool<global::UnityEngine.Vector4>.Shared.Return(array);
			global::System.Buffers.ArrayPool<global::UnityEngine.Vector2>.Shared.Return(array2);
			return result;
		}

		private static float MergeLengthY(global::UnityEngine.Vector2[] activeBuffer, int count)
		{
			if (count <= 0)
			{
				return 0f;
			}
			float num = 0f;
			float num2 = activeBuffer[0].x;
			float num3 = activeBuffer[0].y;
			for (int i = 1; i < count; i++)
			{
				float x = activeBuffer[i].x;
				float y = activeBuffer[i].y;
				if (x <= num3)
				{
					if (y > num3)
					{
						num3 = y;
					}
				}
				else
				{
					num += num3 - num2;
					num2 = x;
					num3 = y;
				}
			}
			num += num3 - num2;
			return global::UnityEngine.Mathf.Clamp01(num);
		}

		private unsafe static float CalculateRectUnionArea(global::UnityEngine.Vector4[] eventsBuffer, global::UnityEngine.Vector2[] activeBuffer, int eventCount)
		{
			if (eventCount == 0)
			{
				return 0f;
			}
			fixed (global::UnityEngine.Vector4* array = eventsBuffer)
			{
				global::Unity.Collections.NativeSortExtension.Sort(array, eventCount, default(global::UnityEngine.Rendering.SweepLineRectUtils.EventComparer));
			}
			int num = 0;
			float num2 = 0f;
			float num3 = eventsBuffer[0].x;
			bool flag = false;
			int num4 = 0;
			while (num4 < eventCount)
			{
				float x = eventsBuffer[num4].x;
				if (flag)
				{
					num3 = x;
					flag = false;
				}
				float num5 = x - num3;
				if (num5 > 0f && num > 0)
				{
					fixed (global::UnityEngine.Vector2* array2 = activeBuffer)
					{
						global::Unity.Collections.NativeSortExtension.Sort(array2, num, default(global::UnityEngine.Rendering.SweepLineRectUtils.ActiveComparer));
					}
					num2 += MergeLengthY(activeBuffer, num) * num5;
					num3 = x;
				}
				do
				{
					global::UnityEngine.Vector4 vector = eventsBuffer[num4];
					float z = vector.z;
					float w = vector.w;
					if (vector.y > 0f)
					{
						activeBuffer[num++] = new global::UnityEngine.Vector2(z, w);
					}
					else
					{
						for (int i = 0; i < num; i++)
						{
							global::UnityEngine.Vector2 vector2 = activeBuffer[i];
							if (global::UnityEngine.Mathf.Approximately(vector2.x, z) && global::UnityEngine.Mathf.Approximately(vector2.y, w))
							{
								int num6 = num - 1;
								activeBuffer[i] = activeBuffer[num6];
								num = num6;
								break;
							}
						}
						if (num == 0)
						{
							flag = true;
						}
					}
					num4++;
				}
				while (num4 < eventCount && global::UnityEngine.Mathf.Approximately(eventsBuffer[num4].x, x));
			}
			return num2;
		}

		private static void InsertEvents(in global::UnityEngine.Rect rect, global::UnityEngine.Vector4[] eventsBuffer, ref int eventCount)
		{
			if (rect.width > 0f && rect.height > 0f)
			{
				float z = global::UnityEngine.Mathf.Clamp01(rect.yMin);
				float w = global::UnityEngine.Mathf.Clamp01(rect.yMax);
				eventsBuffer[eventCount++] = new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Clamp01(rect.xMin), 1f, z, w);
				eventsBuffer[eventCount++] = new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Clamp01(rect.xMax), -1f, z, w);
			}
		}
	}
}
