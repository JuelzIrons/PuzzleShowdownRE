namespace UnityEngine.Rendering.Universal
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public sealed class VolumeRequiresRendererFeatures : global::System.Attribute
	{
		internal global::System.Collections.Generic.HashSet<global::System.Type> TargetFeatureTypes;

		public VolumeRequiresRendererFeatures(params global::System.Type[] featureTypes)
		{
			TargetFeatureTypes = ((featureTypes != null) ? new global::System.Collections.Generic.HashSet<global::System.Type>(featureTypes) : new global::System.Collections.Generic.HashSet<global::System.Type>());
			TargetFeatureTypes.Remove(null);
		}
	}
}
