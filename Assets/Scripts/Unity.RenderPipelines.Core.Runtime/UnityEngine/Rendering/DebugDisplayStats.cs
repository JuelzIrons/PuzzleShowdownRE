namespace UnityEngine.Rendering
{
	public abstract class DebugDisplayStats<TProfileId> where TProfileId : global::System.Enum
	{
		private class AccumulatedTiming
		{
			public float accumulatedValue;

			public float lastAverage;

			internal void UpdateLastAverage(int frameCount)
			{
				lastAverage = accumulatedValue / (float)frameCount;
				accumulatedValue = 0f;
			}
		}

		private enum DebugProfilingType
		{
			CPU = 0,
			InlineCPU = 1,
			GPU = 2
		}

		private static readonly string[] k_DetailedStatsColumnLabels = new string[3] { "CPU", "CPUInline", "GPU" };

		private global::System.Collections.Generic.Dictionary<TProfileId, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming>[] m_AccumulatedTiming = new global::System.Collections.Generic.Dictionary<TProfileId, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming>[3]
		{
			new global::System.Collections.Generic.Dictionary<TProfileId, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming>(),
			new global::System.Collections.Generic.Dictionary<TProfileId, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming>(),
			new global::System.Collections.Generic.Dictionary<TProfileId, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming>()
		};

		private float m_TimeSinceLastAvgValue;

		private int m_AccumulatedFrames;

		private global::System.Collections.Generic.HashSet<TProfileId> m_HiddenProfileIds = new global::System.Collections.Generic.HashSet<TProfileId>();

		private const float k_AccumulationTimeInSeconds = 1f;

		protected bool averageProfilerTimingsOverASecond;

		protected bool hideEmptyScopes = true;

		public abstract void EnableProfilingRecorders();

		public abstract void DisableProfilingRecorders();

		public abstract void RegisterDebugUI(global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> list);

		public abstract void Update();

		protected global::System.Collections.Generic.List<TProfileId> GetProfilerIdsToDisplay()
		{
			global::System.Collections.Generic.List<TProfileId> list = new global::System.Collections.Generic.List<TProfileId>();
			global::System.Type type = typeof(TProfileId);
			foreach (object value in global::System.Enum.GetValues(type))
			{
				if (global::System.Attribute.GetCustomAttribute(global::System.Linq.Enumerable.First(type.GetMember(value.ToString()), (global::System.Reflection.MemberInfo m) => m.DeclaringType == type), typeof(global::UnityEngine.Rendering.HideInDebugUIAttribute)) == null)
				{
					list.Add((TProfileId)value);
				}
			}
			return list;
		}

		protected void UpdateDetailedStats(global::System.Collections.Generic.List<TProfileId> samplers)
		{
			m_HiddenProfileIds.Clear();
			m_TimeSinceLastAvgValue += global::UnityEngine.Time.unscaledDeltaTime;
			m_AccumulatedFrames++;
			bool flag = m_TimeSinceLastAvgValue >= 1f;
			UpdateListOfAveragedProfilerTimings(flag, samplers);
			if (flag)
			{
				m_TimeSinceLastAvgValue = 0f;
				m_AccumulatedFrames = 0;
			}
		}

		protected global::UnityEngine.Rendering.DebugUI.Widget BuildDetailedStatsList(string title, global::System.Collections.Generic.List<TProfileId> samplers)
		{
			return new global::UnityEngine.Rendering.DebugUI.Foldout(title, BuildProfilingSamplerWidgetList(samplers), k_DetailedStatsColumnLabels)
			{
				opened = true
			};
		}

		private void UpdateListOfAveragedProfilerTimings(bool needUpdatingAverages, global::System.Collections.Generic.List<TProfileId> samplers)
		{
			foreach (TProfileId sampler in samplers)
			{
				global::UnityEngine.Rendering.ProfilingSampler profilingSampler = global::UnityEngine.Rendering.ProfilingSampler.Get(sampler);
				bool flag = true;
				if (m_AccumulatedTiming[0].TryGetValue(sampler, out var value))
				{
					value.accumulatedValue += profilingSampler.cpuElapsedTime;
					flag &= value.accumulatedValue == 0f;
				}
				if (m_AccumulatedTiming[1].TryGetValue(sampler, out var value2))
				{
					value2.accumulatedValue += profilingSampler.inlineCpuElapsedTime;
					flag &= value2.accumulatedValue == 0f;
				}
				if (m_AccumulatedTiming[2].TryGetValue(sampler, out var value3))
				{
					value3.accumulatedValue += profilingSampler.gpuElapsedTime;
					flag &= value3.accumulatedValue == 0f;
				}
				if (needUpdatingAverages)
				{
					value?.UpdateLastAverage(m_AccumulatedFrames);
					value2?.UpdateLastAverage(m_AccumulatedFrames);
					value3?.UpdateLastAverage(m_AccumulatedFrames);
				}
				if (flag)
				{
					m_HiddenProfileIds.Add(sampler);
				}
			}
		}

		private float GetSamplerTiming(TProfileId samplerId, global::UnityEngine.Rendering.ProfilingSampler sampler, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType type)
		{
			if (averageProfilerTimingsOverASecond && m_AccumulatedTiming[(int)type].TryGetValue(samplerId, out var value))
			{
				return value.lastAverage;
			}
			return type switch
			{
				global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType.GPU => sampler.gpuElapsedTime, 
				global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType.CPU => sampler.cpuElapsedTime, 
				_ => sampler.inlineCpuElapsedTime, 
			};
		}

		private global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> BuildProfilingSamplerWidgetList(global::System.Collections.Generic.IEnumerable<TProfileId> samplers)
		{
			global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> observableList = new global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget>();
			foreach (TProfileId samplerId in samplers)
			{
				global::UnityEngine.Rendering.ProfilingSampler sampler = global::UnityEngine.Rendering.ProfilingSampler.Get(samplerId);
				if (sampler != null)
				{
					sampler.enableRecording = true;
					observableList.Add(new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = sampler.name,
						isHiddenCallback = () => (hideEmptyScopes && m_HiddenProfileIds.Contains(samplerId)) ? true : false,
						values = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Cast<global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType>(global::System.Enum.GetValues(typeof(global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType))), (global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType e) => CreateWidgetForSampler(samplerId, sampler, e)))
					});
				}
			}
			return observableList;
			global::UnityEngine.Rendering.DebugUI.Value CreateWidgetForSampler(TProfileId val, global::UnityEngine.Rendering.ProfilingSampler sampler2, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.DebugProfilingType type)
			{
				global::System.Collections.Generic.Dictionary<TProfileId, global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming> dictionary = m_AccumulatedTiming[(int)type];
				if (!dictionary.ContainsKey(val))
				{
					dictionary.Add(val, new global::UnityEngine.Rendering.DebugDisplayStats<TProfileId>.AccumulatedTiming());
				}
				return new global::UnityEngine.Rendering.DebugUI.Value
				{
					formatString = "{0:F2}ms",
					refreshRate = 0.2f,
					getter = () => GetSamplerTiming(val, sampler2, type)
				};
			}
		}
	}
}
