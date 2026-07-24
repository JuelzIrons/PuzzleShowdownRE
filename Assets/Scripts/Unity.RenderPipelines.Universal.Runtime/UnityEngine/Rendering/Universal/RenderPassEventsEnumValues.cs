namespace UnityEngine.Rendering.Universal
{
	internal static class RenderPassEventsEnumValues
	{
		public static int[] values;

		static RenderPassEventsEnumValues()
		{
			global::System.Array array = global::System.Enum.GetValues(typeof(global::UnityEngine.Rendering.Universal.RenderPassEvent));
			values = new int[array.Length];
			int num = 0;
			foreach (int item in array)
			{
				values[num] = item;
				num++;
			}
		}
	}
}
