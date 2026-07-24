namespace Unity.VisualScripting
{
	public static class ProfilingUtility
	{
		private static readonly object @lock;

		public static global::Unity.VisualScripting.ProfiledSegment rootSegment { get; private set; }

		public static global::Unity.VisualScripting.ProfiledSegment currentSegment { get; set; }

		static ProfilingUtility()
		{
			@lock = new object();
			currentSegment = (rootSegment = new global::Unity.VisualScripting.ProfiledSegment(null, "Root"));
		}

		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public static void Clear()
		{
			currentSegment = (rootSegment = new global::Unity.VisualScripting.ProfiledSegment(null, "Root"));
		}

		public static global::Unity.VisualScripting.ProfilingScope SampleBlock(string name)
		{
			return new global::Unity.VisualScripting.ProfilingScope(name);
		}

		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public static void BeginSample(string name)
		{
			global::System.Threading.Monitor.Enter(@lock);
			if (!currentSegment.children.Contains(name))
			{
				currentSegment.children.Add(new global::Unity.VisualScripting.ProfiledSegment(currentSegment, name));
			}
			currentSegment = currentSegment.children[name];
			currentSegment.calls++;
			currentSegment.stopwatch.Start();
			_ = global::Unity.VisualScripting.UnityThread.allowsAPI;
		}

		[global::System.Diagnostics.Conditional("ENABLE_PROFILER")]
		public static void EndSample()
		{
			currentSegment.stopwatch.Stop();
			if (currentSegment.parent != null)
			{
				currentSegment = currentSegment.parent;
			}
			_ = global::Unity.VisualScripting.UnityThread.allowsAPI;
			global::System.Threading.Monitor.Exit(@lock);
		}
	}
}
