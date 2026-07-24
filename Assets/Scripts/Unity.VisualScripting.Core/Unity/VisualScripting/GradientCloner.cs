namespace Unity.VisualScripting
{
	internal sealed class GradientCloner : global::Unity.VisualScripting.Cloner<global::UnityEngine.Gradient>
	{
		public override bool Handles(global::System.Type type)
		{
			return type == typeof(global::UnityEngine.Gradient);
		}

		public override global::UnityEngine.Gradient ConstructClone(global::System.Type type, global::UnityEngine.Gradient original)
		{
			return new global::UnityEngine.Gradient();
		}

		public override void FillClone(global::System.Type type, ref global::UnityEngine.Gradient clone, global::UnityEngine.Gradient original, global::Unity.VisualScripting.CloningContext context)
		{
			clone.mode = original.mode;
			clone.SetKeys(original.colorKeys, original.alphaKeys);
		}
	}
}
