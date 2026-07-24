namespace UnityEngine.AdaptivePerformance
{
	public class AdaptivePerformanceIndexer
	{
		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> m_UnappliedScalers;

		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> m_AppliedScalers;

		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> m_DisabledScalers;

		private global::UnityEngine.AdaptivePerformance.ThermalStateTracker m_ThermalStateTracker;

		private global::UnityEngine.AdaptivePerformance.PerformanceStateTracker m_PerformanceStateTracker;

		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerEfficiencyTracker m_ScalerEfficiencyTracker;

		private global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings m_Settings;

		private const string m_FeatureName = "Indexer";

		public float TimeUntilNextAction { get; private set; }

		public global::UnityEngine.AdaptivePerformance.StateAction ThermalAction { get; private set; }

		public global::UnityEngine.AdaptivePerformance.StateAction PerformanceAction { get; private set; }

		public void GetAppliedScalers(ref global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> scalers)
		{
			scalers.Clear();
			scalers.AddRange(m_AppliedScalers);
		}

		public void GetUnappliedScalers(ref global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> scalers)
		{
			scalers.Clear();
			scalers.AddRange(m_UnappliedScalers);
		}

		public void GetDisabledScalers(ref global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> scalers)
		{
			scalers.Clear();
			scalers.AddRange(m_DisabledScalers);
		}

		public void GetAllRegisteredScalers(ref global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> scalers)
		{
			scalers.Clear();
			scalers.AddRange(m_DisabledScalers);
			scalers.AddRange(m_UnappliedScalers);
			scalers.AddRange(m_AppliedScalers);
		}

		public void UnapplyAllScalers()
		{
			TimeUntilNextAction = m_Settings.indexerSettings.thermalActionDelay;
			while (m_AppliedScalers.Count != 0)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler = m_AppliedScalers[0];
				UnapplyScaler(scaler);
			}
		}

		internal void UpdateOverrideLevel(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler)
		{
			if (scaler.OverrideLevel != -1)
			{
				while (scaler.OverrideLevel > scaler.CurrentLevel)
				{
					ApplyScaler(scaler);
				}
				while (scaler.OverrideLevel < scaler.CurrentLevel)
				{
					UnapplyScaler(scaler);
				}
			}
		}

		internal void AddScaler(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler)
		{
			if (!m_UnappliedScalers.Contains(scaler) && !m_AppliedScalers.Contains(scaler))
			{
				m_UnappliedScalers.Add(scaler);
			}
		}

		internal bool RemoveScaler(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler)
		{
			bool result = false;
			if (m_UnappliedScalers.Contains(scaler))
			{
				m_UnappliedScalers.Remove(scaler);
				result = true;
			}
			if (m_DisabledScalers.Contains(scaler))
			{
				m_DisabledScalers.Remove(scaler);
				result = true;
			}
			if (m_AppliedScalers.Contains(scaler))
			{
				while (!scaler.NotLeveled)
				{
					scaler.DecreaseLevel();
				}
				m_AppliedScalers.Remove(scaler);
				result = true;
			}
			return result;
		}

		internal AdaptivePerformanceIndexer(ref global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings settings, global::UnityEngine.AdaptivePerformance.PerformanceStateTracker tracker)
		{
			m_Settings = settings;
			TimeUntilNextAction = m_Settings.indexerSettings.thermalActionDelay;
			m_ThermalStateTracker = new global::UnityEngine.AdaptivePerformance.ThermalStateTracker();
			m_PerformanceStateTracker = tracker;
			m_UnappliedScalers = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();
			m_AppliedScalers = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();
			m_DisabledScalers = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();
			m_ScalerEfficiencyTracker = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerEfficiencyTracker();
		}

		internal void Update()
		{
			if (global::UnityEngine.AdaptivePerformance.Holder.Instance == null || !m_Settings.indexerSettings.active)
			{
				return;
			}
			DeactivateDisabledScalers();
			ActivateEnabledScalers();
			global::UnityEngine.AdaptivePerformance.StateAction stateAction = m_ThermalStateTracker.Update();
			global::UnityEngine.AdaptivePerformance.StateAction stateAction2 = m_PerformanceStateTracker.Update();
			ThermalAction = stateAction;
			PerformanceAction = stateAction2;
			if (global::UnityEngine.Profiling.Profiler.enabled)
			{
				CollectProfilerStats();
			}
			TimeUntilNextAction = global::UnityEngine.Mathf.Max(TimeUntilNextAction - DeltaTime(), 0f);
			if (TimeUntilNextAction == 0f)
			{
				if (m_ScalerEfficiencyTracker.IsRunning)
				{
					m_ScalerEfficiencyTracker.Stop();
				}
				if (stateAction == global::UnityEngine.AdaptivePerformance.StateAction.Increase && stateAction2 == global::UnityEngine.AdaptivePerformance.StateAction.Stale)
				{
					UnapplyHighestCostScaler();
					TimeUntilNextAction = m_Settings.indexerSettings.thermalActionDelay;
				}
				else if (stateAction == global::UnityEngine.AdaptivePerformance.StateAction.Stale && stateAction2 == global::UnityEngine.AdaptivePerformance.StateAction.Stale)
				{
					UnapplyHighestCostScaler();
					TimeUntilNextAction = m_Settings.indexerSettings.thermalActionDelay;
				}
				else if (stateAction == global::UnityEngine.AdaptivePerformance.StateAction.Decrease)
				{
					ApplyLowestCostScaler();
					TimeUntilNextAction = m_Settings.indexerSettings.thermalActionDelay;
				}
				else if (stateAction2 == global::UnityEngine.AdaptivePerformance.StateAction.Decrease)
				{
					ApplyLowestCostScaler();
					TimeUntilNextAction = m_Settings.indexerSettings.performanceActionDelay;
				}
				else if (stateAction == global::UnityEngine.AdaptivePerformance.StateAction.FastDecrease)
				{
					ApplyLowestCostScaler();
					TimeUntilNextAction = m_Settings.indexerSettings.thermalActionDelay / 2f;
				}
				else if (stateAction2 == global::UnityEngine.AdaptivePerformance.StateAction.FastDecrease)
				{
					ApplyLowestCostScaler();
					TimeUntilNextAction = m_Settings.indexerSettings.performanceActionDelay / 2f;
				}
			}
		}

		protected virtual float DeltaTime()
		{
			return global::UnityEngine.Time.deltaTime;
		}

		private void CollectProfilerStats()
		{
			for (int num = m_UnappliedScalers.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler = m_UnappliedScalers[num];
			}
			for (int num2 = m_AppliedScalers.Count - 1; num2 >= 0; num2--)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler2 = m_AppliedScalers[num2];
			}
			for (int num3 = m_DisabledScalers.Count - 1; num3 >= 0; num3--)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler3 = m_DisabledScalers[num3];
			}
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceProfilerStats.FlushScalerDataToProfilerStream();
		}

		private void DeactivateDisabledScalers()
		{
			for (int num = m_UnappliedScalers.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler = m_UnappliedScalers[num];
				if (!adaptivePerformanceScaler.Enabled && !m_DisabledScalers.Contains(adaptivePerformanceScaler))
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("[Indexer] Deactivated " + adaptivePerformanceScaler.Name + " scaler.");
					adaptivePerformanceScaler.Deactivate();
					m_DisabledScalers.Add(adaptivePerformanceScaler);
					m_UnappliedScalers.RemoveAt(num);
				}
			}
			for (int num2 = m_AppliedScalers.Count - 1; num2 >= 0; num2--)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler2 = m_AppliedScalers[num2];
				if (!adaptivePerformanceScaler2.Enabled && !m_DisabledScalers.Contains(adaptivePerformanceScaler2))
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("[Indexer] Deactivated " + adaptivePerformanceScaler2.Name + " scaler.");
					adaptivePerformanceScaler2.Deactivate();
					m_DisabledScalers.Add(adaptivePerformanceScaler2);
					m_AppliedScalers.RemoveAt(num2);
				}
			}
		}

		private void ActivateEnabledScalers()
		{
			for (int num = m_DisabledScalers.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler = m_DisabledScalers[num];
				if (adaptivePerformanceScaler.Enabled)
				{
					adaptivePerformanceScaler.Activate();
					AddScaler(adaptivePerformanceScaler);
					m_DisabledScalers.RemoveAt(num);
					global::UnityEngine.AdaptivePerformance.APLog.Debug("[Indexer] Activated " + adaptivePerformanceScaler.Name + " scaler.");
				}
			}
		}

		private bool ApplyLowestCostScaler()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler = null;
			float num = float.PositiveInfinity;
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler unappliedScaler in m_UnappliedScalers)
			{
				if (unappliedScaler.Enabled && unappliedScaler.OverrideLevel == -1)
				{
					int num2 = unappliedScaler.CalculateCost();
					if (num > (float)num2)
					{
						adaptivePerformanceScaler = unappliedScaler;
						num = num2;
					}
				}
			}
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler appliedScaler in m_AppliedScalers)
			{
				if (appliedScaler.Enabled && appliedScaler.OverrideLevel == -1 && !appliedScaler.IsMaxLevel)
				{
					int num3 = appliedScaler.CalculateCost();
					if (num > (float)num3)
					{
						adaptivePerformanceScaler = appliedScaler;
						num = num3;
					}
				}
			}
			if (adaptivePerformanceScaler != null)
			{
				m_ScalerEfficiencyTracker.Start(adaptivePerformanceScaler, isApply: true);
				ApplyScaler(adaptivePerformanceScaler);
				return true;
			}
			return false;
		}

		private void ApplyScaler(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug($"[Indexer] Applying {scaler.Name} scaler at level {scaler.CurrentLevel} and try to increase level to {scaler.CurrentLevel + 1}");
			if (scaler.NotLeveled)
			{
				m_UnappliedScalers.Remove(scaler);
				m_AppliedScalers.Add(scaler);
			}
			scaler.IncreaseLevel();
		}

		private bool UnapplyHighestCostScaler()
		{
			global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler adaptivePerformanceScaler = null;
			float num = float.NegativeInfinity;
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler appliedScaler in m_AppliedScalers)
			{
				if (appliedScaler.OverrideLevel == -1)
				{
					int num2 = appliedScaler.CalculateCost();
					if (num < (float)num2)
					{
						adaptivePerformanceScaler = appliedScaler;
						num = num2;
					}
				}
			}
			if (adaptivePerformanceScaler != null)
			{
				m_ScalerEfficiencyTracker.Start(adaptivePerformanceScaler, isApply: false);
				UnapplyScaler(adaptivePerformanceScaler);
				return true;
			}
			return false;
		}

		private void UnapplyScaler(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler scaler)
		{
			global::UnityEngine.AdaptivePerformance.APLog.Debug($"[Indexer] Unapplying {scaler.Name} scaler at level {scaler.CurrentLevel} and try to decrease level to {scaler.CurrentLevel - 1}");
			scaler.DecreaseLevel();
			if (scaler.NotLeveled)
			{
				m_AppliedScalers.Remove(scaler);
				m_UnappliedScalers.Add(scaler);
			}
		}
	}
}
