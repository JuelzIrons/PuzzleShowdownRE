namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	public class TimelineClip : global::UnityEngine.Timeline.ICurvesOwner, global::UnityEngine.ISerializationCallbackReceiver
	{
		private enum Versions
		{
			Initial = 0,
			ClipInFromGlobalToLocal = 1
		}

		private static class TimelineClipUpgrade
		{
			public static void UpgradeClipInFromGlobalToLocal(global::UnityEngine.Timeline.TimelineClip clip)
			{
				if (clip.m_ClipIn > 0.0 && clip.m_TimeScale > 1.401298464324817E-45)
				{
					clip.m_ClipIn *= clip.m_TimeScale;
				}
			}
		}

		public enum ClipExtrapolation
		{
			None = 0,
			Hold = 1,
			Loop = 2,
			PingPong = 3,
			Continue = 4
		}

		public enum BlendCurveMode
		{
			Auto = 0,
			Manual = 1
		}

		private const int k_LatestVersion = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		public static readonly global::UnityEngine.Timeline.ClipCaps kDefaultClipCaps = global::UnityEngine.Timeline.ClipCaps.Blending;

		public static readonly float kDefaultClipDurationInSeconds = 5f;

		public static readonly double kTimeScaleMin = 0.001;

		public static readonly double kTimeScaleMax = 1000.0;

		internal static readonly string kDefaultCurvesName = "Clip Parameters";

		internal static readonly double kMinDuration = 1.0 / 60.0;

		internal static readonly double kMaxTimeValue = 1000000.0;

		[global::UnityEngine.SerializeField]
		private double m_Start;

		[global::UnityEngine.SerializeField]
		private double m_ClipIn;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Object m_Asset;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_HackDuration")]
		private double m_Duration;

		[global::UnityEngine.SerializeField]
		private double m_TimeScale = 1.0;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TrackAsset m_ParentTrack;

		[global::UnityEngine.SerializeField]
		private double m_EaseInDuration;

		[global::UnityEngine.SerializeField]
		private double m_EaseOutDuration;

		[global::UnityEngine.SerializeField]
		private double m_BlendInDuration = -1.0;

		[global::UnityEngine.SerializeField]
		private double m_BlendOutDuration = -1.0;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AnimationCurve m_MixInCurve;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AnimationCurve m_MixOutCurve;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TimelineClip.BlendCurveMode m_BlendInCurveMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TimelineClip.BlendCurveMode m_BlendOutCurveMode;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<string> m_ExposedParameterNames;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AnimationClip m_AnimationCurves;

		[global::UnityEngine.SerializeField]
		private bool m_Recordable;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_PostExtrapolationMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_PreExtrapolationMode;

		[global::UnityEngine.SerializeField]
		private double m_PostExtrapolationTime;

		[global::UnityEngine.SerializeField]
		private double m_PreExtrapolationTime;

		[global::UnityEngine.SerializeField]
		private string m_DisplayName;

		public bool hasPreExtrapolation
		{
			get
			{
				if (m_PreExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None)
				{
					return m_PreExtrapolationTime > 0.0;
				}
				return false;
			}
		}

		public bool hasPostExtrapolation
		{
			get
			{
				if (m_PostExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None)
				{
					return m_PostExtrapolationTime > 0.0;
				}
				return false;
			}
		}

		public double timeScale
		{
			get
			{
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.SpeedMultiplier))
				{
					return 1.0;
				}
				return global::System.Math.Max(kTimeScaleMin, global::System.Math.Min(m_TimeScale, kTimeScaleMax));
			}
			set
			{
				UpdateDirty(m_TimeScale, value);
				m_TimeScale = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.SpeedMultiplier) ? global::System.Math.Max(kTimeScaleMin, global::System.Math.Min(value, kTimeScaleMax)) : 1.0);
			}
		}

		public double start
		{
			get
			{
				return m_Start;
			}
			set
			{
				double num = global::System.Math.Max(SanitizeTimeValue(value, m_Start), 0.0);
				if (global::System.Math.Abs(m_Start - num) > double.Epsilon)
				{
					UpdateDirty(m_Start, num);
					m_Start = num;
					if (m_ParentTrack != null)
					{
						m_ParentTrack.OnClipMove(asset as global::UnityEngine.Timeline.ITimelineClipAsset);
					}
				}
			}
		}

		public double duration
		{
			get
			{
				return m_Duration;
			}
			set
			{
				double num = global::System.Math.Max(SanitizeTimeValue(value, m_Duration), double.Epsilon);
				if (global::System.Math.Abs(m_Duration - num) > double.Epsilon)
				{
					UpdateDirty(m_Duration, num);
					m_Duration = num;
					if (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending) && m_ParentTrack != null)
					{
						m_ParentTrack.blendsValid = false;
					}
				}
			}
		}

		public double end => m_Start + m_Duration;

		public double clipIn
		{
			get
			{
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.ClipIn))
				{
					return 0.0;
				}
				return m_ClipIn;
			}
			set
			{
				UpdateDirty(m_ClipIn, value);
				m_ClipIn = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.ClipIn) ? global::System.Math.Max(global::System.Math.Min(SanitizeTimeValue(value, m_ClipIn), kMaxTimeValue), 0.0) : 0.0);
			}
		}

		public string displayName
		{
			get
			{
				return m_DisplayName;
			}
			set
			{
				m_DisplayName = value;
			}
		}

		public double clipAssetDuration
		{
			get
			{
				if (!(m_Asset is global::UnityEngine.Playables.IPlayableAsset playableAsset))
				{
					return double.MaxValue;
				}
				return playableAsset.duration;
			}
		}

		public global::UnityEngine.AnimationClip curves
		{
			get
			{
				return m_AnimationCurves;
			}
			internal set
			{
				m_AnimationCurves = value;
			}
		}

		string global::UnityEngine.Timeline.ICurvesOwner.defaultCurvesName => kDefaultCurvesName;

		public bool hasCurves
		{
			get
			{
				if (m_AnimationCurves != null)
				{
					return !m_AnimationCurves.empty;
				}
				return false;
			}
		}

		public global::UnityEngine.Object asset
		{
			get
			{
				return m_Asset;
			}
			set
			{
				m_Asset = value;
			}
		}

		global::UnityEngine.Object global::UnityEngine.Timeline.ICurvesOwner.assetOwner => GetParentTrack();

		global::UnityEngine.Timeline.TrackAsset global::UnityEngine.Timeline.ICurvesOwner.targetTrack => GetParentTrack();

		[global::System.Obsolete("underlyingAsset property is obsolete. Use asset property instead", true)]
		public global::UnityEngine.Object underlyingAsset
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[global::System.Obsolete("parentTrack is deprecated and will be removed in a future release. Use GetParentTrack() and TimelineClipExtensions::MoveToTrack() or TimelineClipExtensions::TryMoveToTrack() instead.", false)]
		public global::UnityEngine.Timeline.TrackAsset parentTrack
		{
			get
			{
				return m_ParentTrack;
			}
			set
			{
				SetParentTrack_Internal(value);
			}
		}

		public double easeInDuration
		{
			get
			{
				double val = (hasBlendOut ? (duration - m_BlendOutDuration) : duration);
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
				{
					return 0.0;
				}
				return global::System.Math.Min(global::System.Math.Max(m_EaseInDuration, 0.0), val);
			}
			set
			{
				double val = (hasBlendOut ? (duration - m_BlendOutDuration) : duration);
				m_EaseInDuration = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending) ? global::System.Math.Max(0.0, global::System.Math.Min(SanitizeTimeValue(value, m_EaseInDuration), val)) : 0.0);
			}
		}

		public double easeOutDuration
		{
			get
			{
				double val = (hasBlendIn ? (duration - m_BlendInDuration) : duration);
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
				{
					return 0.0;
				}
				return global::System.Math.Min(global::System.Math.Max(m_EaseOutDuration, 0.0), val);
			}
			set
			{
				double val = (hasBlendIn ? (duration - m_BlendInDuration) : duration);
				m_EaseOutDuration = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending) ? global::System.Math.Max(0.0, global::System.Math.Min(SanitizeTimeValue(value, m_EaseOutDuration), val)) : 0.0);
			}
		}

		[global::System.Obsolete("Use easeOutTime instead (UnityUpgradable) -> easeOutTime", true)]
		public double eastOutTime => duration - easeOutDuration + m_Start;

		public double easeOutTime => duration - easeOutDuration + m_Start;

		public double blendInDuration
		{
			get
			{
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
				{
					return 0.0;
				}
				return m_BlendInDuration;
			}
			set
			{
				m_BlendInDuration = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending) ? SanitizeTimeValue(value, m_BlendInDuration) : 0.0);
			}
		}

		public double blendOutDuration
		{
			get
			{
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
				{
					return 0.0;
				}
				return m_BlendOutDuration;
			}
			set
			{
				m_BlendOutDuration = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending) ? SanitizeTimeValue(value, m_BlendOutDuration) : 0.0);
			}
		}

		public global::UnityEngine.Timeline.TimelineClip.BlendCurveMode blendInCurveMode
		{
			get
			{
				return m_BlendInCurveMode;
			}
			set
			{
				m_BlendInCurveMode = value;
			}
		}

		public global::UnityEngine.Timeline.TimelineClip.BlendCurveMode blendOutCurveMode
		{
			get
			{
				return m_BlendOutCurveMode;
			}
			set
			{
				m_BlendOutCurveMode = value;
			}
		}

		public bool hasBlendIn
		{
			get
			{
				if (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
				{
					return m_BlendInDuration > 0.0;
				}
				return false;
			}
		}

		public bool hasBlendOut
		{
			get
			{
				if (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
				{
					return m_BlendOutDuration > 0.0;
				}
				return false;
			}
		}

		public global::UnityEngine.AnimationCurve mixInCurve
		{
			get
			{
				if (m_MixInCurve == null || m_MixInCurve.length < 2)
				{
					m_MixInCurve = GetDefaultMixInCurve();
				}
				return m_MixInCurve;
			}
			set
			{
				m_MixInCurve = value;
			}
		}

		public float mixInPercentage => (float)(mixInDuration / duration);

		public double mixInDuration
		{
			get
			{
				if (!hasBlendIn)
				{
					return easeInDuration;
				}
				return blendInDuration;
			}
		}

		public global::UnityEngine.AnimationCurve mixOutCurve
		{
			get
			{
				if (m_MixOutCurve == null || m_MixOutCurve.length < 2)
				{
					m_MixOutCurve = GetDefaultMixOutCurve();
				}
				return m_MixOutCurve;
			}
			set
			{
				m_MixOutCurve = value;
			}
		}

		public double mixOutTime => duration - mixOutDuration + m_Start;

		public double mixOutDuration
		{
			get
			{
				if (!hasBlendOut)
				{
					return easeOutDuration;
				}
				return blendOutDuration;
			}
		}

		public float mixOutPercentage => (float)(mixOutDuration / duration);

		public bool recordable
		{
			get
			{
				return m_Recordable;
			}
			internal set
			{
				m_Recordable = value;
			}
		}

		[global::System.Obsolete("exposedParameter is deprecated and will be removed in a future release", true)]
		public global::System.Collections.Generic.List<string> exposedParameters => m_ExposedParameterNames ?? (m_ExposedParameterNames = new global::System.Collections.Generic.List<string>());

		public global::UnityEngine.Timeline.ClipCaps clipCaps
		{
			get
			{
				if (!(asset is global::UnityEngine.Timeline.ITimelineClipAsset timelineClipAsset))
				{
					return kDefaultClipCaps;
				}
				return timelineClipAsset.clipCaps;
			}
		}

		public global::UnityEngine.AnimationClip animationClip
		{
			get
			{
				if (m_Asset == null)
				{
					return null;
				}
				global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = m_Asset as global::UnityEngine.Timeline.AnimationPlayableAsset;
				if (!(animationPlayableAsset != null))
				{
					return null;
				}
				return animationPlayableAsset.clip;
			}
		}

		public global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation postExtrapolationMode
		{
			get
			{
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Extrapolation))
				{
					return global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None;
				}
				return m_PostExtrapolationMode;
			}
			internal set
			{
				m_PostExtrapolationMode = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Extrapolation) ? value : global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None);
			}
		}

		public global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation preExtrapolationMode
		{
			get
			{
				if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Extrapolation))
				{
					return global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None;
				}
				return m_PreExtrapolationMode;
			}
			internal set
			{
				m_PreExtrapolationMode = (clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Extrapolation) ? value : global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None);
			}
		}

		public double extrapolatedStart
		{
			get
			{
				if (m_PreExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None)
				{
					return m_Start - m_PreExtrapolationTime;
				}
				return m_Start;
			}
		}

		public double extrapolatedDuration
		{
			get
			{
				double num = m_Duration;
				if (m_PostExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None)
				{
					num += global::System.Math.Min(m_PostExtrapolationTime, kMaxTimeValue);
				}
				if (m_PreExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None)
				{
					num += m_PreExtrapolationTime;
				}
				return num;
			}
		}

		private void UpgradeToLatestVersion()
		{
			if (m_Version < 1)
			{
				global::UnityEngine.Timeline.TimelineClip.TimelineClipUpgrade.UpgradeClipInFromGlobalToLocal(this);
			}
		}

		internal TimelineClip(global::UnityEngine.Timeline.TrackAsset parent)
		{
			SetParentTrack_Internal(parent);
		}

		public global::UnityEngine.Timeline.TrackAsset GetParentTrack()
		{
			return m_ParentTrack;
		}

		internal void SetParentTrack_Internal(global::UnityEngine.Timeline.TrackAsset newParentTrack)
		{
			if (!(m_ParentTrack == newParentTrack))
			{
				if (m_ParentTrack != null)
				{
					m_ParentTrack.RemoveClip(this);
				}
				m_ParentTrack = newParentTrack;
				if (m_ParentTrack != null)
				{
					m_ParentTrack.AddClip(this);
				}
			}
		}

		internal int Hash()
		{
			int hashCode = m_Start.GetHashCode();
			int hashCode2 = m_Duration.GetHashCode();
			int hashCode3 = m_TimeScale.GetHashCode();
			int hashCode4 = m_ClipIn.GetHashCode();
			int num = (int)m_PreExtrapolationMode;
			int hashCode5 = num.GetHashCode();
			num = (int)m_PostExtrapolationMode;
			return global::UnityEngine.Timeline.HashUtility.CombineHash(hashCode, hashCode2, hashCode3, hashCode4, hashCode5, num.GetHashCode());
		}

		public float EvaluateMixOut(double time)
		{
			if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
			{
				return 1f;
			}
			if (mixOutDuration > (double)global::UnityEngine.Mathf.Epsilon)
			{
				float time2 = (float)(time - mixOutTime) / (float)mixOutDuration;
				return global::UnityEngine.Mathf.Clamp01(mixOutCurve.Evaluate(time2));
			}
			return 1f;
		}

		public float EvaluateMixIn(double time)
		{
			if (!clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
			{
				return 1f;
			}
			if (mixInDuration > (double)global::UnityEngine.Mathf.Epsilon)
			{
				float time2 = (float)(time - m_Start) / (float)mixInDuration;
				return global::UnityEngine.Mathf.Clamp01(mixInCurve.Evaluate(time2));
			}
			return 1f;
		}

		private static global::UnityEngine.AnimationCurve GetDefaultMixInCurve()
		{
			return global::UnityEngine.AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}

		private static global::UnityEngine.AnimationCurve GetDefaultMixOutCurve()
		{
			return global::UnityEngine.AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
		}

		public double ToLocalTime(double time)
		{
			if (time < 0.0)
			{
				return time;
			}
			time = (IsPreExtrapolatedTime(time) ? GetExtrapolatedTime(time - m_Start, m_PreExtrapolationMode, m_Duration) : ((!IsPostExtrapolatedTime(time)) ? (time - m_Start) : GetExtrapolatedTime(time - m_Start, m_PostExtrapolationMode, m_Duration)));
			time *= timeScale;
			time += clipIn;
			return time;
		}

		public double ToLocalTimeUnbound(double time)
		{
			return (time - m_Start) * timeScale + clipIn;
		}

		internal double FromLocalTimeUnbound(double time)
		{
			return (time - clipIn) / timeScale + m_Start;
		}

		private static double SanitizeTimeValue(double value, double defaultValue)
		{
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				global::UnityEngine.Debug.LogError("Invalid time value assigned");
				return defaultValue;
			}
			return global::System.Math.Max(0.0 - kMaxTimeValue, global::System.Math.Min(kMaxTimeValue, value));
		}

		internal void SetPostExtrapolationTime(double time)
		{
			m_PostExtrapolationTime = time;
		}

		internal void SetPreExtrapolationTime(double time)
		{
			m_PreExtrapolationTime = time;
		}

		public bool IsExtrapolatedTime(double sequenceTime)
		{
			if (!IsPreExtrapolatedTime(sequenceTime))
			{
				return IsPostExtrapolatedTime(sequenceTime);
			}
			return true;
		}

		public bool IsPreExtrapolatedTime(double sequenceTime)
		{
			if (preExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None && sequenceTime < m_Start)
			{
				return sequenceTime >= m_Start - m_PreExtrapolationTime;
			}
			return false;
		}

		public bool IsPostExtrapolatedTime(double sequenceTime)
		{
			if (postExtrapolationMode != global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None && sequenceTime > end)
			{
				return sequenceTime - end < m_PostExtrapolationTime;
			}
			return false;
		}

		private static double GetExtrapolatedTime(double time, global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation mode, double duration)
		{
			if (duration == 0.0)
			{
				return 0.0;
			}
			switch (mode)
			{
			case global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.Loop:
				if (time < 0.0)
				{
					time = duration - (0.0 - time) % duration;
				}
				else if (time > duration)
				{
					time %= duration;
				}
				break;
			case global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.Hold:
				if (time < 0.0)
				{
					return 0.0;
				}
				if (time > duration)
				{
					return duration;
				}
				break;
			case global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.PingPong:
				if (time < 0.0)
				{
					time = duration * 2.0 - (0.0 - time) % (duration * 2.0);
					time = duration - global::System.Math.Abs(time - duration);
				}
				else
				{
					time %= duration * 2.0;
					time = duration - global::System.Math.Abs(time - duration);
				}
				break;
			}
			return time;
		}

		public void CreateCurves(string curvesClipName)
		{
			if (!(m_AnimationCurves != null))
			{
				m_AnimationCurves = global::UnityEngine.Timeline.TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? kDefaultCurvesName : curvesClipName, GetParentTrack(), isLegacy: true);
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_Version = 1;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_Version < 1)
			{
				UpgradeToLatestVersion();
			}
		}

		public override string ToString()
		{
			return $"{displayName} ({start:F2}, {end:F2}):{clipIn:F2} | {GetParentTrack()}";
		}

		public void ConformEaseValues()
		{
			if (m_EaseInDuration + m_EaseOutDuration > duration)
			{
				double num = CalculateEasingRatio(m_EaseInDuration, m_EaseOutDuration);
				m_EaseInDuration = duration * num;
				m_EaseOutDuration = duration * (1.0 - num);
			}
		}

		private static double CalculateEasingRatio(double easeIn, double easeOut)
		{
			if (global::System.Math.Abs(easeIn - easeOut) < global::UnityEngine.Timeline.TimeUtility.kTimeEpsilon)
			{
				return 0.5;
			}
			if (easeIn == 0.0)
			{
				return 0.0;
			}
			if (easeOut == 0.0)
			{
				return 1.0;
			}
			return easeIn / (easeIn + easeOut);
		}

		private void UpdateDirty(double oldValue, double newValue)
		{
		}
	}
}
