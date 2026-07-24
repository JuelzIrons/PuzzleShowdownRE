namespace UnityEngine.Rendering.Universal
{
	internal class UniversalRenderPipelineDebugDisplayStats : global::UnityEngine.Rendering.DebugDisplayStats<global::UnityEngine.Rendering.Universal.URPProfileId>
	{
		private global::UnityEngine.Rendering.DebugFrameTiming m_DebugFrameTiming = new global::UnityEngine.Rendering.DebugFrameTiming();

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.URPProfileId> m_RecordedSamplers = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.URPProfileId>();

		public override void EnableProfilingRecorders()
		{
			m_RecordedSamplers = GetProfilerIdsToDisplay();
		}

		public override void DisableProfilingRecorders()
		{
			foreach (global::UnityEngine.Rendering.Universal.URPProfileId recordedSampler in m_RecordedSamplers)
			{
				global::UnityEngine.Rendering.ProfilingSampler.Get(recordedSampler).enableRecording = false;
			}
			m_RecordedSamplers.Clear();
		}

		public override void RegisterDebugUI(global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> list)
		{
			m_DebugFrameTiming.RegisterDebugUI(list);
			global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
			{
				displayName = "Detailed Stats",
				opened = false,
				children = 
				{
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
					{
						displayName = "Update every second with average",
						getter = () => averageProfilerTimingsOverASecond,
						setter = delegate(bool value)
						{
							averageProfilerTimingsOverASecond = value;
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
					{
						displayName = "Hide empty scopes",
						tooltip = "Hide profiling scopes where elapsed time in each category is zero",
						getter = () => hideEmptyScopes,
						setter = delegate(bool value)
						{
							hideEmptyScopes = value;
						}
					}
				}
			};
			foldout.children.Add(BuildDetailedStatsList("Profiling Scopes", m_RecordedSamplers));
			list.Add(foldout);
		}

		public override void Update()
		{
			m_DebugFrameTiming.UpdateFrameTiming();
			UpdateDetailedStats(m_RecordedSamplers);
		}
	}
}
