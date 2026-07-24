namespace UnityEngine.Rendering.Universal.UTess
{
	internal struct TessLink
	{
		internal global::Unity.Collections.NativeArray<int> roots;

		internal global::Unity.Collections.NativeArray<int> ranks;

		internal static global::UnityEngine.Rendering.Universal.UTess.TessLink CreateLink(int count, global::Unity.Collections.Allocator allocator)
		{
			global::UnityEngine.Rendering.Universal.UTess.TessLink result = new global::UnityEngine.Rendering.Universal.UTess.TessLink
			{
				roots = new global::Unity.Collections.NativeArray<int>(count, allocator),
				ranks = new global::Unity.Collections.NativeArray<int>(count, allocator)
			};
			for (int i = 0; i < count; i++)
			{
				result.roots[i] = i;
				result.ranks[i] = 0;
			}
			return result;
		}

		internal static void DestroyLink(global::UnityEngine.Rendering.Universal.UTess.TessLink link)
		{
			link.ranks.Dispose();
			link.roots.Dispose();
		}

		internal int Find(int x)
		{
			int index = x;
			while (roots[x] != x)
			{
				x = roots[x];
			}
			while (roots[index] != x)
			{
				int num = roots[index];
				roots[index] = x;
				index = num;
			}
			return x;
		}

		internal void Link(int x, int y)
		{
			int num = Find(x);
			int num2 = Find(y);
			if (num != num2)
			{
				int num3 = ranks[num];
				int num4 = ranks[num2];
				if (num3 < num4)
				{
					roots[num] = num2;
					return;
				}
				if (num4 < num3)
				{
					roots[num2] = num;
					return;
				}
				roots[num2] = num;
				ref global::Unity.Collections.NativeArray<int> reference = ref ranks;
				int index = num;
				int value = reference[index] + 1;
				reference[index] = value;
			}
		}
	}
}
