namespace Unity.VisualScripting
{
	public static class EditorTimeBinding
	{
		public static global::System.Func<int> frameBinding;

		public static global::System.Func<float> timeBinding;

		public static int frame
		{
			get
			{
				if (frameBinding == null || !global::Unity.VisualScripting.UnityThread.allowsAPI)
				{
					return 0;
				}
				return frameBinding();
			}
		}

		public static float time
		{
			get
			{
				if (timeBinding == null || !global::Unity.VisualScripting.UnityThread.allowsAPI)
				{
					return 0f;
				}
				return timeBinding();
			}
		}

		static EditorTimeBinding()
		{
			frameBinding = () => global::UnityEngine.Time.frameCount;
			timeBinding = () => global::UnityEngine.Time.time;
		}
	}
}
