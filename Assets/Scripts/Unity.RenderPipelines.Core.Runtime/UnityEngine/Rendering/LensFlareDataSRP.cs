namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public sealed class LensFlareDataSRP : global::UnityEngine.ScriptableObject
	{
		public global::UnityEngine.Rendering.LensFlareDataElementSRP[] elements;

		public LensFlareDataSRP()
		{
			elements = null;
		}

		public bool HasAModulateByLightColorElement()
		{
			if (elements != null)
			{
				global::UnityEngine.Rendering.LensFlareDataElementSRP[] array = elements;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].modulateByLightColor)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
