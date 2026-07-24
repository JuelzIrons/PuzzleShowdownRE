namespace UnityEngine.Timeline
{
	public abstract class Marker : global::UnityEngine.ScriptableObject, global::UnityEngine.Timeline.IMarker
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Timeline.TimeField(global::UnityEngine.Timeline.TimeFieldAttribute.UseEditMode.ApplyEditMode)]
		[global::UnityEngine.Tooltip("Time for the marker")]
		private double m_Time;

		public global::UnityEngine.Timeline.TrackAsset parent { get; private set; }

		public double time
		{
			get
			{
				return m_Time;
			}
			set
			{
				m_Time = global::System.Math.Max(value, 0.0);
			}
		}

		void global::UnityEngine.Timeline.IMarker.Initialize(global::UnityEngine.Timeline.TrackAsset parentTrack)
		{
			if (parent == null)
			{
				parent = parentTrack;
				try
				{
					OnInitialize(parentTrack);
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogError(ex.Message, this);
				}
			}
		}

		public virtual void OnInitialize(global::UnityEngine.Timeline.TrackAsset aPent)
		{
		}
	}
}
