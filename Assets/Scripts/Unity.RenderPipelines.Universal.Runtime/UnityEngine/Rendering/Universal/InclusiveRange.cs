namespace UnityEngine.Rendering.Universal
{
	internal struct InclusiveRange
	{
		public short start;

		public short end;

		public bool isEmpty => end < start;

		public static global::UnityEngine.Rendering.Universal.InclusiveRange empty => new global::UnityEngine.Rendering.Universal.InclusiveRange(short.MaxValue, short.MinValue);

		public InclusiveRange(short startEnd)
		{
			start = startEnd;
			end = startEnd;
		}

		public InclusiveRange(short start, short end)
		{
			this.start = start;
			this.end = end;
		}

		public void Expand(short index)
		{
			start = global::System.Math.Min(start, index);
			end = global::System.Math.Max(end, index);
		}

		public void Clamp(short min, short max)
		{
			start = global::System.Math.Max(min, start);
			end = global::System.Math.Min(max, end);
		}

		public bool Contains(short index)
		{
			if (index >= start)
			{
				return index <= end;
			}
			return false;
		}

		public static global::UnityEngine.Rendering.Universal.InclusiveRange Merge(global::UnityEngine.Rendering.Universal.InclusiveRange a, global::UnityEngine.Rendering.Universal.InclusiveRange b)
		{
			return new global::UnityEngine.Rendering.Universal.InclusiveRange(global::System.Math.Min(a.start, b.start), global::System.Math.Max(a.end, b.end));
		}

		public override string ToString()
		{
			return $"[{start}, {end}]";
		}
	}
}
