namespace Unity.Jobs
{
	public class EarlyInitHelpers
	{
		public delegate void EarlyInitFunction();

		private static global::System.Collections.Generic.List<global::Unity.Jobs.EarlyInitHelpers.EarlyInitFunction> s_PendingDelegates;

		static EarlyInitHelpers()
		{
			FlushEarlyInits();
		}

		public static void FlushEarlyInits()
		{
			while (s_PendingDelegates != null)
			{
				global::System.Collections.Generic.List<global::Unity.Jobs.EarlyInitHelpers.EarlyInitFunction> list = s_PendingDelegates;
				s_PendingDelegates = null;
				for (int i = 0; i < list.Count; i++)
				{
					try
					{
						list[i]();
					}
					catch (global::System.Exception exception)
					{
						global::UnityEngine.Debug.LogException(exception);
					}
				}
			}
		}

		public static void AddEarlyInitFunction(global::Unity.Jobs.EarlyInitHelpers.EarlyInitFunction func)
		{
			if (s_PendingDelegates == null)
			{
				s_PendingDelegates = new global::System.Collections.Generic.List<global::Unity.Jobs.EarlyInitHelpers.EarlyInitFunction>();
			}
			s_PendingDelegates.Add(func);
		}

		public static void JobReflectionDataCreationFailed(global::System.Exception ex)
		{
			global::UnityEngine.Debug.LogError("Failed to create job reflection data. Please refer to callstack of exception for information on which job could not produce its reflection data.");
			global::UnityEngine.Debug.LogException(ex);
		}
	}
}
