namespace Newtonsoft.Json.Utilities
{
	internal static class JsonTokenUtils
	{
		internal static bool IsEndToken(global::Newtonsoft.Json.JsonToken token)
		{
			if ((uint)(token - 13) <= 2u)
			{
				return true;
			}
			return false;
		}

		internal static bool IsStartToken(global::Newtonsoft.Json.JsonToken token)
		{
			if ((uint)(token - 1) <= 2u)
			{
				return true;
			}
			return false;
		}

		internal static bool IsPrimitiveToken(global::Newtonsoft.Json.JsonToken token)
		{
			if ((uint)(token - 7) <= 5u || (uint)(token - 16) <= 1u)
			{
				return true;
			}
			return false;
		}
	}
}
