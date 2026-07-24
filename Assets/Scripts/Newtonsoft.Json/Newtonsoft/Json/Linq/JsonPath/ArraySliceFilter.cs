namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class ArraySliceFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		public int? Start { get; set; }

		public int? End { get; set; }

		public int? Step { get; set; }

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			if (Step == 0)
			{
				throw new global::Newtonsoft.Json.JsonException("Step cannot be zero.");
			}
			foreach (global::Newtonsoft.Json.Linq.JToken item in current)
			{
				if (item is global::Newtonsoft.Json.Linq.JArray a)
				{
					int stepCount = Step ?? 1;
					int num = Start ?? ((stepCount <= 0) ? (a.Count - 1) : 0);
					int stopIndex = End ?? ((stepCount > 0) ? a.Count : (-1));
					if (Start < 0)
					{
						num = a.Count + num;
					}
					if (End < 0)
					{
						stopIndex = a.Count + stopIndex;
					}
					num = global::System.Math.Max(num, (stepCount <= 0) ? int.MinValue : 0);
					num = global::System.Math.Min(num, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = global::System.Math.Max(stopIndex, -1);
					stopIndex = global::System.Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (IsValid(num, stopIndex, positiveStep))
					{
						for (int i = num; IsValid(i, stopIndex, positiveStep); i += stepCount)
						{
							yield return a[i];
						}
					}
					else if (settings?.ErrorWhenNoMatch ?? false)
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Array slice of {0} to {1} returned no results.", global::System.Globalization.CultureInfo.InvariantCulture, Start.HasValue ? Start.GetValueOrDefault().ToString(global::System.Globalization.CultureInfo.InvariantCulture) : "*", End.HasValue ? End.GetValueOrDefault().ToString(global::System.Globalization.CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (settings?.ErrorWhenNoMatch ?? false)
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Array slice is not valid on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, item.GetType().Name));
				}
			}
		}

		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
