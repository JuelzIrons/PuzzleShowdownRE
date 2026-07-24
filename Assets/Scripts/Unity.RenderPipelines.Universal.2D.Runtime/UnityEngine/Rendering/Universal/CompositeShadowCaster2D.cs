namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.AddComponentMenu("Rendering/2D/Composite Shadow Caster 2D")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(false, "UnityEngine.Experimental.Rendering.Universal", "com.unity.render-pipelines.universal", null)]
	[global::UnityEngine.ExecuteInEditMode]
	public class CompositeShadowCaster2D : global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D
	{
		protected void OnEnable()
		{
			global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.AddGroup(this);
		}

		protected void OnDisable()
		{
			global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.RemoveGroup(this);
		}
	}
}
