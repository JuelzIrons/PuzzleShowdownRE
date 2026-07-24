namespace Unity.Networking.Transport.Utilities
{
	internal static class SequenceHelpers
	{
		internal static int AbsDistance(ushort lhs, ushort rhs)
		{
			if (GreaterThan16(lhs, rhs))
			{
				if (lhs <= rhs)
				{
					return lhs + 65535 + 1 - rhs;
				}
				return lhs - rhs;
			}
			if (rhs < lhs)
			{
				return rhs + 65535 + 1 - lhs;
			}
			return rhs - lhs;
		}

		internal static bool IsNewer(uint current, uint old)
		{
			return old - current >= 2147483648u;
		}

		internal static bool GreaterThan16(ushort lhs, ushort rhs)
		{
			if (lhs <= rhs || lhs - rhs > 32767)
			{
				if (lhs < rhs)
				{
					return rhs - lhs > 32767;
				}
				return false;
			}
			return true;
		}

		internal static bool LessThan16(ushort lhs, ushort rhs)
		{
			return GreaterThan16(rhs, lhs);
		}

		internal static bool StalePacket(ushort sequence, ushort oldSequence, ushort windowSize)
		{
			return LessThan16(sequence, (ushort)(oldSequence - windowSize));
		}
	}
}
