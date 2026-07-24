namespace UnityEngine.Rendering
{
	public class DebugFrameTiming
	{
		private const string k_FpsFormatString = "{0:F1}";

		private const string k_MsFormatString = "{0:F2}ms";

		private const float k_RefreshRate = 0.2f;

		internal global::UnityEngine.Rendering.FrameTimeSampleHistory m_FrameHistory;

		internal global::UnityEngine.Rendering.BottleneckHistory m_BottleneckHistory;

		private global::UnityEngine.FrameTiming[] m_Timing = new global::UnityEngine.FrameTiming[1];

		private global::UnityEngine.Rendering.FrameTimeSample m_Sample;

		public int bottleneckHistorySize { get; set; } = 60;

		public int sampleHistorySize { get; set; } = 30;

		public DebugFrameTiming()
		{
			m_FrameHistory = new global::UnityEngine.Rendering.FrameTimeSampleHistory(sampleHistorySize);
			m_BottleneckHistory = new global::UnityEngine.Rendering.BottleneckHistory(bottleneckHistorySize);
		}

		public void UpdateFrameTiming()
		{
			m_Timing[0] = default(global::UnityEngine.FrameTiming);
			m_Sample = default(global::UnityEngine.Rendering.FrameTimeSample);
			global::UnityEngine.FrameTimingManager.CaptureFrameTimings();
			global::UnityEngine.FrameTimingManager.GetLatestTimings(1u, m_Timing);
			if (m_Timing.Length != 0)
			{
				m_Sample.FullFrameTime = (float)global::System.Linq.Enumerable.First(m_Timing).cpuFrameTime;
				m_Sample.FramesPerSecond = ((m_Sample.FullFrameTime > 0f) ? (1000f / m_Sample.FullFrameTime) : 0f);
				m_Sample.MainThreadCPUFrameTime = (float)global::System.Linq.Enumerable.First(m_Timing).cpuMainThreadFrameTime;
				m_Sample.MainThreadCPUPresentWaitTime = (float)global::System.Linq.Enumerable.First(m_Timing).cpuMainThreadPresentWaitTime;
				m_Sample.RenderThreadCPUFrameTime = (float)global::System.Linq.Enumerable.First(m_Timing).cpuRenderThreadFrameTime;
				m_Sample.GPUFrameTime = (float)global::System.Linq.Enumerable.First(m_Timing).gpuFrameTime;
			}
			m_FrameHistory.DiscardOldSamples(sampleHistorySize);
			m_FrameHistory.Add(m_Sample);
			m_FrameHistory.ComputeAggregateValues();
			m_BottleneckHistory.DiscardOldSamples(bottleneckHistorySize);
			m_BottleneckHistory.AddBottleneckFromAveragedSample(m_FrameHistory.SampleAverage);
			m_BottleneckHistory.ComputeHistogram();
		}

		public void RegisterDebugUI(global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> list)
		{
			list.Add(new global::UnityEngine.Rendering.DebugUI.Foldout
			{
				displayName = "Frame Stats",
				opened = true,
				columnLabels = new string[3] { "Avg", "Min", "Max" },
				children = 
				{
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = "Frame Rate (FPS)",
						values = new global::UnityEngine.Rendering.DebugUI.Value[3]
						{
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F1}",
								getter = () => m_FrameHistory.SampleAverage.FramesPerSecond
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F1}",
								getter = () => m_FrameHistory.SampleMin.FramesPerSecond
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F1}",
								getter = () => m_FrameHistory.SampleMax.FramesPerSecond
							}
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = "Frame Time",
						values = new global::UnityEngine.Rendering.DebugUI.Value[3]
						{
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleAverage.FullFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMin.FullFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMax.FullFrameTime
							}
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = "CPU Main Thread Frame",
						values = new global::UnityEngine.Rendering.DebugUI.Value[3]
						{
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleAverage.MainThreadCPUFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMin.MainThreadCPUFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMax.MainThreadCPUFrameTime
							}
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = "CPU Render Thread Frame",
						values = new global::UnityEngine.Rendering.DebugUI.Value[3]
						{
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleAverage.RenderThreadCPUFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMin.RenderThreadCPUFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMax.RenderThreadCPUFrameTime
							}
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = "CPU Present Wait",
						values = new global::UnityEngine.Rendering.DebugUI.Value[3]
						{
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleAverage.MainThreadCPUPresentWaitTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMin.MainThreadCPUPresentWaitTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMax.MainThreadCPUPresentWaitTime
							}
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ValueTuple
					{
						displayName = "GPU Frame",
						values = new global::UnityEngine.Rendering.DebugUI.Value[3]
						{
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleAverage.GPUFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMin.GPUFrameTime
							},
							new global::UnityEngine.Rendering.DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => m_FrameHistory.SampleMax.GPUFrameTime
							}
						}
					}
				}
			});
			list.Add(new global::UnityEngine.Rendering.DebugUI.Foldout
			{
				displayName = "Bottlenecks",
				children = 
				{
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ProgressBarValue
					{
						displayName = "CPU",
						getter = () => m_BottleneckHistory.Histogram.CPU
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ProgressBarValue
					{
						displayName = "GPU",
						getter = () => m_BottleneckHistory.Histogram.GPU
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ProgressBarValue
					{
						displayName = "Present limited",
						getter = () => m_BottleneckHistory.Histogram.PresentLimited
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.ProgressBarValue
					{
						displayName = "Balanced",
						getter = () => m_BottleneckHistory.Histogram.Balanced
					}
				}
			});
		}

		internal void Reset()
		{
			m_BottleneckHistory.Clear();
			m_FrameHistory.Clear();
		}
	}
}
