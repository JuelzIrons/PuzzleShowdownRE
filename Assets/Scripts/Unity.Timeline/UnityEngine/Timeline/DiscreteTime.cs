namespace UnityEngine.Timeline
{
	internal struct DiscreteTime : global::System.IComparable
	{
		private const double k_Tick = 1E-12;

		public static readonly global::UnityEngine.Timeline.DiscreteTime kMaxTime = new global::UnityEngine.Timeline.DiscreteTime(long.MaxValue);

		private readonly long m_DiscreteTime;

		public static double tickValue => 1E-12;

		public DiscreteTime(global::UnityEngine.Timeline.DiscreteTime time)
		{
			m_DiscreteTime = time.m_DiscreteTime;
		}

		private DiscreteTime(long time)
		{
			m_DiscreteTime = time;
		}

		public DiscreteTime(double time)
		{
			m_DiscreteTime = DoubleToDiscreteTime(time);
		}

		public DiscreteTime(float time)
		{
			m_DiscreteTime = FloatToDiscreteTime(time);
		}

		public DiscreteTime(int time)
		{
			m_DiscreteTime = IntToDiscreteTime(time);
		}

		public DiscreteTime(int frame, double fps)
		{
			m_DiscreteTime = DoubleToDiscreteTime((double)frame * fps);
		}

		public global::UnityEngine.Timeline.DiscreteTime OneTickBefore()
		{
			return new global::UnityEngine.Timeline.DiscreteTime(m_DiscreteTime - 1);
		}

		public global::UnityEngine.Timeline.DiscreteTime OneTickAfter()
		{
			return new global::UnityEngine.Timeline.DiscreteTime(m_DiscreteTime + 1);
		}

		public long GetTick()
		{
			return m_DiscreteTime;
		}

		public static global::UnityEngine.Timeline.DiscreteTime FromTicks(long ticks)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(ticks);
		}

		public int CompareTo(object obj)
		{
			if (obj is global::UnityEngine.Timeline.DiscreteTime)
			{
				return m_DiscreteTime.CompareTo(((global::UnityEngine.Timeline.DiscreteTime)obj).m_DiscreteTime);
			}
			return 1;
		}

		public bool Equals(global::UnityEngine.Timeline.DiscreteTime other)
		{
			return m_DiscreteTime == other.m_DiscreteTime;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Timeline.DiscreteTime)
			{
				return Equals((global::UnityEngine.Timeline.DiscreteTime)obj);
			}
			return false;
		}

		private static long DoubleToDiscreteTime(double time)
		{
			double num = time / 1E-12 + 0.5;
			if (num < 9.223372036854776E+18 && num > -9.223372036854776E+18)
			{
				return (long)num;
			}
			throw new global::System.ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		private static long FloatToDiscreteTime(float time)
		{
			float num = time / 1E-12f + 0.5f;
			if (num < 9.223372E+18f && num > -9.223372E+18f)
			{
				return (long)num;
			}
			throw new global::System.ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		private static long IntToDiscreteTime(int time)
		{
			return DoubleToDiscreteTime(time);
		}

		private static double ToDouble(long time)
		{
			return (double)time * 1E-12;
		}

		private static float ToFloat(long time)
		{
			return (float)ToDouble(time);
		}

		public static explicit operator double(global::UnityEngine.Timeline.DiscreteTime b)
		{
			return ToDouble(b.m_DiscreteTime);
		}

		public static explicit operator float(global::UnityEngine.Timeline.DiscreteTime b)
		{
			return ToFloat(b.m_DiscreteTime);
		}

		public static explicit operator long(global::UnityEngine.Timeline.DiscreteTime b)
		{
			return b.m_DiscreteTime;
		}

		public static explicit operator global::UnityEngine.Timeline.DiscreteTime(double time)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(time);
		}

		public static explicit operator global::UnityEngine.Timeline.DiscreteTime(float time)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(time);
		}

		public static implicit operator global::UnityEngine.Timeline.DiscreteTime(int time)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(time);
		}

		public static explicit operator global::UnityEngine.Timeline.DiscreteTime(long time)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(time);
		}

		public static bool operator ==(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime == rhs.m_DiscreteTime;
		}

		public static bool operator !=(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator >(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime > rhs.m_DiscreteTime;
		}

		public static bool operator <(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime < rhs.m_DiscreteTime;
		}

		public static bool operator <=(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime <= rhs.m_DiscreteTime;
		}

		public static bool operator >=(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime >= rhs.m_DiscreteTime;
		}

		public static global::UnityEngine.Timeline.DiscreteTime operator +(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(lhs.m_DiscreteTime + rhs.m_DiscreteTime);
		}

		public static global::UnityEngine.Timeline.DiscreteTime operator -(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(lhs.m_DiscreteTime - rhs.m_DiscreteTime);
		}

		public override string ToString()
		{
			return m_DiscreteTime.ToString();
		}

		public override int GetHashCode()
		{
			return m_DiscreteTime.GetHashCode();
		}

		public static global::UnityEngine.Timeline.DiscreteTime Min(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(global::System.Math.Min(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		public static global::UnityEngine.Timeline.DiscreteTime Max(global::UnityEngine.Timeline.DiscreteTime lhs, global::UnityEngine.Timeline.DiscreteTime rhs)
		{
			return new global::UnityEngine.Timeline.DiscreteTime(global::System.Math.Max(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		public static double SnapToNearestTick(double time)
		{
			return ToDouble(DoubleToDiscreteTime(time));
		}

		public static float SnapToNearestTick(float time)
		{
			return ToFloat(FloatToDiscreteTime(time));
		}

		public static long GetNearestTick(double time)
		{
			return DoubleToDiscreteTime(time);
		}
	}
}
