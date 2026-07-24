namespace Unity.Netcode
{
	public abstract class BufferedLinearInterpolator<T> where T : struct
	{
		protected internal struct BufferedItem
		{
			internal global::UnityEngine.Transform MeasurementParent;

			public int ItemId;

			public T Item;

			public double TimeSent;

			public BufferedItem(T item, double timeSent, int itemId)
			{
				Item = item;
				TimeSent = timeSent;
				ItemId = itemId;
				MeasurementParent = null;
			}

			public BufferedItem(T item, double timeSent)
			{
				Item = item;
				TimeSent = timeSent;
				ItemId = (int)(timeSent * 100.0);
				MeasurementParent = null;
			}
		}

		internal struct CurrentState
		{
			public global::UnityEngine.Transform TargetParent;

			public global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem? Target;

			public double StartTime;

			public double EndTime;

			public double TimeToTargetValue;

			public double DeltaTime;

			public double MaxDeltaTime;

			public double LastRemainingTime;

			public float LerpT;

			public bool TargetReached;

			public T CurrentValue;

			public T PreviousValue;

			public T NextValue;

			private float m_CurrentDeltaTime;

			public float CurrentDeltaTime => m_CurrentDeltaTime;

			public double FinalTimeToTarget => global::System.Math.Max(0.0, TimeToTargetValue - DeltaTime);

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void AddDeltaTime(float deltaTime)
			{
				m_CurrentDeltaTime = deltaTime;
				DeltaTime = global::System.Math.Min(DeltaTime + (double)deltaTime, TimeToTargetValue);
				LerpT = (float)((TimeToTargetValue == 0.0) ? 1.0 : (DeltaTime / TimeToTargetValue));
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void SetTimeToTarget(double timeToTarget)
			{
				LerpT = 0f;
				DeltaTime = 0.0;
				TimeToTargetValue = timeToTarget;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool TargetTimeAproximatelyReached()
			{
				if (!Target.HasValue)
				{
					return false;
				}
				return FinalTimeToTarget <= (double)(m_CurrentDeltaTime * 0.5f);
			}

			public void Reset(T currentValue)
			{
				Target = null;
				CurrentValue = currentValue;
				NextValue = currentValue;
				PreviousValue = currentValue;
				TargetReached = false;
				LerpT = 0f;
				EndTime = 0.0;
				StartTime = 0.0;
				TimeToTargetValue = 0.0;
				DeltaTime = 0.0;
				m_CurrentDeltaTime = 0f;
				MaxDeltaTime = 0.0;
				LastRemainingTime = 0.0;
			}
		}

		private const int k_BufferCountLimit = 100;

		private const float k_ApproximateLowPrecision = 1E-06f;

		private const float k_ApproximateHighPrecision = 1E-10f;

		private const double k_SmallValue = 9.999999439624929E-11;

		[global::System.Obsolete("This list is no longer used and will be deprecated.", false)]
		protected internal readonly global::System.Collections.Generic.List<global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem> m_Buffer = new global::System.Collections.Generic.List<global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem>();

		[global::System.Obsolete("This property will be deprecated.", false)]
		protected internal T m_InterpStartValue;

		[global::System.Obsolete("This property will be deprecated.", false)]
		protected internal T m_CurrentInterpValue;

		[global::System.Obsolete("This property will be deprecated.", false)]
		protected internal T m_InterpEndValue;

		internal bool LerpSmoothEnabled;

		[global::UnityEngine.Range(0.016f, 1f)]
		public float MaximumInterpolationTime = 0.1f;

		protected internal readonly global::System.Collections.Generic.Queue<global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem> m_BufferQueue = new global::System.Collections.Generic.Queue<global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem>(100);

		internal global::Unity.Netcode.BufferedLinearInterpolator<T>.CurrentState InterpolateState;

		internal float MaxInterpolationBound = 3f;

		internal bool AutoConvertTransformSpace;

		internal bool InLocalSpace;

		internal global::UnityEngine.Transform Parent;

		private double m_LastMeasurementAddedTime;

		private int m_BufferCount;

		private int m_NbItemsReceivedThisFrame;

		private global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem m_LastBufferedItemReceived;

		private T m_RateOfChange;

		internal bool EndOfBuffer => m_BufferQueue.Count == 0;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private float GetPrecision()
		{
			if (m_BufferQueue.Count != 0)
			{
				return 1E-06f;
			}
			return 1E-10f;
		}

		public void Clear()
		{
			m_BufferQueue.Clear();
			m_BufferCount = 0;
			m_LastMeasurementAddedTime = 0.0;
			InterpolateState.Reset(default(T));
			m_RateOfChange = default(T);
		}

		public void ResetTo(T targetValue, double serverTime)
		{
			ResetTo(null, targetValue, serverTime);
		}

		internal void ResetTo(global::UnityEngine.Transform parent, T targetValue, double serverTime)
		{
			Clear();
			InternalReset(parent, targetValue, serverTime);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ConvertInterpolateStateValues(global::UnityEngine.Transform parent, bool inLocalSpace)
		{
			InterpolateState.CurrentValue = OnConvertTransformSpace(parent, InterpolateState.CurrentValue, inLocalSpace);
			InterpolateState.NextValue = OnConvertTransformSpace(parent, InterpolateState.NextValue, inLocalSpace);
			InterpolateState.PreviousValue = OnConvertTransformSpace(parent, InterpolateState.PreviousValue, inLocalSpace);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ConvertTransformSpace(global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem newTarget)
		{
			if (AutoConvertTransformSpace && InterpolateState.TargetParent != newTarget.MeasurementParent)
			{
				if (InterpolateState.TargetParent != null)
				{
					ConvertInterpolateStateValues(InterpolateState.TargetParent, inLocalSpace: false);
				}
				if (newTarget.MeasurementParent != null)
				{
					ConvertInterpolateStateValues(newTarget.MeasurementParent, inLocalSpace: true);
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void InternalReset(global::UnityEngine.Transform parent, T targetValue, double serverTime, bool addMeasurement = true)
		{
			m_RateOfChange = default(T);
			InterpolateState.Reset(targetValue);
			InterpolateState.TargetParent = parent;
			if (addMeasurement)
			{
				AddMeasurement(parent, targetValue, serverTime);
			}
		}

		private void TryConsumeFromBuffer(double renderTime, double minDeltaTime, double maxDeltaTime)
		{
			global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem? bufferedItem = null;
			double num = 0.0;
			bool flag = false;
			bool flag2 = !InterpolateState.Target.HasValue;
			bool flag3 = false;
			if (!flag2 && !InterpolateState.TargetReached)
			{
				InterpolateState.TargetReached = IsApproximately(InterpolateState.CurrentValue, InterpolateState.Target.Value.Item, GetPrecision());
			}
			global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem result;
			while (m_BufferQueue.TryPeek(out result) && (!bufferedItem.HasValue || bufferedItem.Value.TimeSent != result.TimeSent))
			{
				if (!flag2)
				{
					flag3 = result.TimeSent <= renderTime && result.TimeSent > InterpolateState.Target.Value.TimeSent;
				}
				if (!((flag2 && result.TimeSent <= renderTime) || flag3))
				{
					break;
				}
				if (m_BufferQueue.TryDequeue(out var result2))
				{
					ConvertTransformSpace(result2);
					if (!InterpolateState.Target.HasValue)
					{
						InterpolateState.Target = result2;
						flag = true;
						InterpolateState.NextValue = InterpolateState.CurrentValue;
						InterpolateState.PreviousValue = InterpolateState.CurrentValue;
						InterpolateState.SetTimeToTarget(minDeltaTime);
						num = InterpolateState.Target.Value.TimeSent;
						InterpolateState.TargetReached = false;
						InterpolateState.MaxDeltaTime = maxDeltaTime;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							InterpolateState.LastRemainingTime = InterpolateState.FinalTimeToTarget;
							InterpolateState.TargetReached = false;
							InterpolateState.MaxDeltaTime = maxDeltaTime;
							InterpolateState.PreviousValue = InterpolateState.NextValue;
							num = InterpolateState.Target.Value.TimeSent;
						}
						InterpolateState.SetTimeToTarget(global::System.Math.Max(result2.TimeSent - num, minDeltaTime));
						InterpolateState.Target = result2;
					}
					InterpolateState.TargetParent = result2.MeasurementParent;
				}
				if (InterpolateState.Target.HasValue)
				{
					bufferedItem = result;
					continue;
				}
				break;
			}
		}

		internal void ResetCurrentState()
		{
			if (InterpolateState.Target.HasValue)
			{
				InterpolateState.Reset(InterpolateState.CurrentValue);
				m_RateOfChange = default(T);
			}
		}

		internal T Update(float deltaTime, double tickLatencyAsTime, double minDeltaTime, double maxDeltaTime, bool lerp)
		{
			TryConsumeFromBuffer(tickLatencyAsTime, minDeltaTime, maxDeltaTime);
			if (InterpolateState.Target.HasValue)
			{
				if (!InterpolateState.TargetReached)
				{
					InterpolateState.AddDeltaTime(deltaTime);
					if (!lerp)
					{
						InterpolateState.NextValue = SmoothDamp(InterpolateState.NextValue, InterpolateState.Target.Value.Item, ref m_RateOfChange, (float)InterpolateState.TimeToTargetValue * InterpolateState.LerpT, deltaTime);
					}
					else
					{
						InterpolateState.NextValue = Interpolate(InterpolateState.PreviousValue, InterpolateState.Target.Value.Item, InterpolateState.LerpT);
					}
					if (LerpSmoothEnabled)
					{
						InterpolateState.CurrentValue = Interpolate(InterpolateState.CurrentValue, InterpolateState.NextValue, global::UnityEngine.Mathf.Clamp(1f - MaximumInterpolationTime, 0f, 1f));
					}
					else
					{
						InterpolateState.CurrentValue = InterpolateState.NextValue;
					}
				}
				else if (m_BufferQueue.Count == 0 && tickLatencyAsTime - InterpolateState.Target.Value.TimeSent > InterpolateState.MaxDeltaTime + minDeltaTime)
				{
					InterpolateState.Reset(InterpolateState.CurrentValue);
				}
			}
			m_NbItemsReceivedThisFrame = 0;
			return InterpolateState.CurrentValue;
		}

		private void TryConsumeFromBuffer(double renderTime, double serverTime)
		{
			if (InterpolateState.Target.HasValue && !(InterpolateState.Target.Value.TimeSent <= renderTime))
			{
				return;
			}
			global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem? bufferedItem = null;
			bool flag = false;
			global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem result;
			while (m_BufferQueue.TryPeek(out result) && (!bufferedItem.HasValue || bufferedItem.Value.TimeSent != result.TimeSent))
			{
				if (result.TimeSent <= serverTime && (!InterpolateState.Target.HasValue || result.TimeSent > InterpolateState.Target.Value.TimeSent) && m_BufferQueue.TryDequeue(out var result2))
				{
					ConvertTransformSpace(result2);
					if (!InterpolateState.Target.HasValue)
					{
						InterpolateState.Target = result2;
						flag = true;
						InterpolateState.NextValue = InterpolateState.CurrentValue;
						InterpolateState.PreviousValue = InterpolateState.CurrentValue;
						InterpolateState.StartTime = result2.TimeSent;
						InterpolateState.EndTime = result2.TimeSent;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							InterpolateState.StartTime = InterpolateState.Target.Value.TimeSent;
							InterpolateState.PreviousValue = InterpolateState.NextValue;
							InterpolateState.TargetReached = false;
						}
						InterpolateState.EndTime = result2.TimeSent;
						InterpolateState.TimeToTargetValue = InterpolateState.EndTime - InterpolateState.StartTime;
						InterpolateState.Target = result2;
					}
					InterpolateState.TargetParent = result2.MeasurementParent;
				}
				if (InterpolateState.Target.HasValue)
				{
					bufferedItem = result;
					continue;
				}
				break;
			}
		}

		public T Update(float deltaTime, double renderTime, double serverTime)
		{
			TryConsumeFromBuffer(renderTime, serverTime);
			if (!InterpolateState.TargetReached && InterpolateState.Target.HasValue)
			{
				InterpolateState.LerpT = 1f;
				if (InterpolateState.TimeToTargetValue > 9.999999439624929E-11)
				{
					InterpolateState.LerpT = global::System.Math.Clamp((float)((renderTime - InterpolateState.StartTime) / InterpolateState.TimeToTargetValue), 0f, 1f);
				}
				InterpolateState.NextValue = Interpolate(InterpolateState.PreviousValue, InterpolateState.Target.Value.Item, InterpolateState.LerpT);
				if (LerpSmoothEnabled)
				{
					InterpolateState.CurrentValue = Interpolate(InterpolateState.CurrentValue, InterpolateState.NextValue, deltaTime / MaximumInterpolationTime);
				}
				else
				{
					InterpolateState.CurrentValue = InterpolateState.NextValue;
				}
				InterpolateState.TargetReached = IsApproximately(InterpolateState.CurrentValue, InterpolateState.Target.Value.Item, GetPrecision());
			}
			else if (InterpolateState.TargetReached && m_BufferQueue.Count == 0 && renderTime - InterpolateState.Target.Value.TimeSent > 0.30000001192092896)
			{
				InterpolateState.Reset(InterpolateState.CurrentValue);
			}
			m_NbItemsReceivedThisFrame = 0;
			return InterpolateState.CurrentValue;
		}

		[global::System.Obsolete("This method is being deprecated due to it being only used for internal testing purposes.", false)]
		public T Update(float deltaTime, global::Unity.Netcode.NetworkTime serverTime)
		{
			return UpdateInternal(deltaTime, serverTime);
		}

		internal T UpdateInternal(float deltaTime, global::Unity.Netcode.NetworkTime serverTime, int ticksAgo = 1)
		{
			return Update(deltaTime, serverTime.TimeTicksAgo(ticksAgo).Time, serverTime.Time);
		}

		public void AddMeasurement(T newMeasurement, double sentTime)
		{
			AddMeasurement(null, newMeasurement, sentTime);
		}

		internal void AddMeasurement(global::UnityEngine.Transform parent, T newMeasurement, double sentTime)
		{
			m_NbItemsReceivedThisFrame++;
			if (m_NbItemsReceivedThisFrame > 100)
			{
				if (m_LastBufferedItemReceived.TimeSent < sentTime)
				{
					Clear();
					InternalReset(parent, newMeasurement, sentTime, addMeasurement: false);
					m_LastMeasurementAddedTime = sentTime;
					m_LastBufferedItemReceived = new global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem(newMeasurement, sentTime, m_BufferCount)
					{
						MeasurementParent = parent
					};
					m_BufferQueue.Enqueue(m_LastBufferedItemReceived);
				}
			}
			else if (sentTime > m_LastMeasurementAddedTime || m_BufferCount == 0)
			{
				m_BufferCount++;
				m_LastBufferedItemReceived = new global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem(newMeasurement, sentTime, m_BufferCount)
				{
					MeasurementParent = parent
				};
				m_BufferQueue.Enqueue(m_LastBufferedItemReceived);
				m_LastMeasurementAddedTime = sentTime;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private T GetParentRelativeValue(T currentValue)
		{
			if (!AutoConvertTransformSpace || InterpolateState.TargetParent == Parent)
			{
				return currentValue;
			}
			if ((bool)InterpolateState.TargetParent)
			{
				currentValue = OnConvertTransformSpace(InterpolateState.TargetParent, currentValue, inLocalSpace: false);
			}
			if (Parent != null)
			{
				currentValue = OnConvertTransformSpace(Parent, currentValue, inLocalSpace: true);
			}
			return currentValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public T GetInterpolatedValue()
		{
			T val = InterpolateState.CurrentValue;
			if (AutoConvertTransformSpace && InterpolateState.TargetParent != Parent)
			{
				val = GetParentRelativeValue(val);
				if (m_BufferQueue.Count == 0 && (InterpolateState.TargetReached || !InterpolateState.Target.HasValue))
				{
					InterpolateState.CurrentValue = val;
					InterpolateState.NextValue = val;
					InterpolateState.PreviousValue = val;
					InterpolateState.TargetParent = Parent;
				}
			}
			return val;
		}

		protected abstract T Interpolate(T start, T end, float time);

		protected abstract T InterpolateUnclamped(T start, T end, float time);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected virtual T SmoothDamp(T current, T target, ref T rateOfChange, float duration, float deltaTime, float maxSpeed = float.PositiveInfinity)
		{
			return target;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected virtual bool IsApproximately(T first, T second, float precision = 1E-06f)
		{
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected internal virtual T OnConvertTransformSpace(global::UnityEngine.Transform transform, T item, bool inLocalSpace)
		{
			return default(T);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void ConvertTransformSpace(global::UnityEngine.Transform transform, bool inLocalSpace)
		{
			int count = m_BufferQueue.Count;
			for (int i = 0; i < count; i++)
			{
				global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem item = m_BufferQueue.Dequeue();
				item.Item = OnConvertTransformSpace(transform, item.Item, inLocalSpace);
				m_BufferQueue.Enqueue(item);
			}
			InterpolateState.CurrentValue = OnConvertTransformSpace(transform, InterpolateState.CurrentValue, inLocalSpace);
			if (InterpolateState.Target.HasValue)
			{
				global::Unity.Netcode.BufferedLinearInterpolator<T>.BufferedItem value = InterpolateState.Target.Value;
				value.Item = OnConvertTransformSpace(transform, value.Item, inLocalSpace);
				InterpolateState.Target = value;
			}
			InLocalSpace = inLocalSpace;
		}
	}
}
