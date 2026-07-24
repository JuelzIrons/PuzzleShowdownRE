namespace Unity.VisualScripting
{
	public sealed class AnimationCurveCloner : global::Unity.VisualScripting.Cloner<global::UnityEngine.AnimationCurve>
	{
		public override bool Handles(global::System.Type type)
		{
			return type == typeof(global::UnityEngine.AnimationCurve);
		}

		public override global::UnityEngine.AnimationCurve ConstructClone(global::System.Type type, global::UnityEngine.AnimationCurve original)
		{
			return new global::UnityEngine.AnimationCurve();
		}

		public override void FillClone(global::System.Type type, ref global::UnityEngine.AnimationCurve clone, global::UnityEngine.AnimationCurve original, global::Unity.VisualScripting.CloningContext context)
		{
			for (int i = 0; i < clone.length; i++)
			{
				clone.RemoveKey(i);
			}
			global::UnityEngine.Keyframe[] keys = original.keys;
			foreach (global::UnityEngine.Keyframe key in keys)
			{
				clone.AddKey(key);
			}
		}
	}
}
