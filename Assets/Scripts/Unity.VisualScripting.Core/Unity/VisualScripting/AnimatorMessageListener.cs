namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("Visual Scripting/Listeners/Animator Message Listener")]
	public sealed class AnimatorMessageListener : global::UnityEngine.MonoBehaviour
	{
		private void OnAnimatorMove()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnAnimatorMove", base.gameObject);
		}

		private void OnAnimatorIK(int layerIndex)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnAnimatorIK", base.gameObject, layerIndex);
		}
	}
}
